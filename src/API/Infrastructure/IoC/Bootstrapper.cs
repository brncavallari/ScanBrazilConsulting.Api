using API.Context.v1;
using Domain.Commands.v1.TimeOff.Create;
using Domain.Commands.v1.TimeOff.Reject;
using Domain.Interfaces.v1.Context;
using Domain.Interfaces.v1.Repositories.TimeOff;
using Domain.Interfaces.v1.Repositories.UserTimer;
using Domain.Interfaces.v1.Repositories.WorkTimer;
using Domain.Interfaces.v1.Repositories.WorkTimerImported;
using Infrastructure.Data.Mongo.Repositories.v1.TimeOff;
using Infrastructure.Data.Query.Queries.v1.TimeoOff.GetAllTimeOff;
using Infrastructure.Data.Query.Queries.v1.TimeoOff.GetTimeOffByProtocol;
using Infrastructure.Data.Query.Queries.v1.UserTimer.GetAllUserTimer;
using Infrastructure.Data.Query.Queries.v1.UserTimer.GetUserTimerByEmail;
using Infrastructure.Data.Query.Queries.v1.WorkTimer.GetAllWorkTimersImported;
using Infrastructure.Service.Interfaces.v1.Smtp;
using Infrastructure.Service.ServiceHandlers.v1.Smtp;

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
            typeof(CreateTimeOffCommandHandler).Assembly,
            typeof(RejectTimeOffCommandHandler).Assembly,


            typeof(GetUserTimerByEmailQueryHandler).Assembly,
            typeof(GetAllTimeOffQueryHandler).Assembly,
            typeof(GetTimeOffByProtocolQueryHandler).Assembly,
            typeof(GetAllUserTimerQueryHandler).Assembly,
            typeof(GetAllWorkTimersImportedQueryHandler).Assembly,

            typeof(DeleteWorkTimerImportedCommandHandler).Assembly,
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

        services.AddSingleton<ITimeOffRepository>(sp =>
        {
            return new TimeOffRepository("timeOffs");
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

        services.AddScoped<ISmtpServiceClient, SmtpServiceClient>();
        services.AddScoped<IUserContext, UserContext>();
    }
}