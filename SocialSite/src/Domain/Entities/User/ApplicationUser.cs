using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities.User;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Friendship;

namespace Domain.Entities.User
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        IUserRepository userRepository = new UserRepository();

        [NotMapped]
        public virtual ICollection<ApplicationUser> Friends { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<UserPicture> Pictures { get; set; }

        [NotMapped]
        public UserPicture ProfilPicture
        {
            get
            {
                if (Pictures == null) return null;

                foreach (UserPicture picture in Pictures)
                {
                    if (picture.IsProfilPicture)
                    {
                        return picture;
                    }
                }
                return null;
            }
        }

        public ApplicationUser()
        {
            Friends = new List<ApplicationUser>();
            Pictures = new List<UserPicture>();
        }

        public RelationshipBetweenUsers GetRelationshipWithUser(string userId)
        {
            return userRepository.GetRelationshipBetweenUsers(userId, this.Id);
        }

        public IEnumerable<FriendsInvitation> GetFriendsInvitations()
        {
            return userRepository.GetInvitationForUser(this.Id);
        }

        public bool HasProfilePicture()
        {
            foreach (UserPicture picture in Pictures)
            {
                if (picture.IsProfilPicture)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
