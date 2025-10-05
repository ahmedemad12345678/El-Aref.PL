using El_Aref.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Aref.DAL.Data.Configration
{
    public class EmployeeConfigration : IEntityTypeConfiguration<Employee>
    {
        void IEntityTypeConfiguration<Employee>.Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Salary).HasColumnType("decimal(18,2)");
            builder.HasOne(E=>E.Department)
                   .WithMany(E=>E.Employees)
                   .HasForeignKey(E=>E.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
