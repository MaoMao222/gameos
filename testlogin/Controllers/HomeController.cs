using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using testlogin.EFModels;

namespace testlogin.Controllers
{
    public class HomeController : Controller
    {
        bozhong_dbEntities db = new bozhong_dbEntities();
        public ActionResult Index()
        {
            string uname = Session["username"].ToString();

            game_user_account n = (from d in db.game_user_account where d.Accounts == uname select d).FirstOrDefault();
            //List<game_user_account> list = (from d in db.game_user_account where d.Accounts == uname select d).ToList();
            ////return View();   
            //ViewData["datalist"] = list;
             
            return View(n);
        }

        


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}