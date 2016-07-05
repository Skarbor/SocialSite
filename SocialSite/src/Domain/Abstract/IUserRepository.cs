using Domain.Entities.Friendship;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IUserRepository
    {
        ApplicationUser GetUser(string userId);
        List<ApplicationUser> GetUsers(string userNamePattern);
        void SendInvitationToFriends(string userSendingId, string userReceivingInvitationId);
        void AcceptInvitationToFriends(string userWhoAcceptInvitationId, string userWhoSendInvitationId);
        IEnumerable<FriendsInvitation> GetInvitationForUser(string userId);
        RelationshipBetweenUsers GetRelationshipBetweenUsers(string firstUserId, string secondUserId);
        ICollection<ApplicationUser> GetFriends(string userId);
    }
}
