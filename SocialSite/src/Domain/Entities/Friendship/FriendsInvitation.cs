using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.Friendship
{
    public class FriendsInvitation
    {
        public int Id { get; set; }
        public string WhoInvId { get; set; }
        public string WhoHadBeenInvId { get; set; }
        public DateTime Date { get; set; }
        public bool Accepted { get; set; }
        public DateTime? AcceptedDate { get; set; }
    }
}
