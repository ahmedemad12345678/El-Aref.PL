using El_Aref.BLL.Interfaces;
using El_Aref.DAL.Data.Contexts;
using El_Aref.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Aref.BLL.Repositores
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {
        private readonly ElAreffDbContext _context;

        public GenaricRepository(ElAreffDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T? Get(int id)
        {
            return _context.Set<T>().Find(id);

        }


        public int Add(T model)
        {
            _context.Set<T>().Add(model);
            return _context.SaveChanges();
        }


        public int Update(T model)
        {
            _context.Set<T>().Update(model);
            return _context.SaveChanges();
        }

        public int Delete(T model)
        {
            _context.Set<T>().Remove(model);
            return _context.SaveChanges();
        }

    }
}
