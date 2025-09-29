using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Aref.DAL.Model
{
    public class Department : BaseEntity
    {
        public string? Code { get; set; }
        public string Name { get; set; } = string.Empty;

        public DateTime CraeteAt { get; set; }
    }
}
