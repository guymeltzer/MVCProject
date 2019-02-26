using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DAL.Repositories
{
    public interface IUserRepositry
    {
        IEnumerable<User> GetUsers();

        User GetUserByID(int userID);

        void InsertUser(User user);

        void UpdateUser(User user);

        void DeleteUser(int userID);

        User UserLogin(string UserName, string Password);

        User GetUserByName(string name);
    }
}
