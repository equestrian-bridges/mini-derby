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
						.OrderBy(x => x.EventStartDate)
						.SingleOrDefault(x => x.EventStartDate > DateTime.Today);
		}

		public Event GetPreviousEvent()
		{
			return GetEventsBaseQuery()
						.OrderByDescending(x => x.EventStartDate)
						.SingleOrDefault(x => x.EventStartDate < DateTime.Today);
		}

		private IQueryable<Event> GetEventsBaseQuery()
		{
			return this.Context.Events
							.Include(x => x.Horses)
							.Include(x => x.Horses.Select(y => y.Donations))
							.Include(x => x.Sponsors);
		}

		public List<Horse> GetHorses()
		{
			var eventOfInterest = GetNextEvent();
			if (eventOfInterest == null)
			{
				eventOfInterest = GetPreviousEvent();
			}

			return GetHorseBaseQuery()
							.Where(x => x.EventId == eventOfInterest.Id)
							.ToList();
		}

		public Horse GetHorse(Int32 id)
		{
			return GetHorseBaseQuery().Single(x => x.Id == id);
		}

		private IQueryable<Horse> GetHorseBaseQuery()
		{
			return this.Context.Horses
							.Include(x => x.Donations)
							.Include(x => x.Event)
							.Include(x => x.Sponsor);
		}
	}
}
