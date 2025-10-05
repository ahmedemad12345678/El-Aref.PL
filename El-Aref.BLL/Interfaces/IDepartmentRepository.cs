using El_Aref.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Aref.BLL.Interfaces
{
    public interface IDepartmentRepository: IGenaricRepository<Department>
    {
        List<Department>? GetByName(string name);
    }
}
