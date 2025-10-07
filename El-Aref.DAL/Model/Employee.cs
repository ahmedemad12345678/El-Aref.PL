using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Aref.DAL.Model
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime HiringDate { get; set; } = DateTime.Now;
        public DateTime CreateAt { get; set; } = DateTime.Now;

        [DisplayName("Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public string? ImageName { get; set; }
    }
}
