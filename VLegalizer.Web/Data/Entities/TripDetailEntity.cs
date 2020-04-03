﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VLegalizer.Web.Data.Entities
{
    public class TripDetailEntity
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        public int Amount { get; set; }

        public string Description { get; set; }

        [Display(Name = "Picture")]
        public string PicturePath { get; set; }
        
        public ICollection<TripEntity> Trip { get; set; }

        public ICollection<ExpenseTypeEntity> ExpenseTypes { get; set; }
    }
}
