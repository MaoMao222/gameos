using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;

namespace System.Web.Mvc
{
            /// <summary>  
        /// HtmlHelper for My97DatePicker  
        /// </summary>  
        public static class My97DatePicker
        {
            private static string defaultFormat = "yyyy-MM-dd";

            /// <summary>  
            /// 使用特定的名称生成控件  
            /// </summary>  
            /// <param name="helper">HtmlHelper对象</param>  
            /// <param name="name">控件名称</param>  
            /// <returns>Html文本</returns>  
            public static string Calendar(this HtmlHelper helper, string name)
            {
                return Calendar(helper, name, defaultFormat);
            }


            /// <summary>  
            /// 使用特定的名称生成控件  
            /// </summary>  
            /// <param name="helper">HtmlHelper对象</param>  
            /// <param name="name">控件名称</param>  
            /// <param name="format">显示格式</param>  
            /// <returns>Html文本</returns>  
            public static string Calendar(this HtmlHelper helper, string name, string format)
            {
                return GenerateHtml(name, null, format);

            }

            /// <summary>  
            /// 使用特定的名称和初始值生成控件  
            /// </summary>  
            /// <param name="helper">HtmlHelper对象</param>  
            /// <param name="name">控件名称</param>  
            /// <param name="date">要显示的日期时间</param>  
            /// <returns>Html文本</returns>  
            public static string Calendar(this HtmlHelper helper, string name, DateTime date)
            {
                return Calendar(helper, name, date, defaultFormat);
            }

            /// <summary>  
            /// 使用特定的名称和初始值生成控件  
            /// </summary>  
            /// <param name="helper">HtmlHelper对象</param>  
            /// <param name="name">控件名称</param>  
            /// <param name="date">要显示的日期时间</param>  
            /// <param name="format">显示格式</param>  
            /// <returns>Html文本</returns>  
            public static string Calendar(this HtmlHelper helper, string name, DateTime date, string format)
            {
                return GenerateHtml(name, date, format);
            }

            /// <summary>  
            /// 通过lambda表达式生成控件  
            /// </summary>  
            /// <param name="helper">HtmlHelper对象</param>  
            /// <param name="expression">lambda表达式，指定要显示的属性及其所属对象</param>  
            /// <returns>Html文本</returns>  
            public static string CalendarFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
            {
                return CalendarFor(helper, expression, defaultFormat);
            }



            /// <summary>  
            /// 通过lambda表达式生成控件  
            /// </summary>  
            /// <param name="helper">HtmlHelper对象</param>  
            /// <param name="expression">lambda表达式，指定要显示的属性及其所属对象</param>  
            /// <param name="format">显示格式</param>  
            /// <returns>Html文本</returns>  
            public static string CalendarFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string format)
            {
                string name = ExpressionHelper.GetExpressionText(expression);
                DateTime value;
                object data = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, helper.ViewData).Model;

                if (data != null && DateTime.TryParse(data.ToString(), out value))
                {
                    return GenerateHtml(name, value, format);
                }
                else
                {
                    return GenerateHtml(name, null, format);
                }
            }



            /// <summary>  
            /// 通过lambda表达式获取要显示的日期时间  
            /// </summary>  
            /// <param name="helper">HtmlHelper对象</param>  
            /// <param name="expression">lambda表达式，指定要显示的属性及其所属对象</param>  
            /// <param name="format">显示格式</param>  
            /// <returns>Html文本</returns>  
            public static string CalendarDisplayFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string format)
            {

                string name = ExpressionHelper.GetExpressionText(expression);

                DateTime value;



                object data = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, helper.ViewData).Model;

                if (data != null && DateTime.TryParse(data.ToString(), out value))
                {

                    return value.ToString(format);
                }
                else
                {
                    return string.Empty;

                }

            }



            /// <summary>  
            /// 通过lambda表达式获取要显示的日期时间  
            /// </summary>  
            /// <param name="helper">HtmlHelper对象</param>  
            /// <param name="expression">lambda表达式，指定要显示的属性及其所属对象</param>  
            /// <returns>Html文本</returns>  
            public static string CalendarDisplayFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
            {

                return CalendarDisplayFor(helper, expression, defaultFormat);

            }


            /// <summary>  
            /// 生成输入框的Html  
            /// </summary>  
            /// <param name="name">calendar的名称</param>  
            /// <param name="date">calendar的值</param>  
            /// <returns>html文本</returns>  
            private static string GenerateHtml(string name, DateTime? date, string format)
            {
                if (date != null)
                {
                    return "<input type=\"text\" id=\"" + name + "\"  onclick=\"WdatePicker({el:'" + name + "'})\" class=\"Wdate\" value=\"" + date.Value.ToString(format) + "\" />";
                }
                else
                {
                    return "<input type=\"text\" id=\"" + name + "\"  onclick=\"WdatePicker({el:'" + name + "'})\" class=\"Wdate\" value=\"\" />";
                }
            }
        }
    }
