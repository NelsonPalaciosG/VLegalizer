using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VLegalizer.Web.Data.Entities;

namespace VLegalizer.Web.Data
{
    public class DataContext : IdentityDbContext<EmployeeEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<EmployeeEntity> Employees { get; set; }

        public DbSet<TripEntity> Trips { get; set; }

        public DbSet<TripDetailEntity> TripDetails { get; set; }

        public DbSet<ExpenseTypeEntity> ExpenseTypes { get; set; }


    }
}
