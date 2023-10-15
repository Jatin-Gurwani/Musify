using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Musify_web.Models
{
    [Table("Song")]
    public class Song
    {
        [Key]
        public int Song_id { get; set; }
        [Display(Name = "Song Name")]
        public string Song_name { get; set; }
        [Display(Name ="Artist name")]
        public int Artist_id { get; set; }
       
        [Display(Name ="Album Name")]
        public int Album_id { get; set; }
      
        [Display(Name ="Mood Type")]
        public int Genre_id { get; set; }
        [Display(Name ="Song upload ")]
        public string Song_link { get; set; }
        [NotMapped]
        public HttpPostedFileBase Song_file { get; set; }
    }
}