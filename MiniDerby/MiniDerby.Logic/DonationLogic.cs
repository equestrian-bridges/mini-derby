using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniDerby.Data;

namespace MiniDerby.Logic
{
    public class DonationLogic : BaseLogic
    {
		public DonationLogic(DefaultConnection context) : base(context)
		{}

		public List<Horse> GetHorses()
		{
			var horses = Context.Horses.ToList();

			return horses;
		}
    }
}
