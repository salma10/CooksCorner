using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CooksCorner.Models;
using System.Web.Security;
using WebMatrix.WebData;
using PagedList;

namespace CooksCorner.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WrittenTutorialController : Controller
    {
        private CooksCornerDatabaseContext db = new CooksCornerDatabaseContext();
        public WrittenTutorialController()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                    "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }
        //
        // GET: /WrittenTutorial/

        public ActionResult Index(int? page)
        {
            
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var tutorials = db.WrittenTutorials.OrderBy(t => t.Id).Include(t => t.Genre);
            return View(tutorials.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /WrittenTutorial/Details/5

        public ActionResult Details(int id = 0)
        {
            WrittenTutorial writtentutorial = db.WrittenTutorials.Find(id);
            if (writtentutorial == null)
            {
                return HttpNotFound();
            }
            return View(writtentutorial);
        }

        //
        // GET: /WrittenTutorial/Create
        [AllowAnonymous]
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /WrittenTutorial/Create
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        public ActionResult Create(WrittenTutorial writtentutorial)
        {
            writtentutorial.UserId = (int)Membership.GetUser().ProviderUserKey;
            if (ModelState.IsValid)
            {
                db.WrittenTutorials.Add(writtentutorial);
                db.SaveChanges();
                SendNotificationToSubscriber(writtentutorial);
                if (Roles.GetRolesForUser().Contains("Admin"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("MyWrittenRecipes", "Home");
                }
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", writtentutorial.GenreId);
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", writtentutorial.UserId);
            return View(writtentutorial);
        }

        //
        // GET: /WrittenTutorial/Edit/5

        public ActionResult Edit(int id = 0)
        {
            WrittenTutorial writtentutorial = db.WrittenTutorials.Find(id);
            if (writtentutorial == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", writtentutorial.GenreId);
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", writtentutorial.UserId);
            return View(writtentutorial);
        }

        //
        // POST: /WrittenTutorial/Edit/5

        [HttpPost]
        public ActionResult Edit(WrittenTutorial writtentutorial)
        {
            writtentutorial.UserId = (int)Membership.GetUser().ProviderUserKey;
            if (ModelState.IsValid)
            {
                db.Entry(writtentutorial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", writtentutorial.GenreId);
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", writtentutorial.UserId);
            return View(writtentutorial);
        }

        //
        // GET: /WrittenTutorial/Delete/5

        public ActionResult Delete(int id = 0)
        {
            WrittenTutorial writtentutorial = db.WrittenTutorials.Find(id);
            if (writtentutorial == null)
            {
                return HttpNotFound();
            }
            return View(writtentutorial);
        }

        //
        // POST: /WrittenTutorial/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            WrittenTutorial writtentutorial = db.WrittenTutorials.Find(id);
            db.WrittenTutorials.Remove(writtentutorial);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Send mail to passing mail 
        private void SendMail(string MailAddress)
        {
            try 
            {
 
            }
            catch(Exception e)
            {

            }
            
        }
        private void SendNotificationToSubscriber(WrittenTutorial recipe)
        {
            var subscribers = db.Subscribers.ToList();
            foreach (var subscriber in subscribers)
            {
                Notification notification = new Notification("", subscriber.Email, "", "Hi " + subscriber.UserName + ",<br/>    Our new Recipe with Title :" + recipe.Title + "<br/>  Thanks for subscribing in CooksCorner");
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