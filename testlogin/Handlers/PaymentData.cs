using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;

namespace testlogin.Handlers
{
    public class PaymentData
    {
        private SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();
        public PaymentData() { }

        public void SetValue(string key, object value)
        {
            m_values[key] = value;
        }
        public object GetValue(string key)
        {
            if (m_values.ContainsKey(key))
            {
                return m_values[key];
            }
            return null;
        }
        public bool IsSet(string key)
        {
            return m_values.ContainsKey(key);
        }
        public string ToXml()
        {
            //数据为空时不能转化为xml格式
            if (0 == m_values.Count)
            {
                throw new Exception("PaymentData数据为空!");
            }
            StringBuilder xmlString = new StringBuilder();

            xmlString.AppendLine("<xml>");
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                //字段值不能为null，会影响后续流程
                if (pair.Value == null)
                {
                    throw new Exception("PaymentData内部含有值为null的字段!");
                }
                if (pair.Value.GetType() == typeof(int))
                {
                    xmlString.AppendFormat(" <{0}>{1}</{0}>", pair.Key, pair.Value);
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xmlString.AppendFormat(" <{0}><![CDATA[{1}]]></{0}>", pair.Key, pair.Value);
                }
                else//除了string和int类型不能含有其他数据类型
                {
                    throw new Exception("PaymentData字段数据类型错误!");
                }
            }
            xmlString.AppendLine("</xml>");
            return xmlString.ToString();
        }

        public SortedDictionary<string, object> FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new Exception("将空的xml串转换为WxPayData不合法!");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }
            try
            {
                if (m_values["return_code"].ToString() != "SUCCESS")
                {
                    return m_values;
                }
                //CheckSign();//验证签名,不通过会抛异常
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return m_values;
        }
        public string ToUrl()
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value == null)
                {
                    throw new Exception(pair.Key + "值WxPayData内部含有值为null的字段!");
                }

                if (pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }

        public string MakeSign(string KEY)
        {
            //转url格式
            string str = ToUrl();
            //在string后加入API KEY
            str += "&key=" + KEY;
            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }

        public bool CheckSign()
        {
            //如果没有设置签名，则跳过检测
            if (!IsSet("sign"))
            {
                throw new Exception("WxPayData签名存在但不合法!");
            }
            //如果设置了签名但是签名为空，则抛异常
            else if (GetValue("sign") == null || GetValue("sign").ToString() == "")
            {
                throw new Exception("WxPayData签名存在但不合法!");
            }

            //获取接收到的签名
            //string return_sign = GetValue("sign").ToString();
            return true;
        }

        /**
        * @获取Dictionary
        */
        public SortedDictionary<string, object> GetValues()
        {
            return m_values;
        }
    }
}