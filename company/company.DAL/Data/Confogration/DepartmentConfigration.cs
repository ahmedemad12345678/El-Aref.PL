﻿using company.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.DAL.Data.Confogration
{
    internal class DepartmentConfigration : IEntityTypeConfiguration<Department>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Department> builder)
        {
            builder.Property(D=>D.Id).UseIdentityColumn(10,10);
        }
    }
}
