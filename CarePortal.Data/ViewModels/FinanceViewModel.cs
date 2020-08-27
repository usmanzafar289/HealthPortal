using CarePortal.Data.Models;
using System;
using System.Collections.Generic;

namespace CarePortal.Data.ViewModels
{
    public class FinanceViewModel
    {
        public List<Subscription> Subscriptions { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
    }
    public class AddPaymentViewModel
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CardNumber { get; set; }

        public string Expiry { get; set; }

        public int CVV { get; set; }

        public bool IsDefault { get; set; }

        public bool IsDelete { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
    public class AddBalanceViewModel
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
