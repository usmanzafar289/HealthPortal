using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using CarePortal.API.Entities;
using CarePortal.Data.Models;

namespace CarePortal.API.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Department> GetDepartmentsByUserId(string userId)
        {
            List<Department> listDepartment = new List<Department>();

            var query = $"EXEC GetDepartmentsByUserId '{userId}'; ";

            listDepartment = _context.Department.FromSql(query).ToList();

            return listDepartment;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IDepartmentRepository : IDisposable
    {
        List<Department> GetDepartmentsByUserId(string userId);
    }
}
