using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testlogin.Models
{
    public class LogAllModel
    {
        public int logid { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
        public Nullable<System.DateTime> logtime { get; set; }
        public string serverid { get; set; }
        public string ip { get; set; }
        public string code { get; set; }
    }
}