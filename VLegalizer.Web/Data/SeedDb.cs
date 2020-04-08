using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLegalizer.Web.Data.Entities;

namespace VLegalizer.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;

        public SeedDb(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckExpenseTypesAsync();
            await CheckEmployeesAsync();

        }

        private async Task CheckExpenseTypesAsync()
        {
            if (!_dataContext.ExpenseTypes.Any())
            {
                _dataContext.ExpenseTypes.Add(new ExpenseTypeEntity { ExpenseNames = "Alojamiento" });
                _dataContext.ExpenseTypes.Add(new ExpenseTypeEntity { ExpenseNames = "Alimentación" });
                _dataContext.ExpenseTypes.Add(new ExpenseTypeEntity { ExpenseNames = "Bus" });
                _dataContext.ExpenseTypes.Add(new ExpenseTypeEntity { ExpenseNames = "Taxi" });
                await _dataContext.SaveChangesAsync();
            }
        }
        private async Task CheckEmployeesAsync()
        {
            if (!_dataContext.Employees.Any())
            {
                _dataContext.Employees.Add(new EmployeeEntity
                {
                    Document = "1026146085",
                    FirstName = "Nelson",
                    LastName = "Palacios",
                    FixedPhone = "2303460",
                    CellPhone = "3006785655",
                    Address = "Calle 45 A 63 B 31",
                    Trips = new List<TripEntity>
                    {
                        new TripEntity
                        {
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddDays(4),
                            TotalAmount = 0,
                            City = "Bogotá",
                            TripDetails = new List<TripDetailEntity>
                            {
                                new TripDetailEntity
                                {
                                    Date = DateTime.UtcNow,
                                    Description = "Primera noche en Bogotá",
                                    Amount = 200000,
                                    PicturePath = "",
                                    ExpenseTypes = new List<ExpenseTypeEntity>
                                    {
                                        new ExpenseTypeEntity
                                        {
                                            ExpenseNames = "Alojamiento"

                                        }
                                    }
                                }
                            }
                        },

                    }
                }
                );

                await _dataContext.SaveChangesAsync();
            }
        }



    }
}
