using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniDerby.Data;
using MiniDerby;

namespace MiniDerby.Logic
{
    public class DonationLogic : BaseLogic
    {
        public DonationLogic(DefaultConnection context) : base(context)
        { }

        public List<Horse> GetHorses()
        {
            var horses = Context.Horses.ToList();

            return horses;
        }

        public void SaveDonation(String transactionId, String firstName, String lastName, String email, Decimal totalAmount, Int32 horseId)
        {
            // Check to see if donation has already been saved
            var existingDonation = Context.Donations.SingleOrDefault(x => x.PaypalTransactionId == transactionId);

            if (existingDonation == null)
            {
                var newDonation = new Donation
                {
                    Amount = totalAmount,
                    Email = email,
                    HorseId = horseId,
                    Name = String.Format("{0} {1}", firstName, lastName),
                    PaypalTransactionId = transactionId,
                    TransactionDate = DateTime.Now
                };

                Context.Donations.Add(newDonation);
                Context.SaveChanges();
            }
        }
    }
}
