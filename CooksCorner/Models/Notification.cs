using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace CooksCorner.Models
{
    public class Notification
    {
        public string SenderMail { get; set; }
        public string ReceiverMail { get; set; }
        public string MailBody { get; set; }
        public string MailSubject { get; set; }

        public Notification(string FromMail, string ToMail, string Subject, string Body)
        {
            this.SenderMail = String.IsNullOrEmpty(FromMail) ? "cookscornerbd@gmail.com" : FromMail;
            this.ReceiverMail = String.IsNullOrEmpty(ToMail) ? "cookscornerbd@gmail.com" : ToMail;
            this.MailBody = String.IsNullOrEmpty(Body) ? "New Recipe has been posted on CocksCorner, Please check it out" : Body;
            this.MailSubject = String.IsNullOrEmpty(Subject) ? "New Recipe has been posted on CocksCorner" : Subject;
        }

        public bool sendMail()
        {
            try
            {
                using (MailMessage mail = new MailMessage(this.SenderMail, this.ReceiverMail))
                {
                    mail.Subject = this.MailSubject;
                    mail.Body = this.MailBody;

                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(this.SenderMail, "cooks@corner");
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