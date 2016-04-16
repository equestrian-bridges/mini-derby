using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiniDerby.Data;

namespace MiniDerby.Models.Home
{
	public class IndexViewModel
	{
		public IndexViewModel(Event nextEvent, Event previousEvent)
		{
			this.NextEvent = nextEvent;
			this.PreviousEvent = previousEvent;
		}

		public Event PreviousEvent { get; set; }
		public Event NextEvent { get; set; }
	}
}