using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Piranhas.Models
{
    public class ArticleModel
    {
        public int ArticleModelID { get; set; }
        [Display(Name="Post Title")]
        public String ArticleTitle { get; set; }
        [Display(Name="Post Data")]
        public String ArticleData { get; set; }
        public virtual ICollection<FilePath> FilePaths { get; set; } 
    }
}