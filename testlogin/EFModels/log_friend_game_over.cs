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
    
    public partial class log_friend_game_over
    {
        public int id { get; set; }
        public Nullable<int> code { get; set; }
        public string play_detail { get; set; }
        public Nullable<System.DateTime> logtime { get; set; }
        public Nullable<int> userid { get; set; }
        public Nullable<int> landlord { get; set; }
        public Nullable<int> kind_id { get; set; }
        public Nullable<int> room_type { get; set; }
        public Nullable<int> create_type { get; set; }
    }
}
