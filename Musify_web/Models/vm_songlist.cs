using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Musify_web.Models
{
    public class vm_songlist
    {
        public string Song_name { get; set; }
        public string Album_name { get; set; }
        public string Artist_name { get; set; }
        public string Genre_name { get; set; }
        public string Song_link { get; set; }
        public string Album_img { get; set; }
        public int Album_id { get; set; }
        public int Song_id { get; set; }

    }
}