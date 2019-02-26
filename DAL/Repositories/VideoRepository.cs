using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DAL.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private VideosDBContext _context;

        public VideoRepository(VideosDBContext context)
        {
            this._context = context;
        }

        public Video GetVideoByID(int videoID)
        {
            return _context.Videos.Include(v => v.Owner).FirstOrDefault(v => v.VideoID == videoID);
        }

        public IEnumerable<Video> GetVideos()
        {
            return _context.Videos.ToList();
        }

        public void InsertVideo(Video video)
        {
            _context.Videos.Add(video);
        }

        public IEnumerable<Video> OrderVideosBy(string orderBy)
        {
            if (orderBy == "Title")
            {
                return (_context.Videos.OrderBy(v => v.Title).ToList());
            }

            else
            {
                return (_context.Videos.OrderByDescending(v => v.ReleaseYear).ToList());
            }
        }

        public void UpdateVideo(Video video)
        {
            var current = _context.Videos.FirstOrDefault(v => v.VideoID == video.VideoID);
            if (current != null)
            {
                current.Title = video.Title;
                current.ReleaseYear = video.ReleaseYear;
                current.ShortDescription = video.ShortDescription;
                current.LongDescription = video.LongDescription;
                current.Price = video.Price;
                current.Image1 = video.Image1;
                current.Image2 = video.Image2;
                current.Image3 = video.Image3;

                return;
            }

            throw new NullReferenceException();
        }

        public void DeleteVideo(int videoID)
        {
            var current = _context.Videos.FirstOrDefault(v => v.VideoID == videoID);
            if (current != null)
            {
                _context.Videos.Remove(current);
                return;
            }

            throw new NullReferenceException();

        }

    }
}
