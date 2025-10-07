using El_Aref.BLL.Interfaces;
using El_Aref.DAL.Data.Contexts;
using EL_Areff.Comapny.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Aref.BLL.Repositores
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ElAreffDbContext _context;

        public IDepartmentRepository DepartmentRepository { get; }

        public IEmployeeRepository EmployeeRepository { get; }
        public UnitOfWork(ElAreffDbContext context)
        {
            _context = context;
            DepartmentRepository = new DepartmentRepository(_context);
            EmployeeRepository = new EmployeeRepository(_context);
        }

        public async Task<int> ComleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

       

       
        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
