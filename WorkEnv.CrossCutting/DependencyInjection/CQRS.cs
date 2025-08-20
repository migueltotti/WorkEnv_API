using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WorkEnv.Application.CQRS.Event.Query.GetById;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class CQRS
{
    public static IServiceCollection AddCQRS(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblyContaining<GetByIdQueryHandler>());
        
        return services;
    }
}