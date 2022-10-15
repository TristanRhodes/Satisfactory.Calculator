using DotNetGraph;
using DotNetGraph.Attributes;
using DotNetGraph.Extensions;
using DotNetGraph.Node;
using DotNetGraph.SubGraph;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using Xunit;

namespace Satisfactory.Calculator.Tests
{
    public class RecipieTests
    {
        RecipieRegister _recipies;

        public RecipieTests()
        {
            _recipies = RecipieLoader.GetRegister();
        }

        [Theory]
        [InlineData(1, 60)]
        [InlineData(2, 30)]
        [InlineData(4, 15)]
        public void TicksPerMin(int seconds, int ticksPerMin)
        {
            var recipie = new Recipie("Name",
                new[] { new ItemQuantity("Input", 1) },
                new[] { new ItemQuantity("Output", 1) },
                TimeSpan.FromSeconds(seconds));

            recipie.TicksPerMin()
                .Should().Be(ticksPerMin);
        }

        [Theory]
        [InlineData(RecipieCodes.CrystalOscillator)]
        [InlineData(RecipieCodes.IronIngot)]
        [InlineData(RecipieCodes.ReinforcedIronPlate)]
        public async Task Generate(string recipie)
        {
            var source = _recipies.GetByRecipie(recipie);

            var graph = new DotGraph(source.Code, true);


            var stack = new Stack<Recipie>();
            OutputForRecipie(source, graph, stack);

            var dot = graph.Compile(true);

            // TODO: Parameterise
            var fileDirectory = "C:\\temp\\satisfactory";
            var dotExe = "C:\\Program Files\\Graphviz\\bin\\dot";

            if (!Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);

            var dotFile = Path.Combine(fileDirectory, $"{source.Code}.dot");
            File.WriteAllText(dotFile, dot);
            var pngFile = Path.Combine(fileDirectory, $"{source.Code}.png");

            var startInfo = new ProcessStartInfo(dotExe, $"-Tpng {dotFile} -o {pngFile}");
            startInfo.RedirectStandardOutput = true;

            var process = Process
                .Start(startInfo);

            await process.WaitForExitAsync();
        }

        private void OutputForRecipie(Recipie recipie, IDotGraph graph, Stack<Recipie> stack)
        {
            const bool useFullName = false;
            Log($"Recipie: {recipie.Code}");

            stack.Push(recipie);
            var recipieCode = stack.GetCode();
            var ticksPerMin = 60 / recipie.Duration.TotalSeconds;

            var recipieNode = graph.AddNode(recipieCode, n =>
            {
                n.Shape = DotNodeShape.Oval;
                n.Label = (useFullName ? recipieCode : recipie.Code) + $"{Environment.NewLine}{recipie.Duration.TotalSeconds}s ({ticksPerMin}tpm)" ;
                n.FillColor = Color.LightGreen;
                n.Color = Color.DarkGray;
                n.Style = DotNodeStyle.Filled;
            });

            foreach (var input in recipie.Input)
            {
                var inputCode = $"{recipieCode}.in.{input.ItemCode}";
                var quantity = input.Quantity;

                Log($"Input: {inputCode} x {quantity}");

                var node = graph.AddNode(inputCode, n =>
                {
                    n.Shape = DotNodeShape.Oval;
                    n.Label = useFullName ? inputCode : input.ItemCode;
                    n.FillColor = Color.LightBlue;
                    n.Color = Color.DarkGray;
                    n.Style = DotNodeStyle.Filled;
                });

                graph.AddEdge(inputCode, recipieCode, e =>
                {
                    e.Label = $"Input: {input.ItemCode} x {quantity} ({ticksPerMin * quantity}pm)";
                });

                var inputRecipies = _recipies.GetByOutputItem(input.ItemCode);
                foreach (var inputRecipie in inputRecipies)
                {
                    OutputForRecipie(inputRecipie, graph, stack);
                }
            }

            foreach (var output in recipie.Output)
            {
                var parentInputCode = $"{stack.GetParentCode()}.in.{output.ItemCode}";
                var outputCode = $"{recipieCode}.out.{output.ItemCode}";
                var quantity = output.Quantity;

                Log($"Output: {outputCode} x {quantity}");

                var node = graph.AddNode(outputCode, n =>
                {
                    n.Shape = DotNodeShape.Oval;
                    n.Label = useFullName ? outputCode : output.ItemCode;
                    n.FillColor = Color.LightSalmon;
                    n.Color = Color.DarkGray;
                    n.Style = DotNodeStyle.Filled;
                });

                graph.AddEdge(recipieCode, outputCode, e =>
                {
                    e.Label = $"Output: {output.ItemCode} x {quantity} ({ticksPerMin * quantity}pm)";
                });
                
                if (stack.Count > 1)
                    graph.AddEdge(outputCode, parentInputCode);
            }

            stack.Pop();
        }

        private static void Log(string message) => 
            Console.WriteLine(message);
    }
}