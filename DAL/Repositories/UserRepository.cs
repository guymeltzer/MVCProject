using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Models.Models;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepositry
    {
        private VideosDBContext _context;

        public UserRepository(VideosDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserByID(int userID)
        {
            return _context.Users.FirstOrDefault(u => u.UserID == userID);
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            var current = _context.Users.Where(u => u.Username == user.Username).Select(u => u).FirstOrDefault();
            
                current.Password = user.Password;
                current.FirstName = user.FirstName;
                current.LastName = user.LastName;
                current.ConfirmPassword = user.ConfirmPassword;
                current.BirthDate = user.BirthDate;
                current.Email = user.Email;
                current.Address = user.Address;

            _context.SaveChanges();
        }

        public void DeleteUser(int userID)
        {
            var current = _context.Users.FirstOrDefault(u => u.UserID == userID);
            if (current != null)
            {
                _context.Users.Remove(current);
                return;
            }

            throw new NullReferenceException();
        }

        public User UserLogin(string UserName, string Password)
        {
            var userInDB = _context.Users.FirstOrDefault(u => u.Username == UserName && u.Password == Password);

            return userInDB;
        }

        public User GetUserByName(string name)
        {
            var userInDB = _context.Users.FirstOrDefault(u => u.Username == name);
            return userInDB;
        }

        public User GetCurrentUser()
        {
                var fullUser = GetUserByName(HttpContext.Current.User.Identity.Name);

                return fullUser;
        }

    }
}
