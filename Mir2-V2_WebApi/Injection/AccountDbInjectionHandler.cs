using System;
using Application.Repository;
using Database_Mir2_V2_WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models_Mir2_V2_WebApi;
using Serilog;
namespace Mir2_V2_WebApi.Injection {
    public class AccountDbInjectionHandler {

        private ILogger _logger;

        public void SetDatabaseInjection(IServiceCollection services, IConfiguration configuration, DbProvider dbProvider) {
            switch (dbProvider) {
                case DbProvider.AwsDynamo:
                    InjectAwsDynamo(services);
                    break;
                case DbProvider.AwsMySql:
                    InjectAwsMySql(services);
                    break;
                case DbProvider.AzurePostgres:
                    InjectAzurePostgres(services, configuration);
                    break;
                case DbProvider.LocalPostgres:
                    InjectLocalPostgres(services, configuration);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dbProvider), dbProvider, null);
            }
        }
        private void InjectLocalPostgres(IServiceCollection services, IConfiguration configuration) {
            new InjectPostgres().InjectAccountDb(configuration, services);
        }
        private void InjectAzurePostgres(IServiceCollection services, IConfiguration configuration) {
        }

        private void InjectAwsMySql(IServiceCollection services) {
            throw new NotImplementedException();
        }

        private void InjectAwsDynamo(IServiceCollection services) {
            // AmazonInjectionBase.InjectBaseServices(_services);
            // _services.AddSingleton<IDataAccess, DynamoDbAccountRepository>();
        }
    }
}
