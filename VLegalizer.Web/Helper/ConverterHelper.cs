using System.Collections.Generic;
using System.Linq;
using VLegalizer.Common.Models;
using VLegalizer.Web.Data.Entities;

namespace VLegalizer.Web.Helper
{
    public class ConverterHelper : IConverterHelper
    {


        public List<TripResponse> ToTripResponse(List<TripEntity> tripEntities)
        {
            List<TripResponse> list = new List<TripResponse>();
            foreach (TripEntity tripEntity in tripEntities)
            {
                list.Add(ToTripResponse(tripEntity));
            }

            return list;
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

        private ExpenseTypeResponse ToExpenseTypeResponse(ExpenseTypeEntity expenseType)
        {
            if (expenseType == null)
            {
                return null;
            }



            return new ExpenseTypeResponse
            {
                Id = expenseType.Id,
                ExpenseNames = expenseType.ExpenseNames
            };
        }

        public TripDetailResponse ToTripDetailResponse(TripDetailEntity tripDetailEntity)
        {
            return new TripDetailResponse
            {
                Id = tripDetailEntity.Id,
                Date = tripDetailEntity.Date,
                Amount = tripDetailEntity.Amount,
                Description = tripDetailEntity.Description,
                PicturePath = tripDetailEntity.PicturePath,
                ExpenseType = ToExpenseTypeResponse(tripDetailEntity.ExpenseType)
            };
        }

        public TripResponse ToTripResponse(TripEntity tripEntity)
        {
            return new TripResponse
            {
                Id = tripEntity.Id,
                City = tripEntity.City,
                StartDate = tripEntity.StartDate,
                EndDate = tripEntity.EndDate,
                Employee = ToEmployeeResponse(tripEntity.Employee),
                TripDetails = tripEntity.TripDetails?.Select(td => new TripDetailResponse
                {
                    Id = td.Id,
                    Date = td.Date,
                    Amount = td.Amount,
                    PicturePath = td.PicturePath,
                    Description = td.Description,
                    ExpenseType = ToExpenseTypeResponse(td.ExpenseType),
                }).ToList()
            };
        }

    }

}
