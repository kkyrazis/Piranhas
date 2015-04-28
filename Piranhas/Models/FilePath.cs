using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Piranhas.Models
{
    public class FilePath
    {
        public int FilePathID { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(255)]
        public string FileTitle { get; set; }
        public int ArticleModelID { get; set; }
        public virtual ArticleModel Article { get; set; }
    }
}