using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using testlogin.Handlers;
using testlogin.Models;

namespace testlogin.Controllers
{
    public class WeiXinController : Controller
    {

        private readonly WeiXinApi _WeiXinConfig = null;
        public WeiXinController()
        {
            _WeiXinConfig = new WeiXinApi();
        }
        //[Authorize]
        public ActionResult Index()
        {
            IndexModel Model = new IndexModel();
            string openid = Request.RequestContext.HttpContext.User.Identity.Name;

            string OrderNo = _WeiXinConfig.GenerateOutTradeNo();
            PaymentData payData = new PaymentData();
            payData.SetValue("attach", OrderNo);
            payData.SetValue("body", "微信在线支付");
            payData.SetValue("out_trade_no", OrderNo);
            payData.SetValue("trade_type", "JSAPI");
            payData.SetValue("total_fee", 1);//1分钱
            payData.SetValue("openid", openid);
            //payData.SetValue("spbill_create_ip", "127.0.0.1");
            payData.SetValue("notify_url", _WeiXinConfig.NOTIFY_URL);
            PaymentData returnData = _WeiXinConfig.UnifiedOrder(payData);
            string prepay_id = "";
            string returnCode = returnData.GetValue("return_code").ToString();

            if (returnCode.ToUpper().Equals("SUCCESS"))
            {
                #region 获取支付签名
                //获取支付ID
                prepay_id = returnData.GetValue("prepay_id").ToString();
                PaymentData jsApiParam = new PaymentData();
                string NonceStr = _WeiXinConfig.GenerateNonceStr();
                string TimeStamp = _WeiXinConfig.GenerateTimeStamp();
                jsApiParam.SetValue("appId", _WeiXinConfig.AppId);
                jsApiParam.SetValue("timeStamp", TimeStamp);
                jsApiParam.SetValue("nonceStr", NonceStr);
                jsApiParam.SetValue("package", "prepay_id=" + prepay_id);
                jsApiParam.SetValue("signType", "MD5");
                Model.AppId = _WeiXinConfig.AppId;
                Model.TimeStamp = TimeStamp;
                Model.NonceStr = NonceStr;
                Model.PrepayID = prepay_id;
                Model.PaySign = jsApiParam.MakeSign(_WeiXinConfig.SignKey);
                Model.Success = true;
                #endregion

            }
            else
            {
                Model.Success = false;
                Model.ErrorMessage = returnData.GetValue("return_msg").ToString();
            }
            return View(Model);
        }

        /// <summary>
        /// 微信登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            //授权获取OpenId
            string RedirectUrl = "http://111.230.146.113/WeiXin/CallBack";//跳回的页面地址
            //参数详情参考微信官网
            string sUrl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=SCOPE&state=STATE#wechat_redirect", _WeiXinConfig.AppId, RedirectUrl);
            return Redirect(sUrl);
        }
        /// <summary>
        /// 接收微信返回页面
        /// </summary>
        /// <param name="Code">从微信服务器获取的code</param>
        /// <param name="state">自定义数据</param>
        /// <returns></returns>
        public ActionResult CallBack(string code, string state)
        {
            WebClient client = new WebClient();
            string sUrl = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", _WeiXinConfig.AppId, _WeiXinConfig.AppSecret, code);
            //根据code获取OpenId
            string ReturnString = client.DownloadString(sUrl);

            Dictionary<string, string> dicValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(ReturnString);
            if (dicValues.Keys.Contains("errcode"))
            {
                //输出错误信息
                return Content(dicValues["errmsg"]);
            }
            string OpenId = dicValues["openid"];
            //简单处理登录
            System.Web.Security.FormsAuthentication.SetAuthCookie(OpenId, true);

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Notify(FormCollection form)
        {
            StreamReader reader = new StreamReader(Request.InputStream, Encoding.UTF8);
            string postStr = reader.ReadToEnd();
            reader.Close();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(postStr);
            string ReturnCode = doc.SelectSingleNode("//result_code").InnerText;//获取支付结果代码
            StringBuilder xmlStr = new StringBuilder();
            if (ReturnCode.ToUpper().Equals("SUCCESS"))
            {
                try
                {
                    //获取订单号
                    string strOrderNo = doc.SelectSingleNode("//out_trade_no").InnerText;
                    //获取微信支付流水号
                    string weChatTransaction_Id = doc.SelectSingleNode("//transaction_id").InnerText;
                }
                catch (Exception ex)
                {
                    //记录错误
                    throw ex;
                }

                #region 通知微信后台支付成功
                Response.ContentType = "text/xml";
                xmlStr.AppendLine("<xml>");
                xmlStr.AppendFormat("<return_code><![CDATA[SUCCESS]]></return_code>");
                xmlStr.AppendFormat("<return_msg><![CDATA[OK]]></return_msg>");
                xmlStr.AppendFormat("</xml>");
                #endregion
            }
            else
            {
                #region 通知微信后台支付失败
                Response.ContentType = "text/xml";
                xmlStr.AppendLine("<xml>");
                xmlStr.AppendFormat("<return_code><![CDATA[FAIL]]></return_code>");
                xmlStr.AppendFormat("<return_msg><![CDATA[支付失败]]></return_msg>");
                xmlStr.AppendFormat("</xml>");
                #endregion
            }
            return Content(xmlStr.ToString());
        }

        /// <summary>
        /// 微信退款
        /// </summary>
        /// <param name="OrderNo">订单号</param>
        /// <returns></returns>
        [Authorize]
        public JsonResult Refund()
        {

            try
            {
                PaymentData payData = new PaymentData();
                //原订单号
                string OrderNo = "20306516";
                //原订单金额1分
                int OrderTotal = 1;
                //操作用户ID
                int CustomerId = 12;
                string WeChatTransaction_Id = "";
                //商户自定义退款单号
                string refund_no = _WeiXinConfig.GenerateOutTradeNo();

                //微信生成的订单号，在支付通知中有返回
                payData.SetValue("transaction_id", WeChatTransaction_Id);
                //商户侧传给微信的订单号,原支付的订单号, transaction_id、out_trade_no 二选一都行
                payData.SetValue("out_trade_no", OrderNo);
                //商户自定义退款单号
                payData.SetValue("out_refund_no", refund_no);
                //原订单总金额
                payData.SetValue("total_fee", OrderTotal);
                //退款金额
                payData.SetValue("refund_fee", OrderTotal);//退款1分
                payData.SetValue("op_user_id", CustomerId);
                PaymentData returnData = _WeiXinConfig.Refund(payData);
                string return_code = returnData.GetValue("return_code").ToString();
                if (return_code.ToUpper().Equals("FAIL"))
                {
                    return Json(new { Success = false, ErrorMessage = returnData.GetValue("return_msg").ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string result_code = returnData.GetValue("result_code").ToString();
                    if (result_code.ToUpper().Equals("FAIL"))
                    {
                        return Json(new { Success = false, ErrorMessage = returnData.GetValue("err_code_des").ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { Success = true, ErrorMessage = "", Data = returnData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 微信企业付款
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        public JsonResult Transfers()
        {

            try
            {
                //付款金额1元，最少1元
                int Amount = 100;
                //自定义交易订单号
                string TradeNo = _WeiXinConfig.GenerateOutTradeNo();
                //用户OpenId
                string OpenId = Request.RequestContext.HttpContext.User.Identity.Name;
                //校验用户姓名选项 NO_CHECK：不校验真实姓名  FORCE_CHECK：强校验真实姓名（未实名认证的用户会校验失败，无法转账） OPTION_CHECK：针对已实名认证的用户才校验真实姓名（未实名认证用户不校验，可以转账成功）
                string check_name = "NO_CHECK";
                //企业付款描述信息
                string desc = "微信转账";
                //收款用户真实姓名。 如果check_name设置为FORCE_CHECK或OPTION_CHECK，则必填用户真实姓名
                string re_user_name = "张三";
                PaymentData payData = new PaymentData();
                payData.SetValue("amount", Amount);
                payData.SetValue("partner_trade_no", TradeNo);
                payData.SetValue("openid", OpenId);
                payData.SetValue("check_name", check_name);
                payData.SetValue("desc", desc);
                payData.SetValue("re_user_name", re_user_name);
                PaymentData returnData = _WeiXinConfig.Transfers(payData);
                string return_code = returnData.GetValue("return_code").ToString();
                if (return_code.ToUpper().Equals("FAIL"))
                {
                    return Json(new { Success = false, ErrorMessage = returnData.GetValue("return_msg").ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string result_code = returnData.GetValue("result_code").ToString();
                    if (result_code.ToUpper().Equals("FAIL"))
                    {
                        return Json(new { Success = false, ErrorMessage = returnData.GetValue("err_code_des").ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { Success = true, ErrorMessage = "", Data = returnData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}