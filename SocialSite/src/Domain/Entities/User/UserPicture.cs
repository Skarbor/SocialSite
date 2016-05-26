using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public byte[] Picture { get; set; }

        [NotMapped]
        public string PhotoString
        {
            get
            {
                return "data:image/png;base64," + Convert.ToBase64String(Picture);
            }
        }
    }
}
