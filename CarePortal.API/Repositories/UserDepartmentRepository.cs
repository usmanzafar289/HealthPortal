using System;
using CarePortal.API.Entities;

namespace CarePortal.API.Repositories
{
    public class UserDepartmentRepository : IUserDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public UserDepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IUserDepartmentRepository : IDisposable
    {
    }
}
