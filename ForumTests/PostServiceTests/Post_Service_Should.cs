using BLL.Services;
using DAL;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace ForumTests.PostServiceTests
{
    [TestFixture]
    public class Post_Service_Should
    {
        [TestCase("Example", 4)]
        [TestCase("sql injection", 2)]
        [TestCase("Harry Potter", 0)]
        public void Search_Results_Corresponding_To_Query(string query, int expectedCount)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using (var context = new AppDbContext(options))
            {
                context.Posts.Add(new DAL.Entities.Post
                {
                    Id = 1657,
                    UserId = "testUserId",
                    Content = "Content with examples...",
                    Description = "SQL Injection"
                });

                context.Posts.Add(new DAL.Entities.Post
                {
                    Id = 5678,
                    UserId = "testUserId",
                    Content = "Content about SQL injection...",
                    Description = "Description"
                });

                context.Posts.Add(new DAL.Entities.Post
                {
                    Id = 5650,
                    UserId = "testUserId",
                    Content = "Content about working with LINQ. Example: ...",
                    Description = "LINQ"
                });

                context.Posts.Add(new DAL.Entities.Post
                {
                    Id = 178,
                    UserId = "testUserId",
                    Content = "Another content about working with LINQ with examples",
                    Description = "LINQ"
                });

                context.Posts.Add(new DAL.Entities.Post
                {
                    Id = 453,
                    UserId = "testUserId",
                    Content = "Let's see how to create a simple C# example:",
                    Description = "Example: simple \"hello world\" program"
                });

                context.SaveChanges();

                var postService = new PostService(context);
                int resultCount = postService.GetPostsGlobalSearch(query).Count();

                Assert.AreEqual(expectedCount, resultCount);
            }

            //using (context)
            //{
                
            //}
        }
    }
}