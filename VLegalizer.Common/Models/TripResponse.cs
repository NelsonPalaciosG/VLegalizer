    using System;
using System.Collections.Generic;

namespace VLegalizer.Common.Models
{
    public class TripResponse
    {

        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime StartDateLocal => StartDate.ToLocalTime();

        public DateTime EndDate { get; set; }

        public DateTime EndDateLocal => EndDate.ToLocalTime();

        public string City { get; set; }

        public EmployeeResponse Employee { get; set; }

        public List<TripDetailResponse> TripDetails { get; set; }

    }
}
