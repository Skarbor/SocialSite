using Domain.Abstract;
using Domain.Concrete;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SocialSite.Controllers
{
    public class FriendsController : Controller
    {
        IUserRepository userRepository = new UserRepository();

        [HttpGet]
        public void SendInvitationToFriends(string userId)
        {
            userRepository.SendInvitationToFriends(User.GetUserId(), userId);
        }

        //public void AcceptInvitationToFriends(int invitationId)
        //{
        //    userRepository.
        //}
    }
}
