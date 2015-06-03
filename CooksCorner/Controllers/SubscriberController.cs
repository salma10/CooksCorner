using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CooksCorner.Models;
using WebMatrix.WebData;
using System.Net.Mail;
using System.Net;

namespace CooksCorner.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubscriberController : Controller
    {
        private CooksCornerDatabaseContext db = new CooksCornerDatabaseContext();

        public SubscriberController()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                    "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }
        //
        // GET: /Subscriber/

        public ActionResult Index()
        {
            return View(db.Subscribers.ToList());
        }

        //
        // GET: /Subscriber/Details/5

        public ActionResult Details(int id = 0)
        {
            Subscriber subscriber = db.Subscribers.Find(id);
            if (subscriber == null)
            {
                return HttpNotFound();
            }
            return View(subscriber);
        }

        //
        // GET: /Subscriber/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Subscriber/Create
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {

                db.Subscribers.Add(subscriber);
                db.SaveChanges();
                Email objModelMail = new Email();
                objModelMail.To = subscriber.Email ; // mail where we want to send
                objModelMail.From = "cookscornerbd@gmail.com";  // from where we want to send
                objModelMail.Subject = "Succees Message";
                objModelMail.Body = "Hi " + subscriber.UserName + ", <br/> You successfully subscribe to CooksCorner.com. <br/>Thanks for your subscribe"; 
                bool isMailSend = sendMail(objModelMail);
                ViewBag.Message = "sent";
                return RedirectToAction("Index", "Home");
            }
            
           

            return View(subscriber);
        }

        //
        // GET: /Subscriber/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Subscriber subscriber = db.Subscribers.Find(id);
            if (subscriber == null)
            {
                return HttpNotFound();
            }
            return View(subscriber);
        }

        //
        // POST: /Subscriber/Edit/5

        [HttpPost]
        public ActionResult Edit(Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscriber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subscriber);
        }

        //
        // GET: /Subscriber/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Subscriber subscriber = db.Subscribers.Find(id);
            if (subscriber == null)
            {
                return HttpNotFound();
            }
            return View(subscriber);
        }

        //
        // POST: /Subscriber/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscriber subscriber = db.Subscribers.Find(id);
            db.Subscribers.Remove(subscriber);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private bool sendMail(Email objModelMail)
        {
            try
            {
                using (MailMessage mail = new MailMessage(objModelMail.From, objModelMail.To))
                {
                    mail.Subject = objModelMail.Subject;
                    mail.Body = objModelMail.Body ;

                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(objModelMail.From, "cooks@corner");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}