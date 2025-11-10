using Domain.Interfaces.v1.WorkTimer;
using Infrastructure.Data.Mongo.Repositories.v1.WorkTimer;

namespace API.Infrastructure.IoC;
public static class Bootstrapper
{
    public static IServiceCollection Inject(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        });

        services.AddSingleton<IWorkTimerRepository>(sp =>
        {
            return new WorkTimerRepository("workTimers");
        });

        return services;
    }
}
