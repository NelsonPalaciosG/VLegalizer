using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace VLegalizer.Common.Models
{
    public class TripRequest
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public Guid EmployeeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string City { get; set; }


    }
}
