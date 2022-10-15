using System.Linq;

namespace Satisfactory.Calculator
{
    public static class Extensions
    {
        public static Recipe Print(this Recipe Recipe)
        {
            Console.WriteLine($"Duration: {Recipe.Duration}");

            var ticksPerMin = Recipe.TicksPerMin();
            Console.WriteLine($"Ticks per Min: {ticksPerMin}");

            Console.WriteLine("Input: ");
            foreach (var item in Recipe.Input)
            {
                var consumptionPerMin = item.Quantity * ticksPerMin;
                Console.WriteLine($"{item.Quantity} ({consumptionPerMin}) {item.ItemCode}");
            }

            Console.WriteLine("Output: ");
            foreach (var item in Recipe.Output)
            {
                var prodPerMin = item.Quantity * ticksPerMin;
                Console.WriteLine($"{item.Quantity} ({prodPerMin}) {item.ItemCode}");
            }

            return Recipe;
        }

        public static string GetCode(this Stack<Recipe> RecipeStack)
        {
            return string.Join(".", RecipeStack.Reverse());
        }

        public static string GetParentCode(this Stack<Recipe> RecipeStack)
        {
            return string.Join(".", RecipeStack.Skip(1).Reverse());
        }
    }
}