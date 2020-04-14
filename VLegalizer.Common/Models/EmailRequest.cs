using System.ComponentModel.DataAnnotations;

namespace VLegalizer.Common.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
