using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.User;
using Domain.Context;

namespace Domain.Concrete
{
    public class PicturesRepository : IPicturesRepository
    {
        PicturesContext picturesContext;

        public PicturesRepository()
        {
            picturesContext = new PicturesContext();
        }

        public List<UserPicture> GetPicturesForUser(string userId)
        {
            return picturesContext.UserPictures.Where(x => x.UserId == userId).ToList();
        }


    }
}
