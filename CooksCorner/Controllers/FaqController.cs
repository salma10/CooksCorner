using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CooksCorner.Models;
using WebMatrix.WebData;

namespace CooksCorner.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FaqController : Controller
    {
        private CooksCornerDatabaseContext db = new CooksCornerDatabaseContext();


        public FaqController() {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                    "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }
        //
        // GET: /Faq/

        public ActionResult Index()
        {
            return View(db.Faqs.ToList());
        }

        //
        // GET: /Faq/Details/5

        public ActionResult Details(int id = 0)
        {
            Faq faq = db.Faqs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        //
        // GET: /Faq/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Faq/Create

        [HttpPost]
        public ActionResult Create(Faq faq)
        {
            if (ModelState.IsValid)
            {
                db.Faqs.Add(faq);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(faq);
        }

        //
        // GET: /Faq/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Faq faq = db.Faqs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        //
        // POST: /Faq/Edit/5

        [HttpPost]
        public ActionResult Edit(Faq faq)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faq).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faq);
        }

        //
        // GET: /Faq/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Faq faq = db.Faqs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        //
        // POST: /Faq/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Faq faq = db.Faqs.Find(id);
            db.Faqs.Remove(faq);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}