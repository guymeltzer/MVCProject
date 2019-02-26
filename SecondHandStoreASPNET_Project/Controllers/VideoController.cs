using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Repositories;
using System.IO;
using Models.Models;
using System.Web.Mvc;
using ViewModels.VideoViewModel;
using Models.ViewModels;

namespace SecondHandStoreASPNET_Project.Controllers
{
    [RoutePrefix("Main")]
    public class VideoController : Controller
    {

        internal DbManager _dbManager { get; set; }

        public VideoController()
        {
            _dbManager = new DbManager();
        }
        //public void SeedVideos()
        //{
        //    List<Video> videos = new List<Video>();
        //    videos.Add(new Video() { Title = "The Sopranos Sopranos : Series Boxset Season 1-6", Price = 500, VideoID = 1, ShortDescription = "An innovative look at the life of fictional Mafia Capo Tony Soprano, this serial is presented largely first person, but additional perspective is conveyed by the intimate conversations Tony has with his psychotherapist. We see Tony at work, at home, and in therapy. Moments of black comedy intersperse this aggressive, adult drama, with adult language, and extreme violence.", LongDescription = "North Jersey mob boss, Tony Soprano, self-described 'waste management consultant' reluctantly seeks a psychiatrist's help after blacking out. Lest he appear weak, he must keep his therapy a secret from the rest of the Mob. He's stressed: his teenage daughter is giving his wife fits; his mean-spirited mother refuses to move to a retirement community; his aging Uncle Junior, jealous of Tony's rise to the top, won't stay in line and engineers a plot to kill Tony; and the feds, armed with RICO, are circling. In therapy, Tony must come to terms with his father's example, his mother's manipulations, and his own fears of death and loss of family.", ReleaseYear = 1999 });
        //    videos.Add(new Video() { Title = "Monty Python and the Holy Grail", Price = 200, VideoID = 2, ShortDescription = "King Arthur and his Knights of the Round Table embark on a surreal, low-budget search for the Holy Grail, encountering many, very silly obstacles.", LongDescription = "The movie starts out with Arthur, King of the Britons, looking for knights to sit with him at Camelot. He finds many knights including Sir Galahad the pure, Sir Lancelot the brave, the quiet Sir Bedevere, and Sir Robin the Not-Quite-So-Brave-as-Sir Lancelot. They do not travel on horses, but pretend they do and have their servants bang coconuts to make the sound of horse's hooves. Through satire of certain events in history (witch trials, the black plague) they find Camelot, but after literally a quick song and dance they decide that they do not want to go there. While walking away, God (who seems to be grumpy) come to them from a cloud and tells them to find the Holy Grail. They agree and begin their search. While they search for the Grail, scenes of the knight's tales appear and why they have the name they have. Throughout their search they meet interesting people and knights along the way. Most of the characters die; some through a killer rabbit (which they defeat with the holy hand grenade), others from not answering a question right from the bridge of Death, or die some other ridiculous way. In the end, King Arthur and Sir Bedevere are left and find the Castle Arrrghhh where the Holy Grail is. They are met by some French soldiers who taunted them earlier in the film, so they were not able to get into the castle.", ReleaseYear = 1975 });
        //    videos.Add(new Video() { Title = "Dr. Strangelove or: How I Learned to Stop Worrying and Love the Bomb", Price = 150, VideoID = 3, ShortDescription = "An insane general triggers a path to nuclear holocaust that a War Room full of politicians and generals frantically tries to stop.", LongDescription = "U.S. Air Force General Jack Ripper goes completely and utterly mad, and sends his bomber wing to destroy the U.S.S.R. He suspects that the communists are conspiring to pollute the 'precious bodily fluids' of the American people. The U.S. president meets with his advisors, where the Soviet ambassador tells him that if the U.S.S.R. is hit by nuclear weapons, it will trigger a 'Doomsday Machine' which will destroy all plant and animal life on Earth. Peter Sellers portrays the three men who might avert this tragedy: British Group Captain Lionel Mandrake, the only person with access to the demented Gen. Ripper; U.S. President Merkin Muffley, whose best attempts to divert disaster depend on placating a drunken Soviet Premier and the former Nazi genius Dr. Strangelove, who concludes that 'such a device would not be a practical deterrent for reasons which at this moment must be all too obvious.' Will the bombers be stopped in time, or will General Jack Ripper succeed in destroying the world ?", ReleaseYear = 1999 });

        //    foreach (Video v in videos)
        //    {
        //        _dbManager.VideoRepository.InsertVideo(v);
        //    }
        //}

        [Route("~/")]
        [Route("VideoIndex")]
        public ActionResult Index(string orderBy)
        {
            List<int> ItemsNotAvailable = (List<int>)HttpContext.Application["Videos"];
            using (var dbManager = new DbManager())
            {
                if (ItemsNotAvailable != null)
                {
                   return View(dbManager.VideoRepository.OrderVideosBy(orderBy).Where(v => !ItemsNotAvailable.Contains(v.VideoID)));
                }
                return View(dbManager.VideoRepository.OrderVideosBy(orderBy));
            }
        }

        //[Route("~/")]
        //[Route("VideoIndex")]
        //[Authorize]
        //public ActionResult Index()
        //{
        //    return View(_dbManager.VideoRepository.GetVideos());
        //}

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Create(VideoViewModel video)
        {
            //if (ModelState.IsValid)
            //{

            List<HttpPostedFileBase> _images = new List<HttpPostedFileBase>();
            if (video.Image1 != null)
                _images.Add(video.Image1);
            if (video.Image2 != null)
                _images.Add(video.Image2);
            if (video.Image3 != null)
                _images.Add(video.Image3);


            Video _videoModel = new Video();

            using (var manager = new DbManager())
            {
                _videoModel.Owner = _dbManager.UserRepository.GetCurrentUser();
                _videoModel.Price = video.Price;
                _videoModel.ReleaseYear = video.ReleaseYear;
                _videoModel.ShortDescription = video.ShortDescription;
                _videoModel.Title = video.Title;
                _videoModel.LongDescription = video.LongDescription;
            }



            foreach (var item in _images)
            {
                if (item != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        item.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();

                        if (_videoModel.Image1 == null)
                            _videoModel.Image1 = array;
                        else if (_videoModel.Image2 == null)
                            _videoModel.Image2 = array;
                        else
                            _videoModel.Image3 = array;
                    }
                }
            }

            _dbManager.VideoRepository.InsertVideo(_videoModel);
            _dbManager.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int ID)
        {
            _dbManager.Close();
            var currentVideo = _dbManager.VideoRepository.GetVideoByID(ID);
            return View(currentVideo);
        }

       
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var current = _dbManager.VideoRepository.GetVideoByID(id);
            return View(current);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            var current = _dbManager.VideoRepository.GetVideoByID(id);
            if (current != null)
            {
                _dbManager.VideoRepository.DeleteVideo(current.VideoID);
                _dbManager.Save();
            }
            return RedirectToAction("Index");
        }


        //public ActionResult OrderByDate()
        //{
        //    List<Video> tempList = new List<Video>();
        //    tempList = _dbManager.VideoRepository.GetVideos().OrderBy(v => v.ReleaseYear).ToList();
        //    return View("Index", tempList);
        //}

        //public ActionResult UploadImage (Video video, HttpPostedFile file)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View("Index", video);
        //    }

        //    try
        //    {
        //        if (file != null)
        //        {
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                file.InputStream.CopyTo(ms);
        //                byte[] array = ms.GetBuffer();
        //                if (video.Image1 != null)
        //                {
        //                    video.Image1 = array;
        //                }

        //                else if (video.Image2 != null)
        //                {
        //                    video.Image2 = array;
        //                }

        //                else
        //                {
        //                    video.Image3 = array;
        //                }
        //            }

        //            _dbManager.VideoRepository.InsertVideo(video);
        //            _dbManager.Save();
        //            return RedirectToAction("Index");
        //        }
        //        return View("Index", video);
        //    }

        //    catch
        //    {
        //        return View();
        //    }
    }
}
