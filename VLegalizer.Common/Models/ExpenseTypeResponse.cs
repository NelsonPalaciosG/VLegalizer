using System.Collections.Generic;

namespace VLegalizer.Common.Models
{
    public class ExpenseTypeResponse
    {
        public int Id { get; set; }

        public string ExpenseNames { get; set; }

        public List<TripDetailResponse> TripDetails { get; set; }

    }
}
