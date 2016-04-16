using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.IO;
using MiniDerby.Models;
using MiniDerby.PayPal;

namespace MiniDerby.Controllers
{
    public class IPNController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public EmptyResult PayPalPaymentNotification(PayPalCheckoutInfo payPalCheckoutInfo)
        {
            PayPalListenerModel model = new PayPalListenerModel();
            model.PayPalCheckoutInfo = payPalCheckoutInfo;
            byte[] parameters = Request.BinaryRead(Request.ContentLength);

            if (parameters != null)
            {
                model.GetStatus(parameters);
            }

            return new EmptyResult();
        }
    }
}

