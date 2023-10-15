using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Musify_web.Models
{
    [Table("album")]
    public class album
    {
        [Key]
        public int Album_id { get; set; }
        [Display(Name = "Album Name")]
        public string Album_name { get; set; }
        [Display(Name = "Album Year ")]
        [DataType(DataType.Date, ErrorMessage = "re-enter")]
        public DateTime Album_year { get; set; }
        [Display(Name = " Album Cover ")]
        public string Album_img { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }


    }
}