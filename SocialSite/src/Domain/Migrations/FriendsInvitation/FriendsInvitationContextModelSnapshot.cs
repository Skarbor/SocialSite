using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Domain.Context;

namespace Domain.Migrations.FriendsInvitation
{
    [DbContext(typeof(FriendsInvitationContext))]
    partial class FriendsInvitationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.Friendship.FriendsInvitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Accepted");

                    b.Property<DateTime?>("AcceptedDate");

                    b.Property<DateTime>("Date");

                    b.Property<string>("WhoHadBeenInvId");

                    b.Property<string>("WhoInvId");

                    b.HasKey("Id");
                });
        }
    }
}
