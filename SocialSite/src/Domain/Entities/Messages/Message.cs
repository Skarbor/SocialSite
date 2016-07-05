using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.Messages
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserWhoSendId { get; set; }
        public string UserWhoReceivedId { get; set; }
        public DateTime DateTime { get; set; }
        public bool Read { get; set; }

    }
}
