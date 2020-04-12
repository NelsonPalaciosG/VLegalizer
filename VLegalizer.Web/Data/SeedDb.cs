using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLegalizer.Common.Enums;
using VLegalizer.Web.Data.Entities;
using VLegalizer.Web.Helper;

namespace VLegalizer.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext dataContext,
                      IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            EmployeeEntity admin = await CheckEmployeeAsync("1026146086", "Nelson", "Palacios", "nelsonpalacios98055@correo.itm.edu.co", "230 34 60", "300 678 56 51", "Calle 45 A 63 B 31", UserType.Admin);
            EmployeeEntity employee1 = await CheckEmployeeAsync("1026146086", "Nelson", "Palacios", "nelpaga1126@gmail.com", "230 34 60", "300 678 56 53", "Calle 45 A 63 B 31", UserType.Employee);
            EmployeeEntity employee2 = await CheckEmployeeAsync("4040", "Juan", "Zuluaga", "nelpaga@hotmail.com", "350 634 2747", "300 678 56 54", "Calle Luna Calle Sol", UserType.Employee);
            await CheckExpenseTypesAsync();
            await CheckTripAsync();

        }


        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Employee.ToString());
        }

        private async Task CheckExpenseTypesAsync()
        {
            if (!_dataContext.ExpenseTypes.Any())
            {
                //_dataContext.ExpenseTypes.Add(new ExpenseTypeEntity { ExpenseNames = "Alojamiento" });
               //_dataContext.ExpenseTypes.Add(new ExpenseTypeEntity { ExpenseNames = "Alimentación" });
               // _dataContext.ExpenseTypes.Add(new ExpenseTypeEntity { ExpenseNames = "Bus" });
                _dataContext.ExpenseTypes.Add(new ExpenseTypeEntity { ExpenseNames = "Taxi" });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<EmployeeEntity> CheckEmployeeAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string fixedphone,
            string address,
            UserType userType)
        {
            EmployeeEntity user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new EmployeeEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    FixedPhone = fixedphone,
                    Address = address,
                    Document = document,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckTripAsync()
        {
            EmployeeEntity user = await _userHelper.GetUserByEmailAsync("nelpaga1126@gmail.com");
            if (!_dataContext.Trips.Any())
            {
                _dataContext.Trips.Add(

                new TripEntity
                {
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(4),
                    City = "Bogotá",
                    Employee = user,
                    TripDetails = new List<TripDetailEntity>
                    {
                        new TripDetailEntity
                        {
                            Date = DateTime.UtcNow,
                            Description = "Primera noche en Bogotá",
                            Amount = 1500000,
                            PicturePath = "",
                            ExpenseType = new ExpenseTypeEntity
                            {
                                ExpenseNames = "Alojamiento"
                            }

                        },
                        new TripDetailEntity
                        {
                            Date = DateTime.UtcNow,
                            Description = "Almuerzo",
                            Amount = 200000,
                            PicturePath = "",
                            ExpenseType = new ExpenseTypeEntity
                            {
                                ExpenseNames = "Alimetanción"
                            }

                        },
                        new TripDetailEntity
                        {
                            Date = DateTime.UtcNow,
                            Description = "Trayectos día",
                            Amount = 30000,
                            PicturePath = "",
                            ExpenseType = new ExpenseTypeEntity
                            {
                                ExpenseNames = "Bus"
                            }

                        }
                    }
                }
                );

                await _dataContext.SaveChangesAsync();
            }
        }



    }
}
