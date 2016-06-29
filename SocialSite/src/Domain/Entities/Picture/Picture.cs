using Domain.Entities.User;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Picture
{
    public class Picture
    {
        public int Id { get; set; }
        public byte[] PictureBytes { get; set; }
        public DateTime Date { get; set; }

        public virtual UserPicture UserPicture { get; set; }
        public int UserPictureId { get; set; }

        [NotMapped]
        public string PhotoString
        {
            get
            {
                if (PictureBytes!=null)
                {
                    return "data:image/png;base64," + Convert.ToBase64String(PictureBytes);
                }
                else return null;
            }
        }
    }
}
