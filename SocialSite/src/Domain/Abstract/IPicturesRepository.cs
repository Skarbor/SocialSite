using Domain.Entities.Post;
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
        UserPicture GetUserPictureById(int pictureId);

        void SavePicture(UserPicture userPicture);
        Comment AddCommentToPicture(int pictureId, string commentText, string userId);
    }
}
