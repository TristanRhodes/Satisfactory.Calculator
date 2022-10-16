using System.Linq;

namespace Satisfactory.Calculator
{
    public static class Extensions
    {
        public static Recipe Print(this Recipe recipe)
        {
            Console.WriteLine($"Duration: {recipe.Duration}");

            var ticksPerMin = recipe.TicksPerMin;
            Console.WriteLine($"Ticks per Min: {ticksPerMin}");

            Console.WriteLine("Input: ");
            foreach (var item in recipe.Input)
            {
                var consumptionPerMin = item.Quantity * ticksPerMin;
                Console.WriteLine($"{item.Quantity} ({consumptionPerMin}) {item.ItemCode}");
            }

            Console.WriteLine("Output: ");
            foreach (var item in recipe.Output)
            {
                var prodPerMin = item.Quantity * ticksPerMin;
                Console.WriteLine($"{item.Quantity} ({prodPerMin}) {item.ItemCode}");
            }

            return recipe;
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