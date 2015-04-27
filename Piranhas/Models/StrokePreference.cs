using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Piranhas.Models
{
    public class StrokePreference
    {
        public int StrokePreferenceID { get; set; }
        public bool Butterfly { get; set; }
        public bool Backstroke { get; set; }
        public bool Breaststroke { get; set; }
        public bool Freestyle { get; set; }
        [Display(Name="Individual Medley")]
        public bool IndividualMedley { get; set; }
    }
}