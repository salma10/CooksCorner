using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using CooksCorner.Models;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using PagedList;


namespace CooksCorner.Controllers
{
    public class HomeController : Controller
    {
        private CooksCornerDatabaseContext db = new CooksCornerDatabaseContext();

        //
        // GET: /Home/
        public HomeController()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection",
"UserProfile", "UserId", "UserName", autoCreateTables: true);
        }

        public ActionResult Index()
        {
            try {

                if (Request.IsAuthenticated)
                { 
                    int id = WebSecurity.GetUserId(User.Identity.Name);
                    var UserInfo = db.UserInfos.Where(u => u.UserId == id).ToList();
                    if (UserInfo.Count > 0)
                    {
                        UserInfo UInfo = new UserInfo();
                        UInfo.UserId = id;
                        db.UserInfos.Add(UInfo);
                        db.SaveChanges();
                    }
                    
                }
                
            }catch(Exception e){
                int s = 55;
            }
            var genres = db.Genres.ToList();
            var latestVideo1 = db.Videos.OrderByDescending(v => v.VideoId).Take(1).ToList();
            var latestVideo2 = db.Videos.OrderByDescending(v => v.VideoId).Skip(1).Take(1).ToList();
            var latestArticle1 = db.WrittenTutorials.OrderByDescending(a => a.Id).Take(1).ToList();
            var latestArticle2 = db.WrittenTutorials.OrderByDescending(a => a.Id).Skip(1).Take(1).ToList();
            ViewBag.video1 = latestVideo1;
            ViewBag.video2 = latestVideo2;
            ViewBag.article1 = latestArticle1;
            ViewBag.article2 = latestArticle2;
            return View(genres);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
                       
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Email objModelMail)
        {
            if (ModelState.IsValid)
            {
                
                objModelMail.To = "cookscornerbd@gmail.com"; // mail where we want to send
                objModelMail.From = "cookscornerbd@gmail.com";  // from where we want to send
                bool isMailSend = sendMail(objModelMail);
                if (isMailSend)
                {
                    ViewBag.Message = "sent";
                    return View("Contact", objModelMail);
                }
                else {
                    ViewBag.Message = "fail";
                    return View("Contact", objModelMail);
                }
                
            }
            else
            {
                return View();
            }
        }

        

        public ActionResult Browse(int id)
        {
            var genres = db.Genres.Include("Videos").Single(g => g.GenreId == id);

            return View(genres);
        }

        public ActionResult BrowseWritten(int id)
        {
            var genres = db.Genres.Include("WrittenTutorials").Single(g => g.GenreId == id);

            return View(genres);
        }

        public ActionResult Details(int id)
        {
            var video = db.Videos.Find(id);
            return View(video);
        }

        public ActionResult WrittenDetail(int id)
        {
            var tutorial = db.WrittenTutorials.Find(id);
            return View(tutorial);
        }

        public ActionResult Faq()
        {
            return View(db.Faqs.ToList());
        }
        [Authorize]
        public ActionResult MyVideoRecipes(int? page)
        {
            //var videos = db.Videos.Where(v => v.UserId == (int)WebSecurity.CurrentUserId).ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var videos = db.Videos.OrderBy(v => v.VideoId).Where(v => v.UserId == (int)WebSecurity.CurrentUserId);
            return View(videos.ToPagedList(pageNumber, pageSize));
            
        }

        [Authorize]
        public ActionResult MyWrittenRecipes(int? page)
        {
            //var videos = db.Videos.Where(v => v.UserId == (int)WebSecurity.CurrentUserId).ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var recipes = db.WrittenTutorials.OrderBy(v => v.Id).Where(v => v.UserId == (int)WebSecurity.CurrentUserId);
            return View(recipes.ToPagedList(pageNumber, pageSize));

        }
        

        private bool sendMail(Email objModelMail)
        {
            try
            {
                using (MailMessage mail = new MailMessage(objModelMail.From, objModelMail.To))
                {
                    mail.Subject = objModelMail.Subject;
                    mail.Body = objModelMail.Body + "\n Sender Mail :" + objModelMail.SenderMail;

                    mail.IsBodyHtml = false;
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
            }catch( Exception e){
                return false;
            }
        }
      
    }
}
