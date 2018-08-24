using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(testlogin.Startup))]
namespace testlogin
{
    public partial class Startup
    {


        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           
        }

        //public void Configuration(IAppBuilder app)
        //{
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            // New code:
            // app.Run(context =>
            //  {
            //    context.Response.ContentType = "text/plain";
            //     return context.Response.WriteAsync("Hello, world.");
            // });

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Account/Login"),
            //    CookieSecure = CookieSecureOption.SameAsRequest,
            //    ExpireTimeSpan = TimeSpan.FromMinutes(30),//30分钟后过期
            //    SlidingExpiration = true,//当用户保持访问网站的时候再过特定时间（不访问）则失效
            //});
       // }
    }
}
