using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Musify_web.Models
{
    [Table("Artist")]
    public class Artist
    {
        [Key]
        public int Artist_id { get; set; }
        [Display(Name = "Artist Name")]
        public String Artist_name { get; set; }

    }
}