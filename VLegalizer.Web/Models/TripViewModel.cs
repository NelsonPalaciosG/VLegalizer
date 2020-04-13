using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VLegalizer.Web.Models
{
    public class TripViewModel
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime StartDateLocal => StartDate.ToLocalTime();

        public DateTime EndDate { get; set; }

        public DateTime EndDateLocal => EndDate.ToLocalTime();

        public string City { get; set; }

        public string EmployeeName { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}",
        ApplyFormatInEditMode = false)]
        public int TotalAmount { get; set; }
    }
}
