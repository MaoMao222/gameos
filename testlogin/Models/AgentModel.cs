using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testlogin.EFModels;

namespace testlogin.Models
{
    public class AgentModel
    {
        public int id { get; set; }
        public web_admin wa { get; set; }
        public game_user_address uga { get; set; }
    }
}