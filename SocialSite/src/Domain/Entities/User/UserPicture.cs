using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
    public class UserPicture
    {
        public int Id { get; set; }
        public string PictureName { get; set; }
        public string UserId { get; set; }
        public string PictureDescription { get; set; }
        public DateTime Date { get; set; }
        public bool IsProfilPicture { get; set; }
        public bool IsInBackgroundPicture { get; set; }
        public string Path { get; set; }
    }
}
