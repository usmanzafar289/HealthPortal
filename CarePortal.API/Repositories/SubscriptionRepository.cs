using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using CarePortal.API.Entities;
using CarePortal.Data.Models;

namespace CarePortal.API.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Subscription> GetSubscriptions(string UserId)
        {
            List<Subscription> listSubscription = new List<Subscription>();

            var query = $"EXEC GetSubscriptions '{UserId}'; ";

            listSubscription = _context.Subscription.FromSql(query).ToList();

            return listSubscription;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface ISubscriptionRepository : IDisposable
    {
        List<Subscription> GetSubscriptions(string UserId);
    }
}
