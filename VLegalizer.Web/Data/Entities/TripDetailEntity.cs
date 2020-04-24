using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace VLegalizer.Web.Data.Entities
{
    public class TripDetailEntity
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}",
        ApplyFormatInEditMode = false)]
        public int Amount { get; set; }

        [Display(Name = "Picture")]
        public string PicturePath { get; set; }

        public TripEntity Trip { get; set; }

        public ExpenseTypeEntity ExpenseType { get; set; }
        //TODO: replace the correct URL for the image
        public string ImageFullPath => string.IsNullOrEmpty(PicturePath)
            ? "https://vlegalizerwebpalaciosgal.azurewebsites.net//images//Expenses/Alojamiento.jpg"
            : $"https://TDB.azurewebsites.net{PicturePath.Substring(1)}";
    }
}
