using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.User;
using Microsoft.Data.Entity;
using Domain.Entities.Picture;

namespace Domain.Concrete
{
    public class PicturesRepository : IPicturesRepository
    {
        ApplicationDbContext dbContext;

        public PicturesRepository()
        {
            dbContext = new ApplicationDbContext();
        }

        public List<UserPicture> GetPicturesForUser(string userId)
        {
            return dbContext.UserPictures.Where(x => x.UserId == userId).Include(x=>x.Picture).ToList();
        }

        public UserPicture GetPictureById(int pictureId)
        {
            return dbContext.UserPictures.Where(x => x.Id == pictureId).Include(x => x.Picture).FirstOrDefault();
        }

        public void SavePicture(UserPicture userPicture)
        {
            dbContext.Add(userPicture.Picture);
            dbContext.Add(userPicture);
            dbContext.SaveChanges();
        }
    }
}
