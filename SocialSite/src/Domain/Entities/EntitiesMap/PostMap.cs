using Microsoft.Data.Entity.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.EntitiesMap
{
    public class PostMap 
    {
        public PostMap(EntityTypeBuilder<Post.Post> entityBuilder)
        {
            entityBuilder.HasMany(x => x.Comments).WithOne(x => x.Post).HasForeignKey(x => x.PostId).IsRequired(false);
        }
    }
}
