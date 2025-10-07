using El_Aref.BLL.Interfaces;
using El_Aref.BLL.Repositores;
using El_Aref.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;



namespace El_Aref.DAL.Data.Contexts
{
    public class EmployeeRepository : GenaricRepository<Employee>, IEmployeeRepository
    {
        private readonly ElAreffDbContext _context;

        public EmployeeRepository(ElAreffDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetByNameAsync(string name)
        {
            return await _context.Employees.Include(E=>E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}
