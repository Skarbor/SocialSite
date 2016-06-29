using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.User;
using Microsoft.Data.Entity;
using Domain.Entities.Picture;
using Domain.Entities.Post;

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
            return dbContext.UserPictures.Where(x => x.ApplicationUserId == userId).Include(x=>x.Picture).ToList();
        }

        public UserPicture GetUserPictureById(int pictureId)
        {
            var picture = dbContext.UserPictures.Where(x => x.Id == pictureId).Include(x => x.Picture).Include(x=>x.Comments).FirstOrDefault();

            foreach (var comment in picture.Comments)
            {
                dbContext.Comments.Where(x => x.Id == comment.Id).Include(x => x.User).FirstOrDefault();
            }

            return picture;
        }

        public void SavePicture(UserPicture userPicture)
        {
            dbContext.Add(userPicture.Picture);
            dbContext.Add(userPicture);
            dbContext.SaveChanges();
        }

        public void SetProfilePictureById(int pictureId)
        {
            var oldProfilePicture = (from p in dbContext.UserPictures where p.IsProfilPicture == true select p).SingleOrDefault();

            if (oldProfilePicture != null)
            {
                oldProfilePicture.IsProfilPicture = false;
            }

            var newProfilePicture = (from p in dbContext.UserPictures where p.Id == pictureId select p).SingleOrDefault();

            if (newProfilePicture == null) throw new ArgumentException("Nie znaleziono zdjęcia o id: " + pictureId);

            newProfilePicture.IsProfilPicture = true;

            dbContext.SaveChanges();
        }

        public Comment AddCommentToPicture(int pictureId, string commentText, string userId)
        {
            UserPicture userPicture = dbContext.UserPictures.Where(x => x.Id == pictureId).Include(x => x.Picture).FirstOrDefault();

            Comment comment = new Comment();
            comment.Date = DateTime.Now;
            comment.Text = commentText;
            comment.User = dbContext.Users.Where(x => x.Id == userId).FirstOrDefault();
            userPicture.Comments.Add(comment);

            dbContext.SaveChanges();

            return comment;
        }
    }
}
