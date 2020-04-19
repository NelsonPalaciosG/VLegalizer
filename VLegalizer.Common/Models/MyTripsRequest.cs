using System;
using System.Collections.Generic;
using System.Text;

namespace VLegalizer.Common.Models
{
    public class MyTripsRequest
    {
        public string Email { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
