using Domain.Abstract;
using Domain.Context;
using Domain.Entities.Friendship;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class UserRepository : IUserRepository
    {
        ApplicationDbContext applicationDbContext;
        FriendsInvitationContext friendsInvitationContext;

        public UserRepository()
        {
            applicationDbContext = new ApplicationDbContext();
            friendsInvitationContext = new FriendsInvitationContext();
        }


        public ApplicationUser GetUser(string userId)
        {
            return applicationDbContext.Users.Where(p => p.Id == userId).FirstOrDefault();
        }

        public List<ApplicationUser> GetUsers(string userNamePattern)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();

            foreach (ApplicationUser user in applicationDbContext.Users)
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
                friendsInvitationContext.FriendsInvitations.Add(friendsinvitation);

                friendsInvitationContext.SaveChanges();
            }
        }

        public IEnumerable<FriendsInvitation> GetInvitationForUser(string userId)
        {
            return friendsInvitationContext.FriendsInvitations.Where(p => p.WhoHadBeenInvId == userId).AsEnumerable();
        }

        public RelationshipBetweenUsers GetRelationshipBetweenUsers(string firstUserId, string secondUserId)
        {
            ApplicationUser firstUser = applicationDbContext.Users.Where(p => p.Id == firstUserId).FirstOrDefault();
            ApplicationUser secondUser = applicationDbContext.Users.Where(p => p.Id == secondUserId).FirstOrDefault();

            if (firstUser.Friends.Contains(secondUser))
            {
                return RelationshipBetweenUsers.Friends;
            }
            else
            {
                FriendsInvitation invitationOne = (from p in friendsInvitationContext.FriendsInvitations where p.WhoInvId == firstUserId && p.WhoHadBeenInvId == secondUserId select p).FirstOrDefault();
                if (invitationOne != null) return RelationshipBetweenUsers.SendedInvitationToFriends;

                FriendsInvitation invitationTwo = (from p in friendsInvitationContext.FriendsInvitations where p.WhoInvId == secondUserId && p.WhoHadBeenInvId == firstUserId select p).FirstOrDefault();
                if (invitationOne != null) return RelationshipBetweenUsers.ReceivedInvitationToFriends;

                return RelationshipBetweenUsers.NoRelationship;
            }

        }
    }
}
