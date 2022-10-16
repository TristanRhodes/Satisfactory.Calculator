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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FluentAssertions.Common;
using Microsoft.Extensions.Logging;

namespace Satisfactory.Calculator.Tests
{
    public class RecipeTests
    {
        const bool _useFullName = false;

        IServiceProvider _services;
        IConfigurationRoot _config;
        RecipeRegister _recipes;
        ILogger _logger;
        DotGraphProcessor _graphProcessor;

        public RecipeTests()
        {
            _services = Config.CreateServiceCollection();

            _config = _services.GetRequiredService<IConfigurationRoot>();
            _recipes = _services.GetRequiredService<RecipeRegister>();
            _logger = _services.GetRequiredService<ILogger>();
            _graphProcessor = _services.GetRequiredService<DotGraphProcessor>();
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

            Recipe.TicksPerMin
                .Should().Be(ticksPerMin);
        }

        [Theory]
        [InlineData(RecipeCodes.CrystalOscillator)]
        [InlineData(RecipeCodes.IronIngot)]
        [InlineData(RecipeCodes.ReinforcedIronPlate)]
        public async Task Generate(string Recipe)
        {
            var rootRecipie = _recipes.GetByRecipe(Recipe);
            var context = new CalculationContext(rootRecipie);

            WalkRecipeGraph(context);

            var dot = context.Compile();

            var fileDirectory = _config["output-directory"];

            if (!Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);

            var dotFile = Path.Combine(fileDirectory, $"{rootRecipie.Code}.dot");
            File.WriteAllText(dotFile, dot);
            var pngFile = Path.Combine(fileDirectory, $"{rootRecipie.Code}.png");

            await _graphProcessor.GenerateImage(dotFile, pngFile);
        }

        private void WalkRecipeGraph(CalculationContext context)
        {
            var recipe = context.Current;
            var graph = context.Graph;
            var recipeCode = context.GetCode();

            _logger.LogInformation($"Recipe: {recipe.Code}");

            graph.AddNode(recipeCode, n =>
            {
                var label = _useFullName ? recipeCode : recipe.Code;
                var htmlBuilder = GetHtmlRepresentation(recipe, label);

                n.Shape = DotNodeShape.None;
                n.Label = label + $"{Environment.NewLine}";
                n.SetCustomAttribute("label", $"<{htmlBuilder}>");
                n.FillColor = Color.LightGray;
                n.Color = Color.DarkGray;
                n.Style = DotNodeStyle.Filled;
            });

            foreach (var input in recipe.Input)
            {
                var inputCode = $"{recipeCode}.prod.{input.ItemCode}";
                var label = _useFullName ? inputCode : input.ItemCode;

                _logger.LogInformation($"Input: {inputCode} x {input.Quantity}");

                graph.AddNode(inputCode, n =>
                {
                    n.Shape = DotNodeShape.Oval;
                    n.Label = label;
                    n.FillColor = Color.LightBlue;
                    n.Color = Color.DarkGray;
                    n.Style = DotNodeStyle.Filled;
                });

                graph.AddEdge(inputCode, recipeCode, e =>
                {
                    e.Label = BuildInputEdgeMessage(recipe, input);
                });

                var inputRecipes = _recipes.GetByOutputItem(input.ItemCode);
                foreach (var inputRecipe in inputRecipes)
                {
                    context.Push(inputRecipe);
                    WalkRecipeGraph(context);
                }
            }

            foreach (var output in recipe.Output)
            {
                var parentInputCode = $"{context.GetParentCode()}.prod.{output.ItemCode}";
                var outputCode = $"{recipeCode}.prod.{output.ItemCode}";

                _logger.LogInformation($"Output: {outputCode} x {output.Quantity}");


                if (context.Current != context.RootRecipie)
                    graph.AddEdge(recipeCode, parentInputCode, e =>
                    {
                        e.Label = BuildOutputEdgeMessage(recipe, output);
                    });

                if (context.Current == context.RootRecipie)
                {
                    var outputMessage = _useFullName ? outputCode : output.ItemCode;

                    graph.AddNode(outputCode, n =>
                    {
                        n.Shape = DotNodeShape.Oval;
                        n.Label = outputMessage;
                        n.FillColor = Color.LightBlue;
                        n.Color = Color.DarkGray;
                        n.Style = DotNodeStyle.Filled;
                    });

                    graph.AddEdge(recipeCode, outputCode, e =>
                    {
                        e.Label = BuildOutputEdgeMessage(recipe, output);
                    });
                }
            }

            context.Pop();
        }

        private static string BuildInputEdgeMessage(Recipe recipe, ItemQuantity input) =>
            $"Input: {input.ItemCode} x ({recipe.GetInputPerMin(input.ItemCode)}pm)";

        private static string BuildOutputEdgeMessage(Recipe recipe, ItemQuantity output) => 
            $"Output: {output.ItemCode} x {output.Quantity} ({recipe.GetOutputPerMin(output.ItemCode)}pm)";
        private static StringBuilder GetHtmlRepresentation(Recipe recipe, string label)
        {
            // https://www.graphviz.org/doc/info/shapes.html#html

            var htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<TABLE BORDER=\"0\" CELLBORDER=\"1\" CELLSPACING=\"0\" CELLPADDING=\"4\">");

            foreach (var input in recipe.Input)
            {
                htmlBuilder.AppendLine("<TR>");
                htmlBuilder.AppendLine($"<TD>{input.ItemCode}</TD>");
                htmlBuilder.AppendLine($"<TD>{input.Quantity}</TD>");
                htmlBuilder.AppendLine($"<TD>{input.Quantity * recipe.TicksPerMin}pm</TD>");
                htmlBuilder.AppendLine("</TR>");
            }

            htmlBuilder.AppendLine("<TR>");
            htmlBuilder.AppendLine($"<TD COLSPAN=\"3\"><B>{label}</B>({recipe.Duration.TotalSeconds}s / {recipe.TicksPerMin}pm)</TD>");
            htmlBuilder.AppendLine("</TR>");

            foreach (var output in recipe.Output)
            {
                htmlBuilder.AppendLine("<TR>");
                htmlBuilder.AppendLine($"<TD>{output.ItemCode}</TD>");
                htmlBuilder.AppendLine($"<TD>{output.Quantity}</TD>");
                htmlBuilder.AppendLine($"<TD>{output.Quantity * recipe.TicksPerMin}pm</TD>");
                htmlBuilder.AppendLine("</TR>");
            }

            htmlBuilder.AppendLine("</TABLE>");
            return htmlBuilder;
        }
    }
}