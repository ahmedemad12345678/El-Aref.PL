using El_Aref.BLL.Interfaces;
using El_Aref.BLL.Repositores;
using El_Aref.DAL.Data.Contexts;
using El_Aref.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_Areff.Comapny.BLL.Repositories
{
    public class DepartmentRepository : GenaricRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ElAreffDbContext context) : base(context)
        {

        }
    }
}
