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
using MiniDerby.PayPal;
using MiniDerby.Tools;
using System.Text;

namespace MiniDerby.Controllers
{
    public class IPNController : BaseController
    {
        [HttpPost]
        public ActionResult PayPalPaymentNotification(PayPalCheckoutInfo payPalCheckoutInfo)
        {
            byte[] parameters = Request.BinaryRead(Request.ContentLength);

            try
            {
                if (parameters != null)
                {
                    // Set the first parameter to false when putting in production
                    if (GetVerificationCode(false, parameters) == "VERIFIED")
                    {
                        var horseId = Int32.Parse(payPalCheckoutInfo.custom);
                        this.DonationLogic.SaveDonation(payPalCheckoutInfo.txn_id, payPalCheckoutInfo.first_name, payPalCheckoutInfo.last_name, payPalCheckoutInfo.payer_email, payPalCheckoutInfo.Total, horseId);

                        return new EmptyResult();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingLogic.LogException(ex);
            }

            LoggingLogic.LogError("A bad request came from PayPal", Encoding.ASCII.GetString(parameters));

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        private string GetVerificationCode(Boolean isSandbox, Byte[] parameters)
        {
            string response = "";
            try
            {
                string url = isSandbox ?
                  "https://www.sandbox.paypal.com/cgi-bin/webscr" : "https://www.paypal.com/cgi-bin/webscr";

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";

                //must keep the original intact and pass back to PayPal with a _notify-validate command
                string data = Encoding.ASCII.GetString(parameters);
                data += "&cmd=_notify-validate";

                webRequest.ContentLength = data.Length;

                //Send the request to PayPal and get the response                 
                using (StreamWriter streamOut = new StreamWriter(webRequest.GetRequestStream(), System.Text.Encoding.ASCII))
                {
                    streamOut.Write(data);
                    streamOut.Close();
                }

                using (StreamReader streamIn = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                    streamIn.Close();
                }

            }
            catch (Exception ex)
            {
                this.LoggingLogic.LogException(ex, "Error when verifying paypal transaction");
            }

            return response;
        }
    }
}

