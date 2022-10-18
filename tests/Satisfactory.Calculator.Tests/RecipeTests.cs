using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Satisfactory.Calculator.Tests
{
    public class RecipeTests
    {

        IServiceProvider _services;
        IConfigurationRoot _config;
        RecipeRegister _recipes;
        ILogger _logger;
        DotGraphProcessor _graphProcessor;
        GraphBuilder _graphBuilder;

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
            var context = new CalculationContext(rootRecipie, 5);

            _graphBuilder = new GraphBuilder(rootRecipie.Code);

            WalkRecipeGraph(context);

            var dot = _graphBuilder.Compile();

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
            RenderRecipeNode(context);
            RenderInputs(context);
            RenderOutputs(context);
            WalkChildren(context);

            context.Pop();
        }

        private void RenderRecipeNode(CalculationContext context)
        {
            var current = context.Current;
            var recipeCode = context.GetCode();
            _logger.LogInformation($"Recipe: {current.Code}");
            _graphBuilder.AddRecipeNode(current, recipeCode);
        }

        private void RenderInputs(CalculationContext context)
        {
            var current = context.Current;
            var recipeCode = context.GetCode();

            foreach (var input in current.Input)
            {
                var inputCode = $"{recipeCode}.prod.{input.ItemCode}";
                var totalInputPerMin = current.Multiplier * current.Recipe.GetInputPerMin(input.ItemCode);

                _logger.LogInformation($"Input: {inputCode} x {input.Quantity}");
                _graphBuilder.AddRecipeInputNode(input, inputCode, totalInputPerMin, recipeCode);
            }
        }

        private void RenderOutputs(CalculationContext context)
        {
            var current = context.Current;
            var recipeCode = context.GetCode();

            foreach (var output in current.Output)
            {
                var parentInputCode = $"{context.GetParentCode()}.prod.{output.ItemCode}";
                var outputCode = $"{recipeCode}.prod.{output.ItemCode}";

                _logger.LogInformation($"Output: {outputCode} x {output.Quantity}");

                if (context.Current != context.RootRecipie)
                    _graphBuilder.Link(recipeCode, parentInputCode);
                else if (context.Current == context.RootRecipie)
                    _graphBuilder.AddRecipeOutputNode(current, output, outputCode, recipeCode);
            }
        }

        private void WalkChildren(CalculationContext context)
        {
            var current = context.Current;

            foreach (var input in current.Input)
            {
                var totalInputPerMin = current.Multiplier * current.Recipe.GetInputPerMin(input.ItemCode);

                var inputRecipes = _recipes.GetByOutputItem(input.ItemCode);
                foreach (var inputRecipe in inputRecipes)
                {
                    var inputOutput = inputRecipe.GetOutputPerMin(input.ItemCode);

                    var multiplier = totalInputPerMin / inputOutput;
                    context.Push(new StackItem(inputRecipe, multiplier));
                    WalkRecipeGraph(context);
                }
            }
        }
    }
}