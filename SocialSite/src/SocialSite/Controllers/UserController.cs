using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities.User;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using System.IO;
using Microsoft.AspNet.Http;

namespace SocialSite.Controllers
{
    [Authorize]
    public class UserController:Controller
    {
        IUserRepository userRepository = new UserRepository();

        public ViewResult DisplayUserProfile()
        {
            ApplicationUser user = userRepository.GetUser(User.GetUserId());
            return View(user);
        }

        public ActionResult DisplayUsers(string userNamePattern)
        {
            List<ApplicationUser> users = userRepository.GetUsers(userNamePattern);
            users.Remove((from p in users where p.Id == User.GetUserId() select p).FirstOrDefault());
            return View(users);
        }

        [HttpPost]
        public ActionResult DisplayUserPartial(string userId)
        {
            ApplicationUser user = userRepository.GetUser(userId);
            if (user != null)
            {
                return PartialView("DisplaySingleUserPartial", user);
            }
                                    
            else return RedirectToAction("Index", "Home");

        }

        public ActionResult DisplayUserPictures(string userId)
        {
            List<UserPicture> pictures = new List<UserPicture>();

            return View(pictures);
        }

        [HttpPost]
        public JsonResult AddPicture(IFormFile file)
        {
            if (file.Length > 0)
            {
                var fileName = GetFileName(file);
                var savePath = Path.Combine("C:\\SERVER_FILES\\", fileName);

                file.SaveAs(savePath);
                return Json(new { Status = "Ok" });
            }
            return Json(new { Status = "Error" });
        }
        private static string GetFileName(IFormFile file) => file.ContentDisposition.Split(';')
                                                                .Select(x => x.Trim())
                                                                .Where(x => x.StartsWith("filename="))
                                                                .Select(x => x.Substring(9).Trim('"'))
                                                                .First();

    }
}
