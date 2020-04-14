using System.Collections.Generic;
using VLegalizer.Common.Enums;

namespace VLegalizer.Common.Models
{
    public class EmployeeResponse
    {
        public string Id { get; set; }

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FixedPhone { get; set; }

        public string CellPhone { get; set; }

        public string Address { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public UserType UserType { get; set; }

        public List<TripResponse> Trips { get; set; }

    }
}
