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
    
    public partial class game_config_room
    {
        public int id { get; set; }
        public Nullable<int> gameid { get; set; }
        public string name { get; set; }
        public Nullable<int> roomid { get; set; }
        public Nullable<int> tablecount { get; set; }
        public Nullable<int> chaircount { get; set; }
        public string roomname { get; set; }
        public Nullable<int> roomtype { get; set; }
        public Nullable<int> roommode { get; set; }
        public string address { get; set; }
        public Nullable<int> port { get; set; }
        public string modulename { get; set; }
        public string ruleenter { get; set; }
        public string rulegame { get; set; }
        public string rulecustom { get; set; }
        public Nullable<int> status { get; set; }
    }
}
