using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testlogin.EFModels;
using PagedList;
using System.Configuration;

namespace testlogin.Controllers
{
    public class AdminController : Controller
    {
        bozhong_dbEntities db = new bozhong_dbEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                string uname = Session["username"].ToString();

                //int id = Session["aid"];
                game_user_account n = (from d in db.game_user_account where d.Accounts == uname select d).FirstOrDefault();
                Session["userid"] = n.UserID;
                //List<game_user_account> list = (from d in db.game_user_account where d.Accounts == uname select d).ToList();
                ////return View();   
                //ViewData["datalist"] = list;
                return View(n);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult SelectUser()
        {
            if (Session["userid"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                int uid = (int)Session["userid"];
                game_user_account nn = (from dd in db.game_user_account where dd.UserID == uid select dd).FirstOrDefault();
                return View(nn);
            }
            else
            {

                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult LogIngotDetail(int? page)
        {
            if (Session["userid"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                int uid = (int)Session["userid"];
                var list = (from d in db.log_ingot_detail where d.user_id == uid select d);

                int pageNumber = page ?? 1;

                int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
                list = list.OrderBy(x => x.order_id);
                IPagedList<log_ingot_detail> pagedList = list.ToPagedList(pageNumber, pageSize);

                //ViewData["datalist"] = list;
                return View(pagedList);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult LogAll(int? page)
        {
            if (Session["userid"] != null)
            {
                ViewBag.uname = Session["username"].ToString();              
                int uid = (int)Session["userid"];
                /*List<log_gamelogin>*/var list = (from d in db.log_gamelogin  where d.userid == uid select d);

                int pageNumber = page ?? 1;

                int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
                list = list.OrderBy(x => x.logid);
                IPagedList<log_gamelogin> pagedList = list.ToPagedList(pageNumber, pageSize);

                //ViewData["logdatalist"] = pagedList;
                return View(pagedList);
            }
            else
            {
                
                return RedirectToAction("Index", "Login");
            }
        }

        

    }
}