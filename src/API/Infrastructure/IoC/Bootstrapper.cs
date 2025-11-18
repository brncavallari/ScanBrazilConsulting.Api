namespace API.Infrastructure.IoC;
public static class Bootstrapper
{
    public static IServiceCollection Inject(this IServiceCollection services, IConfiguration configuration)
    {
        InjectMediator(services);
        InjectRepositories(services);
        InjectServiceClients(services, configuration);

        return services;
    }

    public static void InjectMediator(IServiceCollection services)
    {
        var assemblies = new Assembly[]
        {
            typeof(UploadWorkTimerImportedCommandHandler).Assembly,
            typeof(UpdateUserTimerCommandHandler).Assembly,
            typeof(GetUserTimerByEmailQueryHandler).Assembly
        };

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                assemblies
            );
        });
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

    public static void InjectServiceClients(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MicrosoftSettings>(
            configuration.GetSection("MicrosoftSettings")
        );

        services.AddHttpClient<IMicrosoftServiceClient, MicrosoftServiceClient>((sp, client) =>
        {
            var msSettings = sp.GetRequiredService<IOptions<MicrosoftSettings>>().Value;

            client.BaseAddress = new Uri(msSettings.Url);
        });
    }
}