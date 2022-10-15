using Xunit;

namespace Satisfactory.Calculator.Tests
{
    public class RecipieConfigTests
    {
        RecipieRegister _recipies;

        public RecipieConfigTests()
        {
            _recipies = RecipieLoader.GetRegister();
        }

        [Fact]
        public void IronPlates()
        {
            var recipie = _recipies
                .GetByRecipie(RecipieCodes.IronPlate);

            recipie.Print();
        }

        [Fact]
        public void IronRods()
        {
            var recipie = _recipies
                .GetByRecipie(RecipieCodes.IronRods);

            recipie.Print();
        }

        [Fact]
        public void ReinforcedIronPlates()
        {
            var recipie = _recipies
                .GetByRecipie(RecipieCodes.ReinforcedIronPlate);

            recipie.Print();
        }

        [Fact]
        public void BoltedReinforcedIronPlates()
        {
            var recipie = _recipies
                .GetByRecipie(RecipieCodes.BoltedReinforcedIronPlate);

            recipie.Print();
        }

        [Fact]
        public void Wire()
        {
            var recipie = _recipies
                .GetByRecipie(RecipieCodes.Wire);

            recipie.Print();
        }

        [Fact]
        public void IronWire()
        {
            var recipie = _recipies
                .GetByRecipie(RecipieCodes.IronWire);

            recipie.Print();
        }

        [Fact]
        public void Cable()
        {
            var recipie = _recipies
                .GetByRecipie(RecipieCodes.Cable);

            recipie.Print();
        }

        [Fact]
        public void QuartzCrystal()
        {
            var recipie = _recipies
                .GetByRecipie(RecipieCodes.QuartzCrystal);

            recipie.Print();
        }

        [Fact]
        public void CrystalOscillator()
        {
            var recipie = _recipies
                .GetByRecipie(RecipieCodes.CrystalOscillator);

            recipie.Print();
        }
    }
}