using Microsoft.Extensions.DependencyInjection;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Repository;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class Repositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWorkSpaceRepository, WorkSpaceRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}