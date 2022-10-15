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

            Recipe.Print();
        }

        [Fact]
        public void IronRods()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.IronRods);

            Recipe.Print();
        }

        [Fact]
        public void ReinforcedIronPlates()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.ReinforcedIronPlate);

            Recipe.Print();
        }

        [Fact]
        public void BoltedReinforcedIronPlates()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.BoltedReinforcedIronPlate);

            Recipe.Print();
        }

        [Fact]
        public void Wire()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.Wire);

            Recipe.Print();
        }

        [Fact]
        public void IronWire()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.IronWire);

            Recipe.Print();
        }

        [Fact]
        public void Cable()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.Cable);

            Recipe.Print();
        }

        [Fact]
        public void QuartzCrystal()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.QuartzCrystal);

            Recipe.Print();
        }

        [Fact]
        public void CrystalOscillator()
        {
            var Recipe = _Recipes
                .GetByRecipe(RecipeCodes.CrystalOscillator);

            Recipe.Print();
        }
    }
}