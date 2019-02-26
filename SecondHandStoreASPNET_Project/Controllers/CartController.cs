using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repositories;

namespace SecondHandStoreASPNET_Project.Controllers
{
    public class CartController : Controller
    {
        public ActionResult AddToCart(int id)
        {
            List<int> VideosNotAvailableByID = new List<int>();
            List<int> VideosInSpecificUserShoppingCart = new List<int>();

            if (HttpContext.Session["Videos"] != null)
            {
                VideosInSpecificUserShoppingCart = (List<int>)HttpContext.Session["Videos"];
            }

            if (HttpContext.Application["Videos"] != null)
            {
                VideosNotAvailableByID = (List<int>)HttpContext.Application["Videos"];
            }

            if (id != 0)
            {
                VideosNotAvailableByID.Add(id);
                VideosInSpecificUserShoppingCart.Add(id);
            }

            HttpContext.Session["Videos"] = VideosInSpecificUserShoppingCart;
            HttpContext.Application["Videos"] = VideosNotAvailableByID;

            return RedirectToAction("Index", "Video");
        }

        public ActionResult GetCart()
        {
            using (DbManager dbManager = new DbManager())
            {
                List<int> ItemsInCart = (List<int>)HttpContext.Session["Videos"];
                if (ItemsInCart != null)
                {
                    return View(dbManager.VideoRepository.GetVideos().Where(v => ItemsInCart.Contains(v.VideoID)));
                }

                else
                {
                    return View(new List<Video>());
                }
            }
        }

        public ActionResult DeleteCart(int id)
        {
            List<int> ItemsNotAvailable = new List<int>();
            List<int> ItemsInCart = new List<int>();

            if (HttpContext.Session["Videos"] != null)
            {
                ItemsInCart = (List<int>)HttpContext.Session["Videos"];
            }

            if (HttpContext.Application["Videos"] != null)
            {
                ItemsNotAvailable = (List<int>)HttpContext.Application["Videos"];
            }

            using (var DbManger = new DbManager())
            {
                ItemsNotAvailable.Remove(id);
                ItemsInCart.Remove(id);
            }
            return RedirectToAction("GetCart");
        }

        public ActionResult CheckOut()
        {
            var CurrentItems = ShoppingCart.GetItemsInCart();

            foreach (var id in CurrentItems)
            {
                using (var dbManager = new DbManager())
                {
                    dbManager.VideoRepository.DeleteVideo(id);
                    dbManager.Save();
                }
                
            }
            ShoppingCart.ClearShoppingCart();
            return RedirectToAction("Index", "Video");
            
        }
    }
}