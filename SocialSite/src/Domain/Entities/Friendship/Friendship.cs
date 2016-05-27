using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.Friendship
{
    public class Friendship
    {
        public int Id { get; set; }
        public string FirstUser { get; set; }
        public string SecondUser { get; set; }
        public DateTime FriendshipDate { get; set; }
    }
}
