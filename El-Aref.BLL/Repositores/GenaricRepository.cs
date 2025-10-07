using El_Aref.BLL.Interfaces;
using El_Aref.DAL.Data.Contexts;
using El_Aref.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace El_Aref.BLL.Repositores
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {
        private readonly ElAreffDbContext _context;

        public GenaricRepository(ElAreffDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T)==typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include(E=>E.Department).ToListAsync() ;
            }
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return (T?)(object?) await _context.Employees.Include(E => E.Department).FirstOrDefaultAsync(e => e.Id == id);
            }
            return await _context.Set<T>().FindAsync(id);

        }


        public async Task AddAsync(T model)
        {
            await _context.Set<T>().AddAsync(model);
        }


        public void Update(T model)
        {
            _context.Set<T>().Update(model);
        }

        public void Delete(T model)
        {
            _context.Set<T>().Remove(model);
        }
        

    }
}
