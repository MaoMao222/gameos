using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testlogin.Models
{
    public class IngotDetailModel
    {
        public int id { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
        public string ingot_num { get; set; }
        public string reason { get; set; }
        public string after_ingot { get; set; }
        public string before_ingot { get; set; }
        public string remark { get; set; }
        public Nullable<System.DateTime> create_time { get; set; }
    }
}