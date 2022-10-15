namespace Satisfactory.Calculator
{
    public static class Extensions
    {
        public static Recipie Print(this Recipie recipie)
        {
            Console.WriteLine($"Duration: {recipie.Duration}");

            var ticksPerMin = recipie.TicksPerMin();
            Console.WriteLine($"Ticks per Min: {ticksPerMin}");

            Console.WriteLine("Input: ");
            foreach (var item in recipie.Input)
            {
                var consumptionPerMin = item.Quantity * ticksPerMin;
                Console.WriteLine($"{item.Quantity} ({consumptionPerMin}) {item.ItemCode}");
            }

            Console.WriteLine("Output: ");
            foreach (var item in recipie.Output)
            {
                var prodPerMin = item.Quantity * ticksPerMin;
                Console.WriteLine($"{item.Quantity} ({prodPerMin}) {item.ItemCode}");
            }

            return recipie;
        }
    }

}