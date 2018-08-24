using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testlogin.EFModels;

namespace testlogin.Controllers
{
    public class ProxyController : Controller
    {
        bozhong_dbEntities db = new bozhong_dbEntities();
        SuperLogin_dbEntities ldb = new SuperLogin_dbEntities();
        // GET: Proxy
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult personal()
        {
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();

                int id = Convert.ToInt32(Session["aid"]);
                web_admin n = (from d in ldb.web_admin where d.id == id select d).FirstOrDefault();
                ViewData["admin"] = n;
                Session["userid"] = n.userid;

                game_user_account gua = (from f in db.game_user_account where f.UserID == n.userid select f).FirstOrDefault();
                ViewData["user"] = gua;

                log_gamelogin lg = (from c in db.log_gamelogin where c.userid == n.userid orderby c.logid descending select c).FirstOrDefault();
                ViewData["gamelogin"] = lg;
                web_admin f_n = (from d in ldb.web_admin where d.id == n.up_agentid select d).FirstOrDefault();
                ViewData["f_admin"] = f_n;
                var erzi = (from e in db.game_user_account where e.bind_number == n.invitecode select e).ToList();
                ViewBag.erzi = erzi.Count();
                if (n.level < 3)
                {
                    var erzi_sanji = (from e in ldb.web_admin where e.up_agentid == n.userid&&e.level==3 select e).ToList();
                    ViewBag.erzi_sanji = 0;
                    ViewBag.erzi_erji = 0;
                    if (erzi_sanji.Count > 0)
                    {
                        ViewBag.erzi_sanji = erzi_sanji.Count();
                    }
                    if (n.level == 1)
                    {
                        var erzi_erji = (from e in ldb.web_admin where e.up_agentid == n.userid && e.level == 2 select e).ToList();
                        if(erzi_erji.Count > 0)
                        {
                            ViewBag.erzi_erji = erzi_erji.Count();
                        }
                    }
                }
                

                return View(n);

            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }
        public ActionResult Rates()
        {
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                if (ViewBag.level != "9")
                {
                    ViewBag.error = "你没有权限修改";
                    return RedirectToAction("Login", "SuperLogin");
                }
                t_rates tr = ldb.t_rates.Find(1);
                if (tr == null)
                {
                    return HttpNotFound();
                }
                ViewData["bl"] = tr;
                //ViewBag.yiji = tr.rate1.ToString();
                //ViewBag.erji = tr.rate2.ToString();
                //ViewBag.sanji = tr.rate3.ToString();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rates([Bind(Include = "id,rate1,rate2,rate3,rate1to2,rate1to3,rate2to3")] t_rates gca)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (ModelState.IsValid)
            {
                ldb.Entry(gca).State = System.Data.Entity.EntityState.Modified;
                ldb.SaveChanges();
                return RedirectToAction("Rates", "Proxy");
            }
            return View(gca);
        }

        public ActionResult Scroll()
        {
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                game_config_email gce = (from d in db.game_config_email where d.id == 3 select d).FirstOrDefault();
                return View(gce);
            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Scroll([Bind(Include = "id,title,detail,type")] game_config_email gca)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (ModelState.IsValid)
            {
                db.Entry(gca).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Scroll","Proxy");
            }
            return View(gca);
        }

        public ActionResult IngotAdd()
        {
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }

        }
        [HttpGet]
        public ActionResult IngotAdd(int ? UserID)
        {
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                game_user_account gua = (from a in db.game_user_account where a.UserID == UserID select a).FirstOrDefault();
                ViewData["User"] = gua;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }
        [HttpPost]
        public ActionResult IngotAdd(FormCollection f)
        {
            int uid = Convert.ToInt32(f["UserID"]);
            int gold = 0;
            int ingot = 0;
            if(f["gold"] != "")
            {
                gold = Convert.ToInt32(f["gold"]);
            }
            if (f["ingot"] != "")
            {
                ingot = Convert.ToInt32(f["ingot"]);
            }             
            
            string remake = "管理员:"+ Session["username"].ToString()+"进行的操作，备注为："+f["remake"];

            game_user_account gua = (from a in db.game_user_account where a.UserID == uid select a).FirstOrDefault();

            int ingot_num = ingot + Convert.ToInt32(gua.Ingot);
            int gold_num = gold + Convert.ToInt32(gua.Gold); ;

            string query1 = @"UPDATE game_user_account SET Ingot = @Ingot ,Gold = @Gold WHERE UserID =@uid;";
            MySqlParameter[] paras1 = new MySqlParameter[]
                            {
                                new MySqlParameter("@uid",uid),
                                new MySqlParameter("@Ingot",ingot_num),
                                new MySqlParameter("@Gold",gold_num)
                            };
            try
            {
                db.Database.ExecuteSqlCommand(query1, paras1);
                
            }
            catch
            {

            }
            if(ingot != 0)
            {
                int reason=7;
                if (ingot < 0)
                {
                    reason = 8;
                }
                string qquery = @"INSERT INTO log_ingot_detail(ingot_num,kind_id,reason,user_id,create_time,after_ingot,before_ingot,remark) values (@num, 101,@reason,@uid,@create_time,@after_ingot,@before_ingot,@remake);";
                MySqlParameter[] pparas = new MySqlParameter[]
                                {
                                new MySqlParameter("@uid",uid),
                                new MySqlParameter("@num",ingot),
                                new MySqlParameter("@reason",reason),
                                new MySqlParameter("@create_time",DateTime.Now),
                                new MySqlParameter("@after_ingot",ingot_num),
                                new MySqlParameter("@before_ingot",Convert.ToInt32(gua.Ingot)),
                                new MySqlParameter("@remake",remake)
                                };
                db.Database.ExecuteSqlCommand(qquery, pparas);
            }
            

            return RedirectToAction("IngotDetail", "SuperAdmin");
        }
    }
}