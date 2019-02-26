using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Models;
using System.Web.Security;
using DAL.Repositories;
using SecondHandStoreASPNET_Project.Helpers;
using Models.ViewModels;

namespace SecondHandStoreASPNET_Project.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users

        private DbManager _dbManager { get; set; }

        public UsersController()
        {
            _dbManager = new DbManager();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName, string password)
        {
            var CheckIfUserExists = _dbManager.UserRepository.UserLogin(userName, password);

            if (CheckIfUserExists == null)
            {
                ModelState.AddModelError("UserName", "User not does not exist in our database, please check and try again!");
                return View();
            }

            
            FormsAuthentication.SetAuthCookie(userName, true);
            ViewBag.Success = "Welcome, Registerd User!";

            return RedirectToAction("Index", "Video");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var CheckIfUserExists = _dbManager.UserRepository.GetUserByID(user.UserID);
            if (CheckIfUserExists != null)
            {
                ModelState.AddModelError("UserName", "Username already exists");
                return View(user);
            }

            _dbManager.UserRepository.InsertUser(user);
            _dbManager.Save();

            FormsAuthentication.SetAuthCookie(user.Username, true);
            return RedirectToAction("Index", "Video");
        }

        [Authorize]
        public ActionResult Edit()
        {
            System.Web.HttpContext currentContext = System.Web.HttpContext.Current;

            var user = currentContext.User.Identity.Name;
            var fullUser = _dbManager.UserRepository.GetUserByName(user);

            return View(fullUser);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            ModelState.Remove("User.Identity.Name");

           // User _user = _dbManager.UserRepository.GetUserByName(user.Username);
            _dbManager.UserRepository.UpdateUser(user);
            return RedirectToAction("Index", "Video");

        }

        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

       

        
    }
}