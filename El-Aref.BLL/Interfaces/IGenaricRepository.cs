using El_Aref.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Aref.BLL.Interfaces
{
    public interface IGenaricRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T? Get(int id);
        int Add(T model);
        int Update(T model);
        int Delete(T model);
    }
}
