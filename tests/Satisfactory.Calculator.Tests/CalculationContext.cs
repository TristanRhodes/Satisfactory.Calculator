using DotNetGraph;
using DotNetGraph.Extensions;

namespace Satisfactory.Calculator.Tests
{
    public class CalculationContext
    {
        public CalculationContext(Recipe recipe)
        {
            RootRecipie = recipe;
            Graph = new DotGraph(recipe.Code, true);
            Stack = new Stack<Recipe>();
            Stack.Push(RootRecipie);
        }

        public Recipe RootRecipie { get; }

        public Recipe Current => Stack.Peek();

        public DotGraph Graph { get; }

        public Stack<Recipe> Stack { get; }

        public void Push(Recipe recipe) =>
            Stack.Push(recipe);

        public Recipe Pop() =>
            Stack.Pop();

        public string Compile() =>
            Graph.Compile(true);

        public string GetCode() =>
            Stack.GetCode();

        public string GetParentCode() =>
            Stack.GetParentCode();
    }
}