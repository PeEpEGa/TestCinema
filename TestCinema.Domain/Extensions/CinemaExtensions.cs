using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestCinema.Domain.Commands;
using TestCinema.Domain.Database;

namespace TestCinema.Domain;

public static class CinemaExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services, Action<IServiceProvider, 
        DbContextOptionsBuilder> dbOptionsAction)
    {
        services.AddMediatR(o => o.RegisterServicesFromAssemblies(typeof(GetAllCinemasQuery).Assembly));
        services.AddDbContext<CinemaDbContext>(dbOptionsAction);
        return services;
    }
}