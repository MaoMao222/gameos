using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testlogin.EFModels;

namespace testlogin.Models
{
    public class UserModel
    {
        public game_user_account gua { get; set; }
        //public int id { get; set; }
        public string use_ingot { get; set; }
        public int paiming { get; set; }
        public int UserID { get; set; }
       // public static int idx { get; set; } = 0;
        
    }
}