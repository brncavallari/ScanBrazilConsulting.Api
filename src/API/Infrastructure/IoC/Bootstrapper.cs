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

        services.AddSingleton<IWorkTimerImportedRepository>(sp =>
        {
            return new WorkTimerImportedRepository("workTimersImported");
        });

        services.AddSingleton<IUserTimerRepository>(sp =>
        {
            return new UserTimerRepository("userTimers");
        });

        services.AddHttpClient<IMicrosoftServiceClient, MicrosoftServiceClient>(client =>
        {
            client.BaseAddress = new Uri("https://graph.microsoft.com/");
        });

        return services;
    }
}