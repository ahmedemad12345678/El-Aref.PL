using El_Aref.BLL.Interfaces;
using El_Aref.BLL.Repositores;
using El_Aref.DAL.Data.Contexts;
using El_Aref.DAL.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_Areff.Comapny.BLL.Repositories
{
    public class EmployeeRepository : GenaricRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ElAreffDbContext context) : base(context)
        {
        }



    }
}
