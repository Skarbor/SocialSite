using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.Post
{
    public class Comment
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }

        public virtual Post Post { get; set; }
        public int? PostId { get; set; }

        public virtual UserPicture UserPicture { get; set; }
        public int? UserPictureid { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Comment()
        {
            Comments = new List<Comment>();
        }
    }
}