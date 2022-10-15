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
        public void CrystalOscillator()
        {
            var recipie = new Recipie(
                RecipieCodes.CrystalOscillator,
                new ItemQuantity(ItemCodes.CrystalOscillator, 2),
                TimeSpan.FromSeconds(120),
                new[] {
                    new ItemQuantity(ItemCodes.QuartzCrystal, 36),
                    new ItemQuantity(ItemCodes.Cable, 28),
                    new ItemQuantity(ItemCodes.ReinforcedIronPlate, 5)
                });

            recipie.Print();
        }
    }

    public static class RecipieLoader
    {
        public static RecipieRegister GetRegister()
        {
            var register = new RecipieRegister();
            register.Add(Recipies.IronPlate);
            register.Add(Recipies.IronWire);
            register.Add(Recipies.Wire);
            register.Add(Recipies.Cable);
            register.Add(Recipies.ReinforcedIronPlate);
            register.Add(Recipies.BoltedReinforcedIronPlate);
            return register;
        }
    }
}