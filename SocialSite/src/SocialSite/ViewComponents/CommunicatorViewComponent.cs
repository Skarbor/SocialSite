using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities.User;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;


namespace SocialSite.ViewComponents
{
    public class CommunicatorViewComponent : ViewComponent
    {
        IUserRepository userRepository = new UserRepository();

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            ICollection<ApplicationUser> friends = userRepository.GetFriends(userId); 
            return View(friends);
        }
    }
}
