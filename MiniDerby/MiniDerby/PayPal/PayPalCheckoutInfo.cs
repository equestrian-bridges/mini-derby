using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniDerby.PayPal
{
    public class PayPalCheckoutInfo
    {
        //ipnguide.pdf - page 43
        #region "Transaction and Notification-Related Variables"
        /// use this to verify its not spoofed, this is our info
        public String receiver_email { get; set; } //127
        public String receiver_id { get; set; } //13
        /// Keep this ID to avoid processing the transaction twice
        /// The merchant’s original transaction identification number for the payment from the buyer, against which the case was registered.
        public String txn_id { get; set; }
        /// The kind of transaction for which the IPN message was sent.
        public String txn_type { get; set; }
        /// Encrypted String used to validate the authenticity of the transaction
        public String verify_sign { get; set; }

        #endregion

        #region "Buyer Information Variables"

        public String address_country { get; set; } //64
        public String address_city { get; set; } //40
        public String address_country_code { get; set; }  //2
        public String address_name { get; set; } //128 - prob don't need
        public String address_state { get; set; } //40
        public String address_status { get; set; }  //confirmed/unconfirmed
        public String address_street { get; set; } //200
        public String address_zip { get; set; } //20
        public String contact_phone { get; set; } //20
        public String first_name { get; set; } //64
        public String last_name { get; set; } //64
        public String payer_email { get; set; } //127
        public String payer_id { get; set; }  //13

        public Int32? Zip
        {
            get
            {
                Int32 temp;
                if (Int32.TryParse(address_zip, out temp))
                {
                    return temp;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion
        #region "Payment Information Variables"

        /*
        public String auth_amount { get; set; }
        public String auth_exp { get; set; } //28
        public String auth_id { get; set; } //19
        public String auth_status { get; set; }
         */

        /// Token passed back from PayPal for cross ref
        public String token { get; set; }
        //public String checkoutstatus { get; set; }
        /// Passthrough variable you can use to identify your Invoice Number for this purchase. If omitted, no variable is passed back.
        public String invoice { get; set; } //127
        /// Item name as passed by you, the merchant. Or, if not passed by you, as
        /// entered by your customer. If this is a shopping cart transaction, PayPal
        /// will append the number of the item (e.g., item_name1, item_name2,
        /// and so forth).
        public String item_name1 { get; set; } //127
        public String item_number1 { get; set; } //127
        public String mc_currency { get; set; } //currency of the payment.
        public String mc_fee { get; set; } //Transaction fee associated with the payment
        public String mc_gross { get; set; }    //Full amount of the customer's payment 
        /// Whether the customer has a verified PayPal account.
        /// verified – Customer has a verified PayPal account
        /// unverified – Customer has an unverified PayPal account.
        public String payer_status { get; set; }
        /// HH:MM:SS Mmm DD, YYYY PDT (28chars)
        public String payment_date { get; set; }

        public DateTime TrxnDate
        {
            get
            {
                DateTime dt = DateTime.Now;
                if (DateTime.TryParse(payment_date, out dt))
                {
                    return dt;
                }
                else
                {
                    return DateTime.Now;
                }
            }
        }

        /// The status of the payment:
        /// Canceled_Reversal: A reversal has been canceled. For example, you
        /// won a dispute with the customer, and the funds for the transaction that was
        /// reversed have been returned to you.
        /// Completed: The payment has been completed, and the funds have been added successfully to your account balance
        ///Created: A German ELV payment is made using Express Checkout. Denied: You denied the payment. This happens only if the payment was
        /// previously pending because of possible reasons described for the pending_reason variable or the Fraud_Management_Filters_x variable.
        /// Expired: This authorization has expired and cannot be captured.
        ///Failed: The payment has failed. This happens only if the payment was made from your customer’s bank account.
        ///Pending: The payment is pending. See pending_reason for more information.
        ///Refunded: You refunded the payment.
        ///Reversed: A payment was reversed due to a chargeback or other type of reversal. The funds have been removed from your account balance and
        ///returned to the buyer. The reason for the reversal is specified in the ReasonCode element.
        ///Processed: A payment has been accepted.
        ///Voided: This authorization has been voided.
        public String payment_status { get; set; }
        /// echeck: This payment was funded with an eCheck.
        /// instant: This payment was funded with PayPal balance, credit card, or Instant Transfer.
        public String payment_type { get; set; }
        /// This variable is set only if payment_status = Pending. - too many reasons (look it up in pdf)
        public String pending_reason { get; set; }
        public String protection_eligibility { get; set; }
        public String quantity { get; set; }
        public String reason_code { get; set; }
        public String correlationID { get; set; }
        public String ack { get; set; }
        public String errmsg { get; set; }
        public Int32? errcode { get; set; }

        /// should hold the clientid passed in from setexpresscheckout
        public String custom { get; set; }

        public Decimal Total
        {
            get
            {
                Decimal amount = 0;
                if (Decimal.TryParse(mc_gross, out amount))
                {
                    return amount;
                }
                else
                {
                    return 0;
                }
            }
        }
        public Decimal Fee
        {
            get
            {
                Decimal amount = 0;
                if (Decimal.TryParse(mc_fee, out amount))
                {
                    return amount;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion
    }
}