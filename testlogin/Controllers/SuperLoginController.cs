using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testlogin.EFModels;

namespace testlogin.Controllers
{
    public class SuperLoginController : Controller
    {
        SuperLogin_dbEntities db = new SuperLogin_dbEntities();
        // GET: SuperLogin
        public ActionResult Index()
        {
            Session.Clear();
            Session.Abandon();
            return View();
        }

        public ActionResult Login(int? mess)
        {
            if (mess == 1)
            {
                ModelState.AddModelError("", "账号密码错误");
                
                ViewBag.LoginState = "error";
                Session.Clear();
                Session.Abandon();
                return View();
            }
            else
            {
                Session.Clear();
                Session.Abandon();
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string um = fc["username"].Trim();
            string pw = fc["password"].Trim();
            if (um != "" && pw != "")
            {
                try
                {
                    var user1 = db.web_admin.Where(a => a.username == um && a.password == pw);
                    if (user1.Count() > 0)
                    {
                        Session["logstate"] = "ok";
                        Session["username"] = um;
                        Console.WriteLine("ok");
                        return RedirectToAction("Index", "SuperAdmin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "账号密码错误");
                        ViewBag.LoginState = "error";
                        return Redirect(Url.Action("Login", "SuperLogin"));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                    return Redirect(Url.Action("Login", "SuperLogin"));
                }
            }
            else
            {
                //ModelState.AddModelError("", "账号密码错误");
                //ViewBag.LoginState = "error";
                return RedirectToAction("Login", "SuperLogin", new { er = 1 });
            }

        }
        public ActionResult LoginOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "SuperLogin");
        }
    }
}