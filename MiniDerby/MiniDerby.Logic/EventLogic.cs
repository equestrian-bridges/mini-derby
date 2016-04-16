using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniDerby.Data;
using System.Data.Entity;

namespace MiniDerby.Logic
{
	public class EventLogic : BaseLogic
	{
		public EventLogic(DefaultConnection context) : base(context)
		{}

		public Event GetNextEvent()
		{
			return GetEventsBaseQuery()
						.OrderBy(x => x.EventDate)
						.SingleOrDefault(x => x.EventDate > DateTime.Today);
		}

		public Event GetPreviousEvent()
		{
			return GetEventsBaseQuery()
						.OrderByDescending(x => x.EventDate)
						.SingleOrDefault(x => x.EventDate < DateTime.Today);
		}

		private IQueryable<Event> GetEventsBaseQuery()
		{
			return this.Context.Events
							.Include(x => x.Horses)
							.Include(x => x.Horses.Select(y => y.Donations))
							.Include(x => x.Sponsors);
		}
	}
}
