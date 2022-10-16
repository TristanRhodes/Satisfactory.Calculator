using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Satisfactory.Calculator.Tests
{
    /// <summary>
    /// Runs GraphViz Dot file and generates a PNG.
    /// </summary>
    public class DotGraphProcessor
    {
        IConfigurationRoot _config;

        public DotGraphProcessor(IConfigurationRoot config)
        {
            _config = config;
        }

        public async Task GenerateImage(string dotFile, string pngFile)
        {
            var dotExe = _config["dot-exe"];
            var startInfo = new ProcessStartInfo(dotExe, $"-Tpng \"{dotFile}\" -o \"{pngFile}\"");
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            var process = Process
                .Start(startInfo);

            var err = process.StandardError.ReadToEnd();
            Console.WriteLine(err);
            var std = process.StandardOutput.ReadToEnd();
            Console.WriteLine(std);

            await process.WaitForExitAsync();
        }
    }
}