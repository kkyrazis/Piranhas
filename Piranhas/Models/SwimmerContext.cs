using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Piranhas.Models
{
    public class SwimmerContext : DbContext
    {
        public SwimmerContext() : base("DefaultConnection") { }
        public DbSet<Swimmer> Swimmers { get; set; }
        public DbSet<StrokePreference> StrokePreferences { get; set; }
    }
}