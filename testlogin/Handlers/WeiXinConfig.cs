using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testlogin.Handlers
{
    public class WeiXinApi
    {
        /// <summary>
        /// 微信分配的AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 公众号的appsecret
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 当前用户IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }
        /// <summary>
        /// 签名Key
        /// </summary>
        public string SignKey { get; set; }

        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public string CertPath { get; set; }

        /// <summary>
        /// 证书密码,windows上可以直接双击导入系统，导入过程中会提示输入证书密码，证书密码默认为您的商户ID
        /// </summary>
        public string CertPassword { get; set; }


        /// <summary>
        /// 支付结果通知回调url，用于商户接收支付结果
        /// </summary>
        public string NOTIFY_URL { get; set; }

        public WeiXinApi()
        {
            this.AppId = "wx4b9be79db187a45b";
            this.AppSecret = "8f6cd89b38adadb7e8a70d239af29e44";
            this.Ip = "127.0.0.1";
            this.MchId = "MchId";
            this.SignKey = "支付签名";
            this.CertPath = "证书路径";
            this.CertPassword = "1497589962";
            this.NOTIFY_URL = "http://hemiaowangluo.com/";
        }



        #region 公用方法

        /**
        * 根据当前系统时间加随机序列来生成订单号
         * @return 订单号
        */
        public string GenerateOutTradeNo()
        {
            var ran = new Random();
            return string.Format("{0}{1}{2}", MchId, DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }

        /**
        * 生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
         * @return 时间戳
        */
        public string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /**
        * 生成随机串，随机串包含字母或数字
        * @return 随机串
        */
        public string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        #endregion

        #region 微信接口操作


        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="inputObj">提交给申请退款API的参数</param>
        /// <param name="CertPath">证书路径</param>
        /// <param name="CertPassword">证书密码</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public PaymentData Refund(PaymentData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new Exception("退款申请接口中，out_trade_no、transaction_id至少填一个！");
            }
            else if (!inputObj.IsSet("out_refund_no"))
            {
                throw new Exception("退款申请接口中，缺少必填参数out_refund_no！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new Exception("退款申请接口中，缺少必填参数total_fee！");
            }
            else if (!inputObj.IsSet("refund_fee"))
            {
                throw new Exception("退款申请接口中，缺少必填参数refund_fee！");
            }
            else if (!inputObj.IsSet("op_user_id"))
            {
                throw new Exception("退款申请接口中，缺少必填参数op_user_id！");
            }

            inputObj.SetValue("appid", AppId);//公众账号ID
            inputObj.SetValue("mch_id", MchId);//商户号
            inputObj.SetValue("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign(SignKey));//签名
            string xml = inputObj.ToXml();
            string response = HttpService.Post(xml, url, true, timeOut, CertPath, CertPassword);
            //将xml格式的结果转换为对象以返回
            PaymentData result = new PaymentData();
            result.FromXml(response);
            return result;
        }



        /**
        * 
        * 统一下单
        * @param WxPaydata inputObj 提交给统一下单API的参数
        * @param int timeOut 超时时间
        * @throws WxPayException
        * @return 成功时返回，其他抛异常
        */
        public PaymentData UnifiedOrder(PaymentData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no"))
            {
                throw new Exception("缺少统一支付接口必填参数out_trade_no！");
            }
            else if (!inputObj.IsSet("body"))
            {
                throw new Exception("缺少统一支付接口必填参数body！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new Exception("缺少统一支付接口必填参数total_fee！");
            }
            else if (!inputObj.IsSet("trade_type"))
            {
                throw new Exception("缺少统一支付接口必填参数trade_type！");
            }

            //关联参数
            if (inputObj.GetValue("trade_type").ToString() == "JSAPI" && !inputObj.IsSet("openid"))
            {
                throw new Exception("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
            }
            if (inputObj.GetValue("trade_type").ToString() == "NATIVE" && !inputObj.IsSet("product_id"))
            {
                throw new Exception("统一支付接口中，缺少必填参数product_id！trade_type为JSAPI时，product_id为必填参数！");
            }

            //异步通知url未设置，则使用配置文件中的url
            if (!inputObj.IsSet("notify_url"))
            {
                inputObj.SetValue("notify_url", NOTIFY_URL);//异步通知url
            }


            inputObj.SetValue("appid", AppId);//公众账号ID
            inputObj.SetValue("mch_id", MchId);//商户号
            inputObj.SetValue("spbill_create_ip", Ip);//终端ip	  	    
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            //签名
            inputObj.SetValue("sign", inputObj.MakeSign(SignKey));
            string xml = inputObj.ToXml();
            string response = HttpService.Post(xml, url, false, timeOut, "", "");
            PaymentData result = new PaymentData();
            result.FromXml(response);
            return result;
        }

        /// <summary>
        /// 企业付款接口
        /// </summary>
        /// <param name="inputObj">参数</param>
        /// <param name="CertPath">证书路径</param>
        /// <param name="CertPassword">证书密码</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public PaymentData Transfers(PaymentData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";

            #region 检测必要参数

            //检测必填参数
            if (!inputObj.IsSet("partner_trade_no") && !inputObj.IsSet("partner_trade_no"))
            {
                throw new Exception("企业付款接口中，缺少必填参数partner_trade_no！");
            }
            if (!inputObj.IsSet("openid"))
            {
                throw new Exception("企业付款接口中，缺少必填参数openid！");
            }
            if (!inputObj.IsSet("check_name"))
            {
                throw new Exception("企业付款接口中，缺少必填参数check_name！");
            }
            else
            {
                string checkName = inputObj.GetValue("check_name").ToString();
                switch (checkName)
                {
                    case "FORCE_CHECK":
                    case "OPTION_CHECK":
                        if (!inputObj.IsSet("check_name"))
                        {
                            throw new Exception("企业付款接口中，缺少必填参数re_user_name！");
                        }
                        break;
                    default:
                        break;
                }
            }
            if (!inputObj.IsSet("amount"))
            {
                throw new Exception("企业付款接口中，缺少必填参数amount！");
            }
            if (!inputObj.IsSet("desc"))
            {
                throw new Exception("企业付款接口中，缺少必填参数desc！");
            }
            if (!inputObj.IsSet("spbill_create_ip"))
            {
                throw new Exception("企业付款接口中，缺少必填参数spbill_create_ip！");
            }
            #endregion

            #region 添加公共参数
            inputObj.SetValue("mch_appid", AppId);//公众账号ID
            inputObj.SetValue("mchid", MchId);//商户号
            inputObj.SetValue("spbill_create_ip", Ip);//随机字符串
            inputObj.SetValue("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign(SignKey));//签名 
            #endregion
            string xml = inputObj.ToXml();
            var start = DateTime.Now;
            string response = HttpService.Post(xml, url, true, timeOut, CertPath, CertPassword);
            PaymentData result = new PaymentData();
            result.FromXml(response);

            return result;
        }

        #endregion
    }
}