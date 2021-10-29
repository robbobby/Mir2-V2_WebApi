using Database_Mir2_V2_WebApi.Broker;
using Database_Mir2_V2_WebApi.PostgresLocalDev;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


using Microsoft.Extensions.Configuration;
using Mir2_v2_WebApi.Helpers.InjectionHandlers;
using Mir2_v2_WebApi.InjectionHandlers;
using Serilog;
namespace Mir2_V2_WebApi {
    public class Startup {
        public Startup(IConfiguration _configuration) {
            Configuration = _configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection _services) {

            _services.AddControllers();
            _services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Mir2_V2_WebApi",
                    Version = "v1"
                });
            });
            
            InjectionHandler.AccountDbInjectionHandler.SetDatabaseInjection(_services, Configuration, DbProvider.LocalPostgres);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mir2_V2_WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
