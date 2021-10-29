using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Mir2_V2_WebApi {
    public class Program {
        public static void Main(string[] args) {

            AddLogging();
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] _args) {

            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(_args);
            hostBuilder.ConfigureAppConfiguration(config => config.AddJsonFile("DatabaseConfig.json"));
            hostBuilder.ConfigureWebHostDefaults(_webBuilder => {
                _webBuilder.UseStartup<Startup>();
            });
            hostBuilder.UseSerilog();

            return hostBuilder;
        }
        private static void AddLogging() {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}
