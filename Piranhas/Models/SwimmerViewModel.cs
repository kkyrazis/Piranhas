using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranhas.Models
{
    public class SwimmerViewModel
    {
        public SwimmerViewModel(Swimmer Swimmer, StrokePreference StrokePreference)
        {
            this.Swimmer = Swimmer;
            this.StrokePreference = StrokePreference;
        }
        public Swimmer Swimmer { get; set; }
        public StrokePreference StrokePreference { get; set; }
    }
}