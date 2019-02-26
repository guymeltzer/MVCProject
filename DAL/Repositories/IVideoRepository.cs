using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DAL.Repositories
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetVideos();

        Video GetVideoByID(int videoID);

        void InsertVideo(Video video);

        void UpdateVideo(Video video);

        void DeleteVideo(int videoID);

        IEnumerable<Video> OrderVideosBy(string orderBy);

    }
}
