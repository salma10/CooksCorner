using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CooksCorner.Models;
using WebMatrix.WebData;

namespace CooksCorner.Controllers
{
     [Authorize(Roles = "Admin")]
    public class StoreManagerController : Controller
    {
        private CooksCornerDatabaseContext db = new CooksCornerDatabaseContext();

        public StoreManagerController()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                 "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }
        public ActionResult Index()
        {
            var AllCategory = db.Videos.Include("Genre");

            return View();
        }

       
    }
}
