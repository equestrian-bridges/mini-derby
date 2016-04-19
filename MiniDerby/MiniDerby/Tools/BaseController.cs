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
		private readonly DefaultConnection _context;
		public DonationLogic DonationLogic { get; set; }
		public EventLogic EventLogic { get; set; }
        public LoggingLogic LoggingLogic { get; set; }

		public BaseController()
		{
			_context = new DefaultConnection();
			this.DonationLogic = new DonationLogic(_context);
            this.LoggingLogic = new LoggingLogic(_context);
			this.EventLogic = new EventLogic(_context);
		}

		public new void Dispose()
		{
			_context.Dispose();
			base.Dispose();
		}
    }
}