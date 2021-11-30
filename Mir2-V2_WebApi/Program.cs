using Application.HelperServices.LoggingService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
namespace Mir2_V2_WebApi {
    public class Program {
        
        public static void Main(string[] args) {
            AddLogging();

            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] _args) {

            Log.Information("Server Starting");

            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(_args);
            hostBuilder.ConfigureAppConfiguration(_config => _config.AddJsonFile("DatabaseConfig.json")); // TODO: Load DatabaseConfig from Application.
            
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
                .WriteTo.Console(theme: RobsCustomTheme.Theme)
                .CreateLogger();
        }
    }
}
