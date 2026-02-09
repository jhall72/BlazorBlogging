using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace BlazorBlogging.Shared.Data
{
    public class BlogDbContext : DbContext
    {
        public DbSet<BlogPost> Posts { get; set; }
        public DbSet<PostVersion> PostVersions { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogPost>().ToCollection("posts");
            modelBuilder.Entity<PostVersion>().ToCollection("versions");
        }
    }
}
