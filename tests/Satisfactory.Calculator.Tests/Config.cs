using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satisfactory.Calculator.Tests
{
    public class Config
    {
        public static IConfigurationRoot Configuration = 
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .Build();

        public static IServiceProvider CreateServiceCollection()
        {
            var services = new ServiceCollection();

            var logger = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            services.AddSingleton<IConfigurationRoot>(Configuration);
            services.AddSingleton<ILogger>(logger.CreateLogger<Config>());
            services.AddSingleton<DotGraphProcessor>();
            services.AddSingleton<RecipeRegister>(RecipeLoader.GetRegister());

            return services.BuildServiceProvider();
        }
    }
}
