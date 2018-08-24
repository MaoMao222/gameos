using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testlogin.EFModels;

namespace testlogin.Models
{
    public class OrderModel
    {        
        public game_user_account gua { get; set; }

        public game_config_product gcp { get; set; }

        public log_game_order lgo { get; set; }
        public string order_status { get; set; }
        public string order_money { get; set; }
        public string pay_type { get; set; }
        public string userid { get; set; }
        public string product_name { get; set; }
        public string pay_money { get; set; }
        public string real_money { get; set; }
        public string out_orderno { get; set; }
        public string pay_time { get; set; }
        public Nullable<System.DateTime> create_time { get; set; }
        public string ingot_num { get; set; }

    }
}