namespace General.Utility.CQRS.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the CQRS dispatchers and all command/query handlers
    /// from the specified assembly.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IServiceCollection AddCqrsDispatchers(this IServiceCollection services, Assembly assembly)
    {
        // Register the dispatcher
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();

        // Scan the assembly for all handlers and register them
        var handlers = assembly.GetTypes()
            .Where(t => t.GetInterfaces()
            .Any(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(ICommandHandler<>) || i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))).ToList();

        foreach (var handler in handlers)
        {
            var interfaces = handler.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                // Register the handler with its interface
                services.AddScoped(@interface, handler);
            }
        }

        return services;
    }
}
