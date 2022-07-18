﻿using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Repositories;

namespace PlatformService.Extensions
{
    public static class ServicesExtension
    {

        public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder)
        {

            if (builder.Environment.IsProduction())
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn"));
                });
            }
            else
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMem");
                });
            }

            services.AddSwaggerConfiguration();           
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IPlatformRepository, PlatformRepository>();
            return services;
        }
    }
}
