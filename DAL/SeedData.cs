using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DAL
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                context.Database.EnsureCreated();

                if (context.Users.Any())
                {
                    return;
                }
            }
        }
    }
}