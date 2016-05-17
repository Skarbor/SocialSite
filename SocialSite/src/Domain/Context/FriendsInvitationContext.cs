using Domain.Entities.Friendship;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Context
{
    public class FriendsInvitationContext :DbContext
    {
        public DbSet<FriendsInvitation> FriendsInvitations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet5-SocialSite-978f91d1-06e0-4edf-82dd-a85845f37b36;Trusted_Connection=True;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
