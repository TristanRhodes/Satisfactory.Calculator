namespace Satisfactory.Calculator
{
    public class Recipe
    {
        public Recipe(string code, ItemQuantity production, TimeSpan timeSpan, params ItemQuantity[] input)
        {
            Code = code;
            Output = new List<ItemQuantity>() { production };
            Duration = timeSpan;
            Input = input.ToList();

            TicksPerMin = TimeSpan.FromMinutes(1) / Duration;
        }

        public Recipe(string code, ItemQuantity[] input, ItemQuantity[] output, TimeSpan timeSpan)
        {
            Code = code;
            Output = output.ToList();
            Duration = timeSpan;
            Input = input.ToList();

            TicksPerMin = TimeSpan.FromMinutes(1) / Duration;
        }

        public string Code { get; }

        public List<ItemQuantity> Input { get;  }

        public List<ItemQuantity> Output { get; }

        public ItemQuantity GetInput(string itemCode) =>
            Input.Single(r => r.ItemCode == itemCode);

        public double GetInputPerMin(string itemCode) =>
            Input.Single(r => r.ItemCode == itemCode).Quantity * TicksPerMin;

        public ItemQuantity GetOutput(string itemCode) =>
            Output.Single(r => r.ItemCode == itemCode);

        public double GetOutputPerMin(string itemCode) =>
            Output.Single(r => r.ItemCode == itemCode).Quantity * TicksPerMin;

        public TimeSpan Duration { get; }

        public double TicksPerMin { get; }

        public override string ToString() => Code;
    }
}