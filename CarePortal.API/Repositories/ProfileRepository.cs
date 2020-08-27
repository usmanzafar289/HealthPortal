using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using CarePortal.API.Entities;
using CarePortal.Data.Models;

namespace CarePortal.API.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Department> GetAllDepartments()
        {
            var query = $"EXEC GetAllDepartments; ";

            List<Department> listDepartments = _context.Department.FromSql(query).ToList();

            return listDepartments;
        }

        public List<UserDepartment> AddUserDepartments(string userId, string departments, DateTimeOffset timestamp)
        {
            try
            {
                var query = $"EXEC AddUserDepartments '{userId}','{departments}','{timestamp}'; ";

                List<UserDepartment> listDepartments = _context.UserDepartment.FromSql(query).ToList();

                return listDepartments;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
    public interface IProfileRepository : IDisposable
    {
        List<Department> GetAllDepartments();
        List<UserDepartment> AddUserDepartments(string userId, string departments, DateTimeOffset timestamp);
    }
}
