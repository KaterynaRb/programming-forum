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
                    return;   // DB has been seeded
                }
                //context.Users.Add(new Entities.User {UserName = "Name", Email = "email@email", HashedPassword = "abc" });

                //context.Topics.Add(new Entities.Topic { Name = "C#"});
                //context.Topics.Add(new Entities.Topic { Name = "C++" });
                //context.Topics.Add(new Entities.Topic { Name = "SQL" });

                //context.Posts.Add(new Entities.Post { Description = "SQL 1", Content = "Content SQL 1", TopicId = 3, UserId = "christopher" });
                //context.Posts.Add(new Entities.Post { Description = "SQL 2", Content = "Content SQL 2", TopicId = 3, UserId = "miley" });

                //context.Posts.Add(new Entities.Post { Description = "C++ 1", Content = "Content C++ 1", TopicId = 2, UserId = "christopher" });
                //context.Posts.Add(new Entities.Post { Description = "C++ 2", Content = "Content C++ 2", TopicId = 2, UserId = "miley" });


                //context.SaveChanges();
            }
        }
    }
}