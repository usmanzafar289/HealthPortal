using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using CarePortal.API.Entities;
using CarePortal.Data.Models;
using CarePortal.Data.ViewModels;

namespace CarePortal.API.Repositories
{
    public class FinanceRepository : IFinanceRepository
    {
        private readonly ApplicationDbContext _context;

        public FinanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public List<Subscription> GetSubscriptions(string _userId) {
            try
            {
                List<Subscription> listAllUserSubscriptions = new List<Subscription>();
                var query = $"EXEC GetSubscriptions'{_userId}';";

                listAllUserSubscriptions = _context.Subscription.FromSql(query).ToList();

                return listAllUserSubscriptions;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public PaymentMethod AddPaymentMethod(AddPaymentViewModel addPaymentViewModel)
        {
            try
            {
                PaymentMethod paymentMethod = new PaymentMethod();
                var query = $"EXEC AddPaymentMethod'{addPaymentViewModel.UserId}', '{addPaymentViewModel.FirstName}','{addPaymentViewModel.LastName}','{addPaymentViewModel.CardNumber}','{addPaymentViewModel.Expiry}','{addPaymentViewModel.CVV}','{addPaymentViewModel.IsDefault}','{addPaymentViewModel.IsDelete}','{addPaymentViewModel.Timestamp}';";

                paymentMethod = _context.PaymentMethod.FromSql(query).FirstOrDefault();

                return paymentMethod;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<PaymentMethod> GetPaymentMethods(string _userId)
        {
            try
            {
                List<PaymentMethod> listAllPaymentMethods = new List<PaymentMethod>();
                var query = $"EXEC GetPaymentMethods'{_userId}';";

                listAllPaymentMethods = _context.PaymentMethod.FromSql(query).ToList();

                return listAllPaymentMethods;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Subscription AddBalance(string _userId, decimal _amount, string _transactionId, DateTimeOffset _Timestamp, DateTimeOffset _TransactionDate)
        {
            try
            {
                Subscription subscriptionItem = new Subscription();
                var query = $"EXEC AddBalance'{_userId}','{_amount}','{_transactionId}','{_Timestamp}','{_TransactionDate}';";

                subscriptionItem = _context.Subscription.FromSql(query).FirstOrDefault();

                return subscriptionItem;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public interface IFinanceRepository : IDisposable
    {
        List<Subscription> GetSubscriptions(string _userId);
        PaymentMethod AddPaymentMethod(AddPaymentViewModel addPaymentViewModel);
        List<PaymentMethod> GetPaymentMethods(string _userId);
        Subscription AddBalance(string _userId,decimal _amount,string _transactionId,DateTimeOffset _Timestamp, DateTimeOffset _TransactionDate);
    }
}
