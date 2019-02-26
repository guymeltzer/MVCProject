using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DbManager : IDisposable
    {
        private VideosDBContext _context = new VideosDBContext();
        private VideoRepository _videoRepository;
        private UserRepository _userRepository;

        public VideoRepository VideoRepository
        {
            get
            {
                if (_videoRepository == null)
                {
                    _videoRepository = new VideoRepository(_context);
                }
                return _videoRepository;
            }
        }

        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Close()
        {
            _context.Database.Connection.Close();
        }
    }
}
