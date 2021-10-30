using System;
using Database_Mir2_V2_WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models_Mir2_V2_WebApi;
using Serilog;
namespace Mir2_V2_WebApi.Injection {
    public class AccountDbInjectionHandler {

        private ILogger logger;
        public AccountDbInjectionHandler() {
        }

        public void SetDatabaseInjection(IServiceCollection _services, IConfiguration _configuration, DbProvider _dbProvider) {
            switch (_dbProvider) {
                case DbProvider.AwsDynamo:
                    InjectAwsDynamo(_services);
                    break;
                case DbProvider.AwsMySql:
                    InjectAwsMySql(_services);
                    break;
                case DbProvider.AzurePostgres:
                    InjectAzurePostgres(_services, _configuration);
                    break;
                case DbProvider.LocalPostgres:
                    InjectLocalPostgres(_services, _configuration);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_dbProvider), _dbProvider, null);
            }
        }
        private void InjectLocalPostgres(IServiceCollection _services, IConfiguration _configuration) {
            new InjectPostgres().InjectAccountDb(_configuration, _services);
        }
        private void InjectAzurePostgres(IServiceCollection _services, IConfiguration _configuration) {
        }

        private void InjectAwsMySql(IServiceCollection _services) {
            throw new NotImplementedException();
        }

        private void InjectAwsDynamo(IServiceCollection _services) {
            // AmazonInjectionBase.InjectBaseServices(_services);
            // _services.AddSingleton<IDataAccess, DynamoDbAccountRepository>();
        }
    }
}
