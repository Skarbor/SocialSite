using Domain.Entities.Picture;
using Domain.Entities.Post;
using System.Collections.Generic;

namespace Domain.Entities.User
{
    public class UserPicture
    {
        public int Id { get; set; }
        public string PictureName { get; set; }
        public string UserId { get; set; }
        public string PictureDescription { get; set; }
        public bool IsProfilPicture { get; set; }
        public bool IsInBackgroundPicture { get; set; }

        public virtual Picture.Picture Picture { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public UserPicture()
        {
            Picture = new Entities.Picture.Picture();
            Comments = new List<Comment>();
        }
    }
}
