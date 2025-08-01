using Microsoft.Extensions.DependencyInjection;
using TaskMaster.Core.Interfaces;
using TaskMaster.Infrastructure.Repositories;

namespace TaskMaster.Infrastructure.Configuration
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();
            return services;
        }
    }
}