using Domain.Entities.Picture;
using Domain.Entities.Post;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.User
{
    public class UserPicture
    {
        public int Id { get; set; }
        public string PictureName { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        public string PictureDescription { get; set; }
        public bool IsProfilPicture { get; set; }
        public bool IsInBackgroundPicture { get; set; }

        public virtual Picture.Picture Picture { get; set; }
        public int PictureId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public UserPicture()
        {
            Picture = new Entities.Picture.Picture();
            Comments = new List<Comment>();
        }
    }
}
