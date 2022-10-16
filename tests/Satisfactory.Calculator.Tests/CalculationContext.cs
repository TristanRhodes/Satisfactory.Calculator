using DotNetGraph;
using DotNetGraph.Extensions;

namespace Satisfactory.Calculator.Tests
{
    public class CalculationContext
    {
        public CalculationContext(string sourceCode)
        {
            Graph = new DotGraph(sourceCode, true);
            Stack = new Stack<Recipe>();
        }

        public DotGraph Graph { get; }

        public Stack<Recipe> Stack { get; }

        public string Compile() =>
            Graph.Compile(true);
    }
}