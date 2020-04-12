using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VLegalizer.Web.Data.Entities
{
    public class ExpenseTypeEntity
    {
        public int Id { get; set; }

        [Display(Name = "Expense name's")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string ExpenseNames { get; set; }

        public ICollection<TripDetailEntity> TripDetails { get; set; }
    }
}
