using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Piranhas.Models
{
    public class Swimmer
    {
        public int SwimmerID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Birthdate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int StrokePreferenceID { get; set; }
        public string UserID { get; set; }
    }
}