using FluentAssertions;
using Xunit;

namespace Satisfactory.Calculator.Tests
{
    public class RecipeConfigTests
    {
        RecipeRegister _Recipes;

        public RecipeConfigTests()
        {
            _Recipes = RecipeLoader.GetRegister();
        }

        [Fact]
        public void IronPlates()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.IronPlate);

            Recipe.Duration.Should().Be(TimeSpan.FromSeconds(6));

            Recipe.GetInput(ItemCodes.IronIngot).Quantity.Should().Be(3);
            Recipe.GetInputPerMin(ItemCodes.IronIngot).Should().Be(30);

            Recipe.GetOutput(ItemCodes.IronPlate).Quantity.Should().Be(2);
            Recipe.GetOutputPerMin(ItemCodes.IronPlate).Should().Be(20);

            Recipe.Print();
        }

        [Fact]
        public void IronRods()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.IronRods);

            Recipe.Duration.Should().Be(TimeSpan.FromSeconds(4));

            Recipe.GetInput(ItemCodes.IronIngot).Quantity.Should().Be(1);
            Recipe.GetInputPerMin(ItemCodes.IronIngot).Should().Be(15);

            Recipe.GetOutput(ItemCodes.IronRods).Quantity.Should().Be(1);
            Recipe.GetOutputPerMin(ItemCodes.IronRods).Should().Be(15);

            Recipe.Print();
        }

        [Fact]
        public void ReinforcedIronPlates()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.ReinforcedIronPlate);

            Recipe.Duration.Should().Be(TimeSpan.FromSeconds(12));

            Recipe.GetInput(ItemCodes.IronPlate).Quantity.Should().Be(6);
            Recipe.GetInputPerMin(ItemCodes.IronPlate).Should().Be(30);

            Recipe.GetInput(ItemCodes.Screws).Quantity.Should().Be(12);
            Recipe.GetInputPerMin(ItemCodes.Screws).Should().Be(60);

            Recipe.GetOutput(ItemCodes.ReinforcedIronPlate).Quantity.Should().Be(1);
            Recipe.GetOutputPerMin(ItemCodes.ReinforcedIronPlate).Should().Be(5);

            Recipe.Print();
        }

        [Fact]
        public void BoltedReinforcedIronPlates()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.BoltedReinforcedIronPlate);

            Recipe.GetInput(ItemCodes.IronPlate).Quantity.Should().Be(18);
            Recipe.GetInputPerMin(ItemCodes.IronPlate).Should().Be(90);

            Recipe.GetInput(ItemCodes.Screws).Quantity.Should().Be(50);
            Recipe.GetInputPerMin(ItemCodes.Screws).Should().Be(250);

            Recipe.GetOutput(ItemCodes.ReinforcedIronPlate).Quantity.Should().Be(3);
            Recipe.GetOutputPerMin(ItemCodes.ReinforcedIronPlate).Should().Be(15);

            Recipe.Print();
        }

        [Fact]
        public void Wire()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.Wire);

            Recipe.Duration.Should().Be(TimeSpan.FromSeconds(4));

            Recipe.GetInput(ItemCodes.CopperIngot).Quantity.Should().Be(1);
            Recipe.GetInputPerMin(ItemCodes.CopperIngot).Should().Be(15);

            Recipe.GetOutput(ItemCodes.Wire).Quantity.Should().Be(2);
            Recipe.GetOutputPerMin(ItemCodes.Wire).Should().Be(30);

            Recipe.Print();
        }

        [Fact]
        public void IronWire()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.IronWire);

            Recipe.Duration.Should().Be(TimeSpan.FromSeconds(24));

            Recipe.GetInput(ItemCodes.IronIngot).Quantity.Should().Be(5);
            Recipe.GetInputPerMin(ItemCodes.IronIngot).Should().Be(12.5);

            Recipe.GetOutput(ItemCodes.Wire).Quantity.Should().Be(9);
            Recipe.GetOutputPerMin(ItemCodes.Wire).Should().Be(22.5);

            Recipe.Print();
        }

        [Fact]
        public void Cable()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.Cable);

            Recipe.Duration.Should().Be(TimeSpan.FromSeconds(2));

            Recipe.GetInput(ItemCodes.Wire).Quantity.Should().Be(2);
            Recipe.GetInputPerMin(ItemCodes.Wire).Should().Be(60);

            Recipe.GetOutput(ItemCodes.Cable).Quantity.Should().Be(1);
            Recipe.GetOutputPerMin(ItemCodes.Cable).Should().Be(30);

            Recipe.Print();
        }

        [Fact]
        public void QuartzCrystal()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.QuartzCrystal);

            Recipe.Duration.Should().Be(TimeSpan.FromSeconds(8));

            Recipe.GetInput(ItemCodes.RawQuartz).Quantity.Should().Be(5);
            Recipe.GetInputPerMin(ItemCodes.RawQuartz).Should().Be(37.5);

            Recipe.GetOutput(ItemCodes.QuartzCrystal).Quantity.Should().Be(3);
            Recipe.GetOutputPerMin(ItemCodes.QuartzCrystal).Should().Be(22.5);

            Recipe.Print();
        }

        [Fact]
        public void CrystalOscillator()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.CrystalOscillator);

            Recipe.Duration.Should().Be(TimeSpan.FromSeconds(120));

            Recipe.GetInput(ItemCodes.QuartzCrystal).Quantity.Should().Be(36);
            Recipe.GetInputPerMin(ItemCodes.QuartzCrystal).Should().Be(18);

            Recipe.GetInput(ItemCodes.ReinforcedIronPlate).Quantity.Should().Be(5);
            Recipe.GetInputPerMin(ItemCodes.ReinforcedIronPlate).Should().Be(2.5);

            Recipe.GetInput(ItemCodes.Cable).Quantity.Should().Be(28);
            Recipe.GetInputPerMin(ItemCodes.Cable).Should().Be(14);

            Recipe.GetOutput(ItemCodes.CrystalOscillator).Quantity.Should().Be(2);
            Recipe.GetOutputPerMin(ItemCodes.CrystalOscillator).Should().Be(1);

            Recipe.Print();
        }
    }
}