using System;

namespace VLegalizer.Common.Models
{
    public class TripDetailResponse
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        public string Description { get; set; }

        public int Amount { get; set; }

        public string PicturePath { get; set; }

        public string ExpenseType { get; set; }

        //TODO: replace the correct URL for the image
        public string ImageFullPath => string.IsNullOrEmpty(PicturePath)
            ? null
            : $"https://TDB.azurewebsites.net{PicturePath.Substring(1)}";

        public TripResponse Trip { get; set; }

    }
}
