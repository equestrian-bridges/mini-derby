﻿using MiniDerby.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDerby.Logic
{
    public class LoggingLogic : BaseLogic
    {
		public LoggingLogic() : base(new DefaultConnection())
		{ }

        public LoggingLogic(DefaultConnection context) : base(context)
        { }

        public void LogException(Exception ex, String message = "")
        {
            Context.Errors.Add(new Error
            {
                ErrorDate = DateTime.Now,
                ErrorMessage = message,
                StackTrace = ex.ToString()
            });

            Context.SaveChanges();
        }

        public void LogError(String title, String message)
        {
            Context.Errors.Add(new Error
            {
                ErrorDate = DateTime.Now,
                ErrorMessage = title,
                StackTrace = message
            });

            Context.SaveChanges();
        }
    }
}
