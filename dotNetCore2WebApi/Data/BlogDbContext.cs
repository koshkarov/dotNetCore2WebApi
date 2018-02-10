using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetCore2WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotNetCore2WebApi.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
