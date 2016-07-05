using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities.User;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialSite.ViewComponents
{
    public class ConversationViewComponent : ViewComponent
    {
        IUserRepository userRepository = new UserRepository();

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            ApplicationUser user =  userRepository.GetUser(userId);
            return View(user.FirstName);
        }
    }
}
