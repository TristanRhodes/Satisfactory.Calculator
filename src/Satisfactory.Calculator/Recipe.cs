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
        }

        public Recipe(string code, ItemQuantity[] input, ItemQuantity[] output, TimeSpan timeSpan)
        {
            Code = code;
            Output = output.ToList();
            Duration = timeSpan;
            Input = input.ToList();
        }

        public string Code { get; }

        public List<ItemQuantity> Input { get;  }

        public List<ItemQuantity> Output { get; }

        public TimeSpan Duration { get; }

        public double TicksPerMin()
        {
            var timespan = TimeSpan.FromMinutes(1);
            return timespan / Duration;
        }

        public override string ToString() => Code;
    }
}