using Domain.Abstract;
using Domain.Concrete;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System.Security.Claims;
using Domain.Entities.User;
using System.Collections.Generic;
using Domain.Entities.Messages;

namespace SocialSite.Controllers
{
    [Authorize]
    public class CommunicatorController: Controller
    {
        IUserRepository userRepository = new UserRepository();
        ICommunicatorRepository communicatorRepository = new CommunicatorRepository();

        public ActionResult GetFriendsListInComunicator()
        {
            ICollection<ApplicationUser> friends = userRepository.GetFriends(User.GetUserId());

            return PartialView("CommunicationFriendsList", friends);
        }

        [HttpGet]
        public JsonResult SendMessage(string receivingUserId, string message)
        {
            communicatorRepository.SendMessage(User.GetUserId(), receivingUserId, message);

            return Json("Ok");
        }

        public IEnumerable<Message> ReceiveLastXMessagesFromUser(string userWhoSendId, int numberOfMessagesToReceive)
        {
            return communicatorRepository.ReceiveLastXMessagesFromUser(User.GetUserId(), userWhoSendId, numberOfMessagesToReceive);
        }
        public IEnumerable<Message> ReceiveMessagesSendedAfterLastReceivedMessageByItsIdFromUser(string userWhoSendId, int lastReceivedMessageId)
        {
            return communicatorRepository.ReceiveMessagesSendedAfterLastReceivedMessageByItsIdFromUser(User.GetUserId(), userWhoSendId, lastReceivedMessageId);
        }
        IEnumerable<Message> ReceiveNotReadedMessages()
        {
            return communicatorRepository.ReceiveNotReadedMessages(User.GetUserId());
        }

        public IEnumerable<Message> ReceiveLastXMessagesFromConversation(string userWhoSendId, int numberOfMessagesToReceive)
        {
            return communicatorRepository.ReceiveLastXMessagesFromConversation(User.GetUserId(), userWhoSendId, numberOfMessagesToReceive);
        }

        public JsonResult GetUserProfilPicture(string userId = "")
        {
            ApplicationUser user;

            if (userId == "")
            {
                user = userRepository.GetUser(User.GetUserId());
            }
            user = userRepository.GetUser(userId);

            if (user.HasProfilePicture())
            {
                return new JsonResult(user.ProfilPicture.Picture.PhotoString);
            }
            return new JsonResult("");
        }
    }
}
