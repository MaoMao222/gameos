//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace testlogin.EFModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class game_user_address
    {
        public int user_id { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public string address { get; set; }
        public string address_ip { get; set; }
        public System.DateTime logtime { get; set; }
        public string mac { get; set; }
        public string session { get; set; }
        public Nullable<int> login_count { get; set; }
        public int promoter_id { get; set; }
        public string phone { get; set; }
    }
}
