//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MiniDerby.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Donation
    {
        public int Id { get; set; }
        public Nullable<int> HorseId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string PaypalTransactionId { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public Nullable<int> EventId { get; set; }
    
        public virtual Horse Horse { get; set; }
        public virtual Event Event { get; set; }
    }
}
