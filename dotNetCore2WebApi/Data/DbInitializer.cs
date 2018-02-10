using System;
using System.Linq;
using dotNetCore2WebApi.Entities;

namespace dotNetCore2WebApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BlogDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{
                FirstName = "Bob",
                LastName = "Marley",
                Email = "BobMarley@gmail.com",
                Password = "mcHcEmFRaJaCjbRBJibTGLZFFOV8BZ3HGJmd3kbvBMc=",
                PasswordSalt = "b6c258d3af7248bdb044c1bdff68ace4",
                IsSuperAdmin = true
                }
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            
            context.SaveChanges();

            var posts = new Post[]
            {
            new Post{
                Title = "Hello World",
                Summary = "Summary",
                Content = "Content",
                IsPublished = true,
                CreateDate = DateTime.Now,
                UserId = context.Users.FirstOrDefault().Id
                }
            };
            foreach (Post p in posts)
            {
                context.Posts.Add(p);
            }
            context.SaveChanges();
        }
    }
}