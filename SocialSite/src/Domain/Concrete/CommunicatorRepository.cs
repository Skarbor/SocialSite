using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Messages;
using Domain.Entities.User;
using Domain.Communicator;

namespace Domain.Concrete
{
    public class CommunicatorRepository : ICommunicatorRepository
    {
        ApplicationDbContext dbContext;

        public CommunicatorRepository()
        {
            dbContext = new ApplicationDbContext();
        }

        public IEnumerable<Message> ReceiveLastXMessagesFromUser(string receivingUserId, string userWhoSendId, int numberOfMessagesToReceive)
        {
            return dbContext.Messages.Where(x => x.UserWhoReceivedId == receivingUserId && x.UserWhoSendId == userWhoSendId).OrderByDescending(x => x.DateTime).Take(numberOfMessagesToReceive).AsEnumerable().OrderBy(x => x.DateTime);
        }

        public IEnumerable<Message> ReceiveMessagesSendedAfterLastReceivedMessageByItsIdFromUser(string receivingUserId, string userWhoSendId, int lastReceivedMessageId)
        {
            Message msg = dbContext.Messages.Where(x => x.Id == lastReceivedMessageId).Single();

            return dbContext.Messages.Where(x => x.UserWhoReceivedId == receivingUserId && x.UserWhoSendId == userWhoSendId && x.DateTime > msg.DateTime).AsEnumerable();
        }

        public IEnumerable<Message> ReceiveNotReadedMessages(string receivingUserId)
        {
            return dbContext.Messages.Where(x => x.UserWhoReceivedId == receivingUserId && x.Read == false).AsEnumerable();
        }

        public IEnumerable<Message> ReceiveLastXMessagesFromConversation(string receivingUserId, string userWhoSendId, int numberOfMessagesToReceive)
        {
            return dbContext.Messages.Where(x => (x.UserWhoReceivedId == receivingUserId || x.UserWhoReceivedId == userWhoSendId) &&
                                                 (x.UserWhoSendId == receivingUserId || x.UserWhoSendId == userWhoSendId))
                                                 .OrderByDescending(x => x.DateTime)
                                                 .Take(numberOfMessagesToReceive)
                                                 .AsEnumerable()
                                                 .OrderBy(x => x.DateTime);
        }

        public int SendMessage(string sendingUserId, string receivingUserId, string message)
        {
            Message msg = new Message();
            msg.UserWhoSendId = sendingUserId;
            msg.UserWhoReceivedId = receivingUserId;
            msg.Content = message;
            msg.DateTime = DateTime.Now;
            msg.Read = false;

            dbContext.Messages.Add(msg);
            dbContext.SaveChanges();

            return msg.Id;
        }
    }
}
