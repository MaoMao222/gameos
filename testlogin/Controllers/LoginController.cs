using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testlogin.EFModels;

namespace testlogin.Controllers
{
    public class LoginController : Controller
    {
        bozhong_dbEntities db = new bozhong_dbEntities();
        // GET: Login
        public ActionResult Index()
        {
            Session.Clear();
            Session.Abandon();
            return View();
        }
        public ActionResult Login()
        {
            Session.Clear();
            Session.Abandon();
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string um = fc["username"];
            string pw = fc["password"];
            if (um != "" && pw != "")
            {
                try
                {
                    var user1 = db.game_user_account.Where(a => a.Accounts == um && a.LogonPass == pw);
                    if (user1.Count() > 0)
                    {

                        Session["username"] = um;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "账号密码错误");
                        ViewBag.LoginState = "error";
                        return Redirect(Url.Action("Index", "Login"));
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("",ex.ToString());
                    return Redirect(Url.Action("Index", "Login"));
                }
            }
            else
            {
                ModelState.AddModelError("", "账号密码不能为空");
                return Redirect(Url.Action("Index", "Login"));
            }
            //return View();
        }
        public ActionResult LoginOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}