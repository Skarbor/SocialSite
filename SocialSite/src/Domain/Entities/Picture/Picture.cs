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

        [NotMapped]
        public string PhotoString
        {
            get
            {
                return "data:image/png;base64," + Convert.ToBase64String(PictureBytes);
            }
        }
    }
}
