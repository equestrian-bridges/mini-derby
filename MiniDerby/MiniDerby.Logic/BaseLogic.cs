using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniDerby.Data;

namespace MiniDerby.Logic
{
	public class BaseLogic : IDisposable
	{
		protected MiniDerby.Data.DefaultConnection Context { get; set; }

		public BaseLogic(DefaultConnection context)
		{
			if (context != null)
			{
				this.Context = context;
			}
			else
			{
				this.Context = new Data.DefaultConnection();
			}
		}

		public void Dispose()
		{
			((IDisposable)Context).Dispose();
		}
	}
}
