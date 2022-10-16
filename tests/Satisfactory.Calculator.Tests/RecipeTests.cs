using DotNetGraph.Attributes;
using DotNetGraph.Extensions;
using DotNetGraph.Node;
using DotNetGraph.SubGraph;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Xunit;
using Microsoft.Extensions.Configuration;
using DotNetGraph.Core;
using System.Text;

namespace Satisfactory.Calculator.Tests
{
    public class RecipeTests
    {
        IConfigurationRoot _config;
        RecipeRegister _Recipes;
         DotGraphProcessor _graphProcessor;

        public RecipeTests()
        {
            _config = Config.Configuration;
            _Recipes = RecipeLoader.GetRegister();
            _graphProcessor = new DotGraphProcessor(_config);
        }

        [Theory]
        [InlineData(1, 60)]
        [InlineData(2, 30)]
        [InlineData(4, 15)]
        public void TicksPerMin(int seconds, int ticksPerMin)
        {
            var Recipe = new Recipe("Name",
                new[] { new ItemQuantity("Input", 1) },
                new[] { new ItemQuantity("Output", 1) },
                TimeSpan.FromSeconds(seconds));

            Recipe.TicksPerMin()
                .Should().Be(ticksPerMin);
        }

        [Theory]
        [InlineData(RecipeCodes.CrystalOscillator)]
        [InlineData(RecipeCodes.IronIngot)]
        [InlineData(RecipeCodes.ReinforcedIronPlate)]
        public async Task Generate(string Recipe)
        {
            var source = _Recipes.GetByRecipe(Recipe);
            var context = new CalculationContext(source.Code);

            OutputForRecipe(source, context);

            var dot = context.Compile();

            var fileDirectory = _config["output-directory"];

            if (!Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);

            var dotFile = Path.Combine(fileDirectory, $"{source.Code}.dot");
            File.WriteAllText(dotFile, dot);
            var pngFile = Path.Combine(fileDirectory, $"{source.Code}.png");

            await _graphProcessor.GenerateImage(dotFile, pngFile);
        }



        private void OutputForRecipe(Recipe recipe, CalculationContext context)
        {
            var stack = context.Stack;
            var graph = context.Graph;

            const bool useFullName = false;
            Log($"Recipe: {recipe.Code}");

            stack.Push(recipe);
            var recipeCode = stack.GetCode();
            var ticksPerMin = 60 / recipe.Duration.TotalSeconds;

            var RecipeNode = graph.AddNode(recipeCode, n =>
            {
                var label = useFullName ? recipeCode : recipe.Code;
                var htmlBuilder = GetHtmlRepresentation(recipe, label);

                n.Shape = DotNodeShape.None;
                n.Label = label + $"{Environment.NewLine}";
                n.SetCustomAttribute("label", $"<{htmlBuilder}>");
                n.FillColor = Color.LightGreen;
                n.Color = Color.DarkGray;
                n.Style = DotNodeStyle.Filled;
            });

            foreach (var input in recipe.Input)
            {
                var inputCode = $"{recipeCode}.prod.{input.ItemCode}";
                var quantity = input.Quantity;

                Log($"Input: {inputCode} x {quantity}");

                var label = useFullName ? inputCode : input.ItemCode;

                var node = graph.AddNode(inputCode, n =>
                {
                    n.Shape = DotNodeShape.Oval;
                    n.Label = label;
                    n.FillColor = Color.LightBlue;
                    n.Color = Color.DarkGray;
                    n.Style = DotNodeStyle.Filled;
                });

                graph.AddEdge(inputCode, recipeCode, e =>
                {
                    e.Label = $"Input: {input.ItemCode} x {quantity} ({ticksPerMin * quantity}pm)";
                });

                var inputRecipes = _Recipes.GetByOutputItem(input.ItemCode);
                foreach (var inputRecipe in inputRecipes)
                {
                    OutputForRecipe(inputRecipe, context);
                }
            }

            foreach (var output in recipe.Output)
            {
                var parentInputCode = $"{stack.GetParentCode()}.prod.{output.ItemCode}";
                var outputCode = $"{recipeCode}.prod.{output.ItemCode}";
                var quantity = output.Quantity;

                Log($"Output: {outputCode} x {quantity}");


                if (stack.Count > 1)
                    graph.AddEdge(recipeCode, parentInputCode, e =>
                    {
                        e.Label = $"Output: {output.ItemCode} x {quantity} ({ticksPerMin * quantity}pm)";
                    });

                if (stack.Count == 1)
                {
                    var node = graph.AddNode(outputCode, n =>
                    {
                        n.Shape = DotNodeShape.Oval;
                        n.Label = useFullName ? outputCode : output.ItemCode;
                        n.FillColor = Color.LightBlue;
                        n.Color = Color.DarkGray;
                        n.Style = DotNodeStyle.Filled;
                    });

                    graph.AddEdge(recipeCode, outputCode, e =>
                    {
                        e.Label = $"Output: {output.ItemCode} x {quantity} ({ticksPerMin * quantity}pm)";
                    });
                }
            }

            stack.Pop();
        }

        private static StringBuilder GetHtmlRepresentation(Recipe recipe, string label)
        {
            // https://www.graphviz.org/doc/info/shapes.html#html
            var ticksPerMin = 60 / recipe.Duration.TotalSeconds;

            var htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<TABLE BORDER=\"0\" CELLBORDER=\"1\" CELLSPACING=\"0\" CELLPADDING=\"4\">");

            foreach (var input in recipe.Input)
            {
                htmlBuilder.AppendLine("<TR>");
                htmlBuilder.AppendLine($"<TD>{input.ItemCode}</TD>");
                htmlBuilder.AppendLine($"<TD>{input.Quantity}</TD>");
                htmlBuilder.AppendLine($"<TD>{input.Quantity * ticksPerMin}pm</TD>");
                htmlBuilder.AppendLine("</TR>");
            }

            htmlBuilder.AppendLine("<TR>");
            htmlBuilder.AppendLine($"<TD COLSPAN=\"3\"><B>{label}</B>({recipe.Duration.TotalSeconds}s / {ticksPerMin}pm)</TD>");
            htmlBuilder.AppendLine("</TR>");

            foreach (var output in recipe.Output)
            {
                htmlBuilder.AppendLine("<TR>");
                htmlBuilder.AppendLine($"<TD>{output.ItemCode}</TD>");
                htmlBuilder.AppendLine($"<TD>{output.Quantity}</TD>");
                htmlBuilder.AppendLine($"<TD>{output.Quantity * ticksPerMin}pm</TD>");
                htmlBuilder.AppendLine("</TR>");
            }

            htmlBuilder.AppendLine("</TABLE>");
            return htmlBuilder;
        }

        private static void Log(string message) =>
            Console.WriteLine(message);
    }
}