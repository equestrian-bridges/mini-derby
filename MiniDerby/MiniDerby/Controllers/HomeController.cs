using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniDerby.Models.Home;
using MiniDerby.Tools;


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

        public ActionResult Horses()
		{
			var horses = this.EventLogic.GetHorses();
			return View(horses);
		}

		public ActionResult Horse(Int32 id)
		{
			var horse = this.EventLogic.GetHorse(id);
			return View(horse);
		}
	}
}