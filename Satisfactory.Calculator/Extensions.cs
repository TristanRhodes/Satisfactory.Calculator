namespace Satisfactory.Calculator
{
    public static class Extensions
    {
        public static Recipie Print(this Recipie recipie)
        {
            Console.WriteLine($"Duration: {recipie.Duration}");

            var ticksPerMin = recipie.TicksPerMin();
            Console.WriteLine($"Ticks per Min: {ticksPerMin}");

            var prodPerMin = recipie.Output.Quantity * ticksPerMin;
            Console.WriteLine($"Production: {recipie.Output.Quantity} ({prodPerMin}) {recipie.Output.ItemCode}");

            Console.WriteLine("Consumption: ");
            foreach (var item in recipie.Input)
            {
                var consumptionPerMin = item.Quantity * ticksPerMin;
                Console.WriteLine($"{item.Quantity} ({consumptionPerMin}) {item.ItemCode}");
            }

            return recipie;
        }
    }

}