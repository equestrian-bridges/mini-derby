using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDerby.Data
{
	public partial class Horse
	{
		public decimal TotalAmountRaised
		{
			get
			{
				return this.Donations.Sum(x => x.Amount).Value;
			}
		}
	}
}
