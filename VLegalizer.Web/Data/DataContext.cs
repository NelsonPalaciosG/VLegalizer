﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLegalizer.Web.Data.Entities;

namespace VLegalizer.Web.Data
{
    public class DataContext : IdentityDbContext<EmployeeEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<EmployeeEntity> Employees { get; set; }

    }
}
