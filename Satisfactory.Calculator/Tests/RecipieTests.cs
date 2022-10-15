using FluentAssertions;
using System.Security.Cryptography.X509Certificates;
using Xunit;

namespace Satisfactory.Calculator.Tests
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
    }
}