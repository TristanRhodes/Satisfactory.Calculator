namespace Satisfactory.Calculator
{
    public class Recipie
    {
        public Recipie(string code, ItemQuantity production, TimeSpan timeSpan, params ItemQuantity[] input)
        {
            Code = code;
            Output = production;
            Duration = timeSpan;
            Input = input.ToList();
        }

        public string Code { get; }
        public ItemQuantity Output { get;  }

        public List<ItemQuantity> Input { get;  }

        public TimeSpan Duration { get; }

        public double TicksPerMin()
        {
            var timespan = TimeSpan.FromMinutes(1);
            return timespan / Duration;
        }
    }
}