using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranhas.Models
{
    public class SwimmerViewModel
    {
        public SwimmerViewModel(Swimmer Swimmer, StrokePreferenceViewModel StrokePreferenceViewModel)
        {
            this.Swimmer = Swimmer;
            this.StrokePreferenceViewModel = StrokePreferenceViewModel;
        }
        public Swimmer Swimmer { get; set; }
        public StrokePreferenceViewModel StrokePreferenceViewModel { get; set; }
    }
}