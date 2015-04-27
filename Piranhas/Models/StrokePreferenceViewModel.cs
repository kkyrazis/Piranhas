using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranhas.Models
{
    public class StrokePreferenceViewModel
    {
        public StrokePreferenceViewModel(StrokePreference pref, int pos)
        {
            this.StrokePreference = pref;
            this.Position = pos;
        }

        public StrokePreferenceViewModel()
        {
            this.Position = 0;
        }

        public StrokePreference StrokePreference { get; set; }
        public int Position { get; set; }
    }
}