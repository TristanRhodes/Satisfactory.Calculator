using DotNetGraph.Extensions;
using DotNetGraph.Node;
using System.Drawing;
using System.Text;
using DotNetGraph;

namespace Satisfactory.Calculator.Tests
{
    /// <summary>
    /// Used for assembling the DOT graph
    /// </summary>
    public class GraphBuilder
    {
        public DotGraph Graph { get; }
        public static bool UseFullName = false;

        public GraphBuilder(string rootCode)
        {
            Graph = new DotGraph(rootCode, true);
        }

        public void AddRecipeNode(StackItem current, string recipeCode) => Graph.AddNode(recipeCode, n =>
        {
            var nodeMessage = UseFullName ? recipeCode : current.Code;

            var htmlBuilder = GetHtmlRepresentation(current.Recipe, nodeMessage, current.Multiplier);

            n.Shape = DotNodeShape.None;
            n.Label = nodeMessage;
            n.SetCustomAttribute("label", $"<{htmlBuilder}>");
            n.FillColor = Color.LightGray;
            n.Color = Color.DarkGray;
            n.Style = DotNodeStyle.Filled;
        });

        public void AddRecipeInputNode(ItemQuantity input, string inputCode, double totalInputPerMin, string recipeCode)
        {
            var inputMessage = UseFullName ? inputCode : input.ItemCode;
            inputMessage += Environment.NewLine;
            inputMessage += $"{totalInputPerMin}pm";

            Graph.AddNode(inputCode, n =>
            {
                n.Shape = DotNodeShape.Oval;
                n.Label = inputMessage;
                n.FillColor = Color.LightBlue;
                n.Color = Color.DarkGray;
                n.Style = DotNodeStyle.Filled;
            });

            Graph.AddEdge(inputCode, recipeCode);
        }

        public void AddRecipeOutputNode(StackItem current, ItemQuantity output, string outputCode, string recipeCode)
        {
            var outputMessage = UseFullName ? outputCode : output.ItemCode;
            outputMessage += Environment.NewLine;
            outputMessage += $"{current.Multiplier * current.Recipe.GetOutputPerMin(output.ItemCode)}pm";

            Graph.AddNode(outputCode, n =>
            {
                n.Shape = DotNodeShape.Oval;
                n.Label = outputMessage;
                n.FillColor = Color.LightBlue;
                n.Color = Color.DarkGray;
                n.Style = DotNodeStyle.Filled;
            });

            Graph.AddEdge(recipeCode, outputCode);
        }

        private static StringBuilder GetHtmlRepresentation(Recipe recipe, string label, double multiplier)
        {
            // https://www.graphviz.org/doc/info/shapes.html#html

            var htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<TABLE BORDER=\"0\" CELLBORDER=\"1\" CELLSPACING=\"0\" CELLPADDING=\"4\">");

            var rowCount = recipe.Input.Count + recipe.Output.Count + 1;
            var firstRow = true;

            foreach (var input in recipe.Input)
            {
                htmlBuilder.AppendLine("<TR>");
                htmlBuilder.AppendLine($"<TD>{input.ItemCode}</TD>");
                htmlBuilder.AppendLine($"<TD>{input.Quantity}</TD>");
                htmlBuilder.AppendLine($"<TD>{input.Quantity * recipe.TicksPerMin}pm</TD>");
                if (firstRow)
                {
                    htmlBuilder.AppendLine($"<TD ROWSPAN=\"{rowCount}\">x{multiplier}</TD>");
                    firstRow = false;
                }
                htmlBuilder.AppendLine("</TR>");
            }

            htmlBuilder.AppendLine("<TR>");
            htmlBuilder.AppendLine($"<TD COLSPAN=\"3\"><B>{label}</B> ({recipe.Duration.TotalSeconds}s / {recipe.TicksPerMin}pm)</TD>");
            if (firstRow)
            {
                htmlBuilder.AppendLine($"<TD ROWSPAN=\"{rowCount}\">x{multiplier}</TD>");
                firstRow = false;
            }
            htmlBuilder.AppendLine("</TR>");

            foreach (var output in recipe.Output)
            {
                htmlBuilder.AppendLine("<TR>");
                htmlBuilder.AppendLine($"<TD>{output.ItemCode}</TD>");
                htmlBuilder.AppendLine($"<TD>{output.Quantity}</TD>");
                htmlBuilder.AppendLine($"<TD>{output.Quantity * recipe.TicksPerMin}pm</TD>");
                htmlBuilder.AppendLine("</TR>");
            }

            htmlBuilder.AppendLine("</TABLE>");
            return htmlBuilder;
        }

        public void Link(string recipeCode, string parentInputCode) =>
            Graph.AddEdge(recipeCode, parentInputCode);

        public string Compile() =>
            Graph.Compile();
    }
}