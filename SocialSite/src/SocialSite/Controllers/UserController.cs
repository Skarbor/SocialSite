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
        IPicturesRepository pictureRepository = new PicturesRepository();

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
            List<UserPicture> pictures = pictureRepository.GetPicturesForUser(userId);

            return View(pictures);
        }

        [HttpPost]
        public JsonResult AddPicture(IFormFile file)
        {
            UserPicture userPicture = new UserPicture();
            userPicture.UserId = User.GetUserId();
            userPicture.Picture.Date = DateTime.Now;

            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                userPicture.Picture.PictureBytes = binaryReader.ReadBytes((int)file.Length);
            }

            pictureRepository.SavePicture(userPicture);

            return Json(new { Status = "Ok" });
        }

        public ActionResult DisplaySinglePicture(int pictureId)
        {
            UserPicture picture = pictureRepository.GetPictureById(pictureId);

            return View(picture);
        }


        private static string GetFileName(IFormFile file) => file.ContentDisposition.Split(';')
                                                                .Select(x => x.Trim())
                                                                .Where(x => x.StartsWith("filename="))
                                                                .Select(x => x.Substring(9).Trim('"'))
                                                                .First();

    }
}
