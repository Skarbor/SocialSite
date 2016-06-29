using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Domain.Entities.Friendship;
using System.Reflection.Emit;
using Domain.Entities.EntitiesMap;

namespace Domain.Entities.User
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<FriendsInvitation> FriendsInvitations { get; set; }
        public DbSet<UserPicture> UserPictures { get; set; }
        public DbSet<Picture.Picture> Pictures { get; set; }
        public DbSet<Friendship.Friendship> Friendships { get; set; }
        public DbSet<Post.Post> Posts { get; set; }
        public DbSet<Post.Comment> Comments { get; set; }

        public ApplicationDbContext() : base()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            new PostMap(builder.Entity<Post.Post>());

            //builder.Entity<ApplicationUser>().HasMany(p => p.Pictures).WithOne(p => p.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet5-SocialSite-978f91d1-06e0-4edf-82dd-a85845f37b36;Trusted_Connection=True;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
