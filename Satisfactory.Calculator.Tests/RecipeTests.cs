using DotNetGraph;
using DotNetGraph.Attributes;
using DotNetGraph.Extensions;
using DotNetGraph.Node;
using DotNetGraph.SubGraph;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using Xunit;

namespace Satisfactory.Calculator.Tests
{
    public class RecipeTests
    {
        RecipeRegister _Recipes;

        public RecipeTests()
        {
            _Recipes = RecipeLoader.GetRegister();
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

            var graph = new DotGraph(source.Code, true);


            var stack = new Stack<Recipe>();
            OutputForRecipe(source, graph, stack);

            var dot = graph.Compile(true);

            // TODO: Parameterise
            var fileDirectory = "C:\\temp\\satisfactory";
            var dotExe = "C:\\Program Files\\Graphviz\\bin\\dot";

            if (!Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);

            var dotFile = Path.Combine(fileDirectory, $"{source.Code}.dot");
            File.WriteAllText(dotFile, dot);
            var pngFile = Path.Combine(fileDirectory, $"{source.Code}.png");

            var startInfo = new ProcessStartInfo(dotExe, $"-Tpng {dotFile} -o {pngFile}");
            startInfo.RedirectStandardOutput = true;

            var process = Process
                .Start(startInfo);

            await process.WaitForExitAsync();
        }

        private void OutputForRecipe(Recipe recipe, IDotGraph graph, Stack<Recipe> stack)
        {
            const bool useFullName = true;
            Log($"Recipe: {recipe.Code}");

            stack.Push(recipe);
            var recipeCode = stack.GetCode();
            var ticksPerMin = 60 / recipe.Duration.TotalSeconds;

            var RecipeNode = graph.AddNode(recipeCode, n =>
            {
                n.Shape = DotNodeShape.Oval;
                n.Label = (useFullName ? recipeCode : recipe.Code) + $"{Environment.NewLine}{recipe.Duration.TotalSeconds}s ({ticksPerMin}tpm)" ;
                n.FillColor = Color.LightGreen;
                n.Color = Color.DarkGray;
                n.Style = DotNodeStyle.Filled;
            });

            foreach (var input in recipe.Input)
            {
                var inputCode = $"{recipeCode}.prod.{input.ItemCode}";
                var quantity = input.Quantity;

                Log($"Input: {inputCode} x {quantity}");

                var node = graph.AddNode(inputCode, n =>
                {
                    n.Shape = DotNodeShape.Oval;
                    n.Label = useFullName ? inputCode : input.ItemCode;
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
                    OutputForRecipe(inputRecipe, graph, stack);
                }
            }

            foreach (var output in recipe.Output)
            {
                var parentInputCode = $"{stack.GetParentCode()}.prod.{output.ItemCode}";
                var outputCode = $"{recipeCode}.prod.{output.ItemCode}";
                var quantity = output.Quantity;

                Log($"Output: {outputCode} x {quantity}");

                var node = graph.AddNode(outputCode, n =>
                {
                    n.Shape = DotNodeShape.Oval;
                    n.Label = useFullName ? outputCode : output.ItemCode;
                    n.FillColor = Color.LightSalmon;
                    n.Color = Color.DarkGray;
                    n.Style = DotNodeStyle.Filled;
                });

                graph.AddEdge(recipeCode, outputCode, e =>
                {
                    e.Label = $"Output: {output.ItemCode} x {quantity} ({ticksPerMin * quantity}pm)";
                });
                
                if (stack.Count > 1)
                    graph.AddEdge(outputCode, parentInputCode);
            }

            stack.Pop();
        }

        private static void Log(string message) => 
            Console.WriteLine(message);
    }
}