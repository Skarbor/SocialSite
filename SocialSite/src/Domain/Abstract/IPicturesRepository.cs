using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IPicturesRepository
    {
        List<UserPicture> GetPicturesForUser(string userId);
        UserPicture GetPictureById(int pictureId);

        void SavePicture(UserPicture userPicture);
    }
}
