using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniDerby.Tools;

namespace MiniDerby.Controllers
{
    public class DonationController : BaseController
    {
        public ActionResult Index()
        {
			var horses = this.DonationLogic.GetHorses();
            return View(horses);
        }
    }
}