using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testlogin.Models
{
    public class IndexModel
    {
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 支付Id
        /// </summary>
        public string PrepayID { get; set; }
        /// <summary>
        /// 支付签名
        /// </summary>
        public string PaySign { get; set; }

        /// <summary>
        /// 返回页面
        /// </summary>
        public string ReturnUrl { get; set; }

        public bool Success { get; set; }


        public string ErrorMessage { get; set; }

    }
}