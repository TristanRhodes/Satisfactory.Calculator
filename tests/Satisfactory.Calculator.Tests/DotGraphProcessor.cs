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
            var dotExe = _config["dot-exe"];
            var startInfo = new ProcessStartInfo(dotExe, $"-Tpng \"{dotFile}\" -o \"{pngFile}\"");
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            var process = Process
                .Start(startInfo);

            var err = process.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(err))
                throw new ApplicationException(err);

            var std = process.StandardOutput.ReadToEnd();
            if (!string.IsNullOrEmpty(std))
                _logger.LogInformation(std);

            await process.WaitForExitAsync();
        }
    }
}