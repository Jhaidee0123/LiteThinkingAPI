using LiteThinking.Domain.Ports.Repositories;
using LiteThinking.Infrastructure.Authentication;
using LiteThinking.Infrastructure.Helpers;
using LiteThinking.Infrastructure.Repositories;

namespace LiteThinking.Api.Extensions;

public static class DependenciesExtension
{
    public static IServiceCollection AddDependenciesExtension(this IServiceCollection services, IConfiguration configuration)
    {
        // Repositories
        services.AddScoped<IUserAuthenticationRepository, UserAuthenticationRepository>();
        services.AddScoped<IPdfGenerator, PdfGenerator>();
        services.AddScoped<ISendGridClientService, SendGridClientService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
