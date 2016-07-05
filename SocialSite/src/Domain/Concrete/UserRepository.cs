using Domain.Abstract;
using Domain.Entities.Friendship;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
namespace Domain.Concrete
{
    public class UserRepository : IUserRepository
    {
        ApplicationDbContext applicationDbContext;

        public UserRepository()
        {
            applicationDbContext = new ApplicationDbContext();
        }


        public ApplicationUser GetUser(string userId)
        {
            var user = applicationDbContext.Users.Where(p => p.Id == userId).Include(x => x.Pictures).ThenInclude(x=>x.Picture).FirstOrDefault();
            return user;
        }

        public List<ApplicationUser> GetUsers(string userNamePattern)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();

            foreach (ApplicationUser user in applicationDbContext.Users.Include(x=>x.Pictures).ThenInclude(x=>x.Picture))
            {
                if (!String.IsNullOrEmpty(userNamePattern))
                {
                    if (user.UserName.Contains(userNamePattern)) users.Add(user);
                }
                else users.Add(user);
            }

            return users;
        }

        public void SendInvitationToFriends(string userSendingId, string userReceivingInvitationId)
        {
            ApplicationUser sendingUser = applicationDbContext.Users.Where(p => p.Id == userSendingId).FirstOrDefault();
            ApplicationUser receivingUser = applicationDbContext.Users.Where(p => p.Id == userReceivingInvitationId).FirstOrDefault();

            if (sendingUser != null && receivingUser != null)
            {
                FriendsInvitation friendsinvitation = new FriendsInvitation()
                {
                    WhoInvId = sendingUser.Id,
                    WhoHadBeenInvId = receivingUser.Id,
                    Date = DateTime.Now
                };
                applicationDbContext.FriendsInvitations.Add(friendsinvitation);

                applicationDbContext.SaveChanges();
            }
        }

        public void AcceptInvitationToFriends(string userWhoAcceptInvitationId, string userWhoSendInvitationId)
        {
            FriendsInvitation invitation = applicationDbContext.FriendsInvitations.Where(p => p.WhoInvId == userWhoSendInvitationId && p.WhoHadBeenInvId == userWhoAcceptInvitationId).Single();

            if (invitation == null) throw new ArgumentException("Nie znaleziono zaproszenia");

            invitation.Accepted = true;
            invitation.AcceptedDate = DateTime.Now;

            ApplicationUser userWhoSendInvitation = invitation.GetUserWhoSendInvitation();
            ApplicationUser userWhoHadBeenInvitated = invitation.GetUserWhoHadBenInvitated();

            Friendship friendship = new Friendship();
            friendship.FirstUser = userWhoSendInvitation.Id;
            friendship.SecondUser = userWhoHadBeenInvitated.Id;
            friendship.FriendshipDate = DateTime.Now;

            applicationDbContext.Friendships.Add(friendship);

            applicationDbContext.SaveChanges();
        }

        public IEnumerable<FriendsInvitation> GetInvitationForUser(string userId)
        {
            return applicationDbContext.FriendsInvitations.Where(p => p.WhoHadBeenInvId == userId).AsEnumerable();
        }

        public RelationshipBetweenUsers GetRelationshipBetweenUsers(string firstUserId, string secondUserId)
        {
            ApplicationUser firstUser = applicationDbContext.Users.Where(p => p.Id == firstUserId).FirstOrDefault();
            ApplicationUser secondUser = applicationDbContext.Users.Where(p => p.Id == secondUserId).FirstOrDefault();

            if (GetFriends(firstUser.Id).Contains(secondUser))
            {
                return RelationshipBetweenUsers.Friends;
            }
            else
            {
                FriendsInvitation invitationOne = (from p in applicationDbContext.FriendsInvitations where p.WhoInvId == firstUserId && p.WhoHadBeenInvId == secondUserId select p).FirstOrDefault();
                if (invitationOne != null) return RelationshipBetweenUsers.SendedInvitationToFriends;

                FriendsInvitation invitationTwo = (from p in applicationDbContext.FriendsInvitations where p.WhoInvId == secondUserId && p.WhoHadBeenInvId == firstUserId select p).FirstOrDefault();
                if (invitationTwo != null) return RelationshipBetweenUsers.ReceivedInvitationToFriends;

                return RelationshipBetweenUsers.NoRelationship;
            }

        }

        public ICollection<ApplicationUser> GetFriends(string userId)
        {
            try
            {
                IEnumerable<Friendship> friendships = applicationDbContext.Friendships.Where(x => (x.FirstUser == userId || x.SecondUser == userId)).AsEnumerable();

                List<string> userIds = new List<string>();

                foreach (Friendship item in friendships)
                {
                    if (item.FirstUser != userId) userIds.Add(item.FirstUser);
                    else if (item.SecondUser != userId) userIds.Add(item.SecondUser);
                    else throw new ArgumentException("Zły format obiektu Friendship!");
                }

                List<ApplicationUser> users = new List<ApplicationUser>();

                foreach (var item in userIds)
                {
                    users.Add(GetUser(item));
                }

                return users;

                //user = applicationDbContext.Users.Where(x => x.Id == userId).Include(x => x.Friends).ThenInclude(x => x.Pictures).ThenInclude(x => x.Picture).Single();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
