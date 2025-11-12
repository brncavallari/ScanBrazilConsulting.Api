using Infrastructure.Data.Query.Queries.v1.GetUserTimerByEmail;

namespace API.Infrastructure.IoC;
public static class Bootstrapper
{
    public static IServiceCollection Inject(this IServiceCollection services)
    {
        InjectMediator(services);
        InjectRepositories(services);
        InjectServiceClients(services);

        return services;
    }

    public static void InjectMediator(IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                AppDomain.CurrentDomain.GetAssemblies()
            );
        });

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
            typeof(GetUserTimerByEmailQueryHandler).Assembly)
        );
    }
    public static void InjectRepositories(IServiceCollection services)
    {
        services.AddSingleton<IWorkTimerRepository>(sp =>
        {
            return new WorkTimerRepository("workTimers");
        });

        services.AddSingleton<IWorkTimerImportedRepository>(sp =>
        {
            return new WorkTimerImportedRepository("workTimersImported");
        });

        services.AddSingleton<IUserTimerRepository>(sp =>
        {
            return new UserTimerRepository("userTimers");
        });
    }
    public static void InjectServiceClients(IServiceCollection services)
    {
        services.AddHttpClient<IMicrosoftServiceClient, MicrosoftServiceClient>(client =>
        {
            client.BaseAddress = new Uri("https://graph.microsoft.com/");
        });
    }
}