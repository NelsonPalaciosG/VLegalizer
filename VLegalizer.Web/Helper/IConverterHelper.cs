using System.Collections.Generic;
using VLegalizer.Common.Models;
using VLegalizer.Web.Data.Entities;

namespace VLegalizer.Web.Helper
{
    public interface IConverterHelper
    {
        TripResponse ToTripResponse(TripEntity tripEntity);
       
        List<TripResponse> ToTripResponse(List<TripEntity> tripEntity);

        EmployeeResponse ToEmployeeResponse(EmployeeEntity employee);

        List<TripDetailResponse> ToTripDetailResponse(List<TripDetailEntity> tripEntity);

       // ExpenseTypeRequest ToExpenseTypeAsync(ExpenseTypeEntity expenseTypeEntity);
    }
}
