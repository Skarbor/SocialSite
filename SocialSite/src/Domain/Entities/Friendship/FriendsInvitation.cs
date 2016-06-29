using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities.User;
using System;

namespace Domain.Entities.Friendship
{
    public class FriendsInvitation
    {
        IUserRepository userRepository = new UserRepository();

        public int Id { get; set; }
        public string WhoInvId { get; set; }
        public string WhoHadBeenInvId { get; set; }
        public DateTime Date { get; set; }
        public bool Accepted { get; set; }
        public DateTime? AcceptedDate { get; set; }

        public ApplicationUser GetUserWhoSendInvitation()
        {
            return userRepository.GetUser(WhoInvId);
        }

        public ApplicationUser GetUserWhoHadBenInvitated()
        {
            return userRepository.GetUser(WhoHadBeenInvId);
        }
    }
}
