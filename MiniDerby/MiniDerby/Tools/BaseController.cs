using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniDerby.Data;
using MiniDerby.Logic;

namespace MiniDerby.Tools
{
    public class BaseController : Controller
    {
        public DonationLogic DonationLogic { get; set; }
        public LoggingLogic LoggingLogic { get; set; }

        public BaseController()
		{
			var context = new DefaultConnection();
            this.DonationLogic = new DonationLogic(context);
            this.LoggingLogic = new LoggingLogic(context);
        }
    }
}