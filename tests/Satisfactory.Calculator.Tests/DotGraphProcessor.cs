using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Satisfactory.Calculator.Tests
{
    /// <summary>
    /// Runs GraphViz Dot file and generates a PNG.
    /// </summary>
    public class DotGraphProcessor
    {
        IConfigurationRoot _config;
        ILogger _logger;

        public DotGraphProcessor(IConfigurationRoot config, ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task GenerateImage(string dotFile, string pngFile)
        {
            var graphViz = new GraphVizNet.GraphViz();

            graphViz.LayoutAndRenderDotGraphFromFile(dotFile, pngFile, "png");        
        }
    }
}