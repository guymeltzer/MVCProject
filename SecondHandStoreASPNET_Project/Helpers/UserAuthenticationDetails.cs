using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Models;
using Models.ViewModels;
using DAL.Repositories;

namespace SecondHandStoreASPNET_Project.Helpers
{
    public class UserAuthenticationDetails
    {
        private DbManager _dbManager { get; set; }

        public UserAuthenticationDetails()
        {
            _dbManager = new DbManager();
        }
        public UserConnectViewModel GetUserCredentials()
        {
            UserConnectViewModel user = new UserConnectViewModel(HttpContext.Current.User.Identity.Name, HttpContext.Current.User.Identity.IsAuthenticated);

            return user;
        }

        public User getFullUser()
        {

            var fullUser = _dbManager.UserRepository.GetUserByName(HttpContext.Current.User.Identity.Name);

                return fullUser;
        }
    }
}