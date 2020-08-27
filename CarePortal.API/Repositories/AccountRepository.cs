using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CarePortal.API.Entities;
using CarePortal.Data.Models;

namespace CarePortal.API.Repositories
{
    public class AccountRepository:IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public Subscription AddSubscription(string userId,string profileId)
        {
            try
            {
                Subscription addSubscription = new Subscription();
                var query = $"EXEC AddSubscription'{userId}','{profileId}';";

                addSubscription = _context.Subscription.FromSql(query).First();

                return addSubscription;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
    public interface IAccountRepository : IDisposable
    {
        Subscription AddSubscription(string userId, string profileId);
    }
}
