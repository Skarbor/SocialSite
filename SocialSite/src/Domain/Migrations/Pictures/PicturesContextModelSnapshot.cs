using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Domain.Context;

namespace Domain.Migrations.Pictures
{
    [DbContext(typeof(PicturesContext))]
    partial class PicturesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.User.UserPicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsInBackgroundPicture");

                    b.Property<bool>("IsProfilPicture");

                    b.Property<byte[]>("Picture");

                    b.Property<string>("PictureDescription");

                    b.Property<string>("PictureName");

                    b.Property<string>("UserId");

                    b.HasKey("Id");
                });
        }
    }
}
