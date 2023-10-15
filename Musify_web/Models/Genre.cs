using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Musify_web.Models
{
    [Table("Genre")]
    public class Genre
    {
        [Key]
        public int Genre_id { get; set; }
        public String Genre_name { get; set; }
    }
}