using Domain.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface ICommunicatorRepository
    {
        int SendMessage(string sendingUserId, string receivingUserId, string message);
        IEnumerable<Message> ReceiveLastXMessagesFromUser(string receivingUserId, string userWhoSendId, int numberOfMessagesToReceive);
        IEnumerable<Message> ReceiveMessagesSendedAfterLastReceivedMessageByItsIdFromUser(string receivingUserId, string userWhoSendId, int lastReceivedMessageId);
        IEnumerable<Message> ReceiveNotReadedMessages(string receivingUserId);
        IEnumerable<Message> ReceiveLastXMessagesFromConversation(string receivingUserId, string userWhoSendId, int numberOfMessagesToReceive);
    }
}
