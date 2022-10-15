using FluentAssertions;
using System.Security.Cryptography.X509Certificates;
using Xunit;

namespace Satisfactory.Calculator
{
    public class RecipieTests
    {
        RecipieRegister _recipies;

        public RecipieTests()
        {
            _recipies = RecipieLoader.GetRegister();
        }

        [Theory]
        [InlineData(1, 60)]
        [InlineData(2, 30)]
        [InlineData(4, 15)]
        public void TicksPerMin(int seconds, int ticksPerMin)
        {
            var recipie = new Recipie("code", 
                new ItemQuantity("code", 2), 
                TimeSpan.FromSeconds(seconds));

            recipie.TicksPerMin()
                .Should().Be(ticksPerMin);
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