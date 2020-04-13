using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VLegalizer.Web.Data.Entities
{
    public class TripEntity
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}}", ApplyFormatInEditMode = false)]
        public DateTime StartDate { get; set; }

        public DateTime StartDateLocal => StartDate.ToLocalTime();

        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}}", ApplyFormatInEditMode = false)]
        public DateTime EndDate { get; set; }

        public DateTime EndDateLocal => EndDate.ToLocalTime();

        public string City { get; set; }

        public EmployeeEntity Employee { get; set; }

        public ICollection<TripDetailEntity> TripDetails { get; set; }
    }
}
