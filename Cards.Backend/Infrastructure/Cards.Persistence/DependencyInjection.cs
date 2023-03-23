using Cards.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cards.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<CardsDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<ICardsDbContext>(provider =>
                provider.GetService<CardsDbContext>());
            return services;
        }
    }
}
