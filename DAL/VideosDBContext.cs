using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Models.Models;

namespace DAL
{
    public class VideosDBContext : DbContext
    {
        public VideosDBContext() : base("VideosDB")
        {

        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Video> Videos { get; set; }
    }
}
