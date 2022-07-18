using Microsoft.EntityFrameworkCore;

namespace PlatformService.Data
{
    public static class DataSeed
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder, bool isProd)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not run migrations: {ex.Message}");
                }
            }
            else
            {
                if (!context.Platforms.Any())
                {
                    Console.WriteLine("Seeding Data...");
                    context.Platforms.AddRange(
                    new Models.Platform
                    {
                        Name = ".NET",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Models.Platform
                    {
                        Name = "SQL Server Express",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Models.Platform
                    {
                        Name = "Kubernetes",
                        Publisher = "Cloud Native Computing Platform",
                        Cost = "Free"
                    }
                   );
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("We already have data");
                }
            }

        }
    }
}
