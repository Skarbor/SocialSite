using Domain.Entities.User;
using Microsoft.Data.Entity.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.EntitiesMap
{
    public class UserPictureMap
    {
        public UserPictureMap(EntityTypeBuilder<UserPicture> entityBuilder)
        {
            entityBuilder.HasMany(x => x.Comments).WithOne(x => x.UserPicture).IsRequired(false).HasForeignKey(x => x.UserPictureid).IsRequired(false);
        }
    }
}
