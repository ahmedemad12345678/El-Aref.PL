using El_Aref.BLL.Interfaces;
using El_Aref.BLL.Repositores;
using El_Aref.DAL.Data.Contexts;
using El_Aref.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_Areff.Comapny.BLL.Repositories
{
    public class DepartmentRepository : GenaricRepository<Department>, IDepartmentRepository
    {
        private readonly ElAreffDbContext _context;

        public DepartmentRepository(ElAreffDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Department>? GetByName(string name)
        {
            return _context.Departments.Include(E => E.Employees).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}
