using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CooksCorner.Models;
using WebMatrix.WebData;
using System.Web.Security;
using PagedList;

namespace CooksCorner.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VideoController : Controller
    {
        private CooksCornerDatabaseContext db = new CooksCornerDatabaseContext();


        public VideoController()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                    "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }
        //
        // GET: /Video/

        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            
            var video = db.Videos.OrderBy(v => v.VideoId).Include(v => v.Genre);
            return View(video.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Video/Details/5

        public ActionResult Details(int id = 0)
        {
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        //
        // GET: /Video/Create
        [AllowAnonymous]
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            return View();
        }

        //
        // POST: /Video/Create
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        public ActionResult Create(Video video)
        {
            video.UserId = (int)Membership.GetUser().ProviderUserKey;
            if (ModelState.IsValid)
            {
                db.Videos.Add(video);
                db.SaveChanges();
                SendNotificationToSubscriber(video);
                if (Roles.GetRolesForUser().Contains("Admin"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("MyVideoRecipes", "Home");
                }
                 
                
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", video.GenreId);
            return View(video);
        }

        //
        // GET: /Video/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", video.GenreId);
            return View(video);
        }

        //
        // POST: /Video/Edit/5

        [HttpPost]
        public ActionResult Edit(Video video)
        {
            video.UserId = (int)Membership.GetUser().ProviderUserKey;
            if (ModelState.IsValid)
            {
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", video.GenreId);
            return View(video);
        }

        //
        // GET: /Video/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        //
        // POST: /Video/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Video video = db.Videos.Find(id);
            db.Videos.Remove(video);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void SendNotificationToSubscriber( Video video)
        {
            var subscribers = db.Subscribers.ToList();
            foreach (var subscriber in subscribers)
            {
                Notification notification = new Notification("", subscriber.Email, "", "Hi " + subscriber.UserName + ",<br/>&nbsp;&nbsp;&nbsp;&nbsp;Our new Video Recipe with Title :" + video.Title + "<br/>&nbsp;&nbsp;&nbsp;Thanks for subscribing in CooksCorner");
                notification.sendMail();
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}