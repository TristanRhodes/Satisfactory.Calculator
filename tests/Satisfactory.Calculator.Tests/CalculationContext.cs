using DotNetGraph;
using DotNetGraph.Extensions;

namespace Satisfactory.Calculator.Tests
{
    public class CalculationContext
    {
        public CalculationContext(Recipe recipe, double mutliplier)
        {
            RootRecipie = new StackItem(null, recipe, mutliplier);
            Stack = new Stack<StackItem>();
            Stack.Push(RootRecipie);
        }

        public StackItem RootRecipie { get; }

        public StackItem Current => Stack.Peek();

        public Stack<StackItem> Stack { get; }

        public void Push(StackItem stackItem) =>
            Stack.Push(stackItem);

        public StackItem Pop() =>
            Stack.Pop();
    }

    public record StackItem(StackItem parent, Recipe Recipe, double Multiplier)
    {
        public string Code => Recipe.Code;
        public List<ItemQuantity> Input => Recipe.Input;
        public List<ItemQuantity> Output => Recipe.Output;
    }
}