using System.Collections.Generic;
using System.Linq;
using VLegalizer.Common.Models;
using VLegalizer.Web.Data.Entities;

namespace VLegalizer.Web.Helper
{
    public class ConverterHelper : IConverterHelper
    {
       /* public TripResponse ToTripResponse(TripEntity tripEntity)
        {
            return new TripResponse
            {
                Id = tripEntity.Id,
                StartDate = tripEntity.StartDate,
                EndDate = tripEntity.EndDate,
                City = tripEntity.City,
                TripDetails = tripEntity.TripDetails?.Select(td => new TripDetailResponse
                {
                    Id = td.Id,
                    Date = td.Date,
                    Description = td.Description,
                    Amount = td.Amount,
                    PicturePath = td.PicturePath,
                    ExpenseType = td.ExpenseType.ExpenseNames
                }).ToList(),
                Employee = ToEmployeeResponse(tripEntity.Employee)
            };

        }*/
        public List<TripResponse> ToTripResponse(List<TripEntity> tripEntity)
        {
            return tripEntity.Select(t => new TripResponse
            {
                Id = t.Id,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                City = t.City,

                Employee = ToEmployeeResponse(t.Employee)
            }).ToList();
        }

        public EmployeeResponse ToEmployeeResponse(EmployeeEntity employee)
        {
            if (employee == null)
            {
                return null;
            }

            return new EmployeeResponse
            {
                Document = employee.Document,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                FixedPhone = employee.FixedPhone,
                CellPhone = employee.CellPhone,
                Address = employee.Address,
                UserType = employee.UserType
            };
        }
    }

}
