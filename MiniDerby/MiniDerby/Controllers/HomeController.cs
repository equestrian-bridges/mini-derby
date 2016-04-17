using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniDerby.Models.Home;
using MiniDerby.Tools;
using System.Net.Mail;
using System.Text;
using MiniDerby.Models.Contact;

namespace MiniDerby.Controllers
{
	public class HomeController : BaseController
	{
		public ActionResult Index()
		{
			var model = new IndexViewModel(this.EventLogic.GetNextEvent(), this.EventLogic.GetPreviousEvent());
			return View(model);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}
        
		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
        
        
        [HttpPost]
        public ActionResult Contact(ContactModel c)
        {
            try
            {
                var msg = new MailMessage();
                var smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential("fivenseven@gmail.com", "Moxyox12");

                var from = new MailAddress(c.Email.ToString());
                var sb = new StringBuilder();

                msg.To.Add("youremail@email.com");
                msg.Subject = String.Format("Contact Us: {0}", c.Subject);
                msg.IsBodyHtml = false;
                smtp.Host = "mail.yourdomain.com";
                sb.Append("Name: " + c.Name);
                sb.Append(Environment.NewLine);
                sb.Append("Email: " + c.Email);
                sb.Append(Environment.NewLine);
                sb.Append("Comments: " + c.Message);
                msg.Body = sb.ToString();
                smtp.Send(msg);
                msg.Dispose();
                return View("Success");
            }
            catch (Exception ex)
            {
                LoggingLogic.LogException(ex, "Home Controller");
                return View("Error");
            }
        }

        public ActionResult Horses()
		{
			return View();
		}
	}
}