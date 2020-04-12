﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VLegalizer.Web.Data.Entities
{
    public class TripDetailEntity
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        public string Description { get; set; }

        public int Amount { get; set; }

        [Display(Name = "Picture")]
        public string PicturePath { get; set; }

        public TripEntity Trip { get; set; }

        //public virtual ExpenseTypeEntity ExpenseType { get; set; }

        public ExpenseTypeEntity ExpenseType { get; set; }
    }
}
