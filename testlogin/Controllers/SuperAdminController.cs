using MySql.Data.MySqlClient;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testlogin.EFModels;
using testlogin.Models;

namespace testlogin.Controllers
{
    public class SuperAdminController : Controller
    {
        bozhong_dbEntities db = new bozhong_dbEntities();
        SuperLogin_dbEntities ldb = new SuperLogin_dbEntities();

        // GET: SuperAdmin
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                var g_username = Session["username"].ToString();
                ViewBag.uname = Session["username"].ToString();
                web_admin wad = (from d in ldb.web_admin where d.username == g_username select d).FirstOrDefault();
                var g_level = wad.level.ToString();
                Session["level"] = g_level;
                Session["aid"] = wad.id;
                ViewBag.level = Session["level"].ToString();
                ViewBag.money = wad.accumulated_income;

                var erzi = (from a in ldb.web_admin where a.parentid == wad.id select a).ToList();
                ViewBag.dlcount = erzi.Count().ToString();
               
                if(wad.invitecode < 10000)
                {
                    int num = wad.id + 10000;
                    
                    string query = @"UPDATE web_admin SET invitecode = @num WHERE id =@uid;";
                    MySqlParameter[] paras = new MySqlParameter[]
                    {
                                new MySqlParameter("@uid",wad.id),
                                new MySqlParameter("@num",num)
                    };
                    ldb.Database.ExecuteSqlCommand(query, paras);
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }

        public ActionResult LogOrderAll(int? page, string starttime, string endtime)
        {
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                /*List<log_gamelogin>*/
                System.ComponentModel.NullableConverter nullableDateTime = new System.ComponentModel.NullableConverter(typeof(DateTime?));
                var list = (from d in db.log_game_order
                            join s in db.game_config_product on d.product_id equals s.product_id 
                            join t in db.game_user_account on d.user_id equals t.UserID 
                            select new OrderModel
                            {
                                //order_status = d.order_status.ToString(),
                                //order_money = d.order_money.ToString(),
                                //pay_type = d.pay_type.ToString(),
                                //userid = d.user_id.ToString(),
                                //product_name = s.product_name,
                                //pay_money = d.pay_money.ToString(),
                                //real_money = d.real_money.ToString(),
                                //out_orderno = d.out_orderno,
                                //pay_time = d.pay_time.ToString(),
                                //create_time = d.create_time,
                                //ingot_num = d.ingot_num.ToString()

                                lgo =d,
                                create_time = d.create_time,
                                gcp = s,
                                gua = t
                            });

                if (starttime != "0")
                {
                    DateTime? start = (DateTime?)nullableDateTime.ConvertFromString(starttime);
                    DateTime? end = (DateTime?)nullableDateTime.ConvertFromString(endtime);
                    
                    list = (from d in db.log_game_order
                            join s in db.game_config_product on d.product_id equals s.product_id 
                            join t in db.game_user_account on d.user_id equals t.UserID 
                            where d.create_time >= start && d.create_time <= end
                            select new OrderModel
                            {
                                //order_status = d.order_status.ToString(),
                                //order_money = d.order_money.ToString(),
                                //pay_type = d.pay_type.ToString(),
                                //userid = d.user_id.ToString(),
                                //product_name = s.product_name,
                                //pay_money = d.pay_money.ToString(),
                                //real_money = d.real_money.ToString(),
                                //out_orderno = d.out_orderno,
                                //pay_time = d.pay_time.ToString(),
                                //create_time = d.create_time,
                                //ingot_num = d.ingot_num.ToString()
                                lgo = d,
                                create_time = d.create_time,
                                gcp = s,
                                gua = t
                            });
                }
                int pageNumber = page ?? 1;
                ViewBag.start = starttime;
                ViewBag.end = endtime;
                int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
                list = list.OrderByDescending(x => x.create_time);
                IPagedList<OrderModel> pagedList = list.ToPagedList(pageNumber, pageSize);

                //ViewData["logdatalist"] = pagedList;
                return View(pagedList);
            }
            else
            {

                return RedirectToAction("Login", "SuperLogin");
            }
        }
        [HttpPost]
        public ActionResult LogOrderAll(int? page, FormCollection collection)
        {
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                /*List<log_gamelogin>*/
                System.ComponentModel.NullableConverter nullableDateTime = new System.ComponentModel.NullableConverter(typeof(DateTime?));
                ViewBag.start = collection["start"].ToString();
                ViewBag.end = collection["end"].ToString();
                var list = (from d in db.log_game_order
                            join s in db.game_config_product on d.product_id equals s.product_id 
                            join t in db.game_user_account on d.user_id equals t.UserID 
                            select new OrderModel
                            {                              

                                lgo = d,
                                create_time = d.create_time,
                                gcp = s,
                                gua = t
                            });
                if(collection["start"].ToString()!="0"&& collection["end"].ToString() != "0")
                {
                    DateTime? start = (DateTime?)nullableDateTime.ConvertFromString(collection["start"].ToString());
                    DateTime? end = (DateTime?)nullableDateTime.ConvertFromString(collection["end"].ToString());
                    list = (from d in db.log_game_order
                            join s in db.game_config_product on d.product_id equals s.product_id 
                            join t in db.game_user_account on d.user_id equals t.UserID 
                            where d.create_time >= start && d.create_time <= end
                            select new OrderModel
                            {
                                //order_status = d.order_status.ToString(),
                                //order_money = d.order_money.ToString(),
                                //pay_type = d.pay_type.ToString(),
                                //userid = d.user_id.ToString(),
                                //product_name = s.product_name,
                                //pay_money = d.pay_money.ToString(),
                                //real_money = d.real_money.ToString(),
                                //out_orderno = d.out_orderno,
                                //pay_time = d.pay_time.ToString(),
                                //create_time = d.create_time,
                                //ingot_num = d.ingot_num.ToString()
                                lgo = d,
                                create_time = d.create_time,
                                gcp = s,
                                gua = t
                            });
                    ViewBag.start = start.ToString();
                    ViewBag.end = end.ToString();
                }
                
                int pageNumber = page ?? 1;

                int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
                list = list.OrderByDescending(x => x.create_time);
                IPagedList<OrderModel> pagedList = list.ToPagedList(pageNumber, pageSize);

                //ViewData["logdatalist"] = pagedList;
                return View(pagedList);
            }
            else
            {

                return RedirectToAction("Login", "SuperLogin");
            }
        }
        //public ActionResult LogAll(int page)
        //{
        //    if (Session["username"] != null)
        //    {
        //        ViewBag.uname = Session["username"].ToString();
        //        ViewBag.level = Session["level"].ToString();
        //        var list = (from d in db.log_gamelogin select d);
        //        int pageNumber = page;
        //        int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
        //        list = list.OrderByDescending(x => x.logtime);
        //        IPagedList<log_gamelogin> pagedList = list.ToPagedList(pageNumber, pageSize);

        //        return View(pagedList);
        //    }
        //    else
        //    {

        //        return RedirectToAction("Login", "SuperLogin");
        //    }
        //}
        public ActionResult LogAll(int? page,string starttime,string endtime)
        {
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                System.ComponentModel.NullableConverter nullableDateTime = new System.ComponentModel.NullableConverter(typeof(DateTime?));
                var list = (from gl in db.log_gamelogin join ua in db.game_user_account on gl.userid equals ua.UserID select new LogAllModel
                {
                    logid = gl.logid,
                    userid = gl.userid.ToString(),
                    username = ua.nick_name,
                    logtime = gl.logtime,
                    serverid = gl.serverid.ToString(),
                    ip = gl.clientip,
                    code = gl.code.ToString()
                });
                if (starttime !="0")
                {                                    
                    DateTime? start = (DateTime?)nullableDateTime.ConvertFromString(starttime);
                    DateTime? end = (DateTime?)nullableDateTime.ConvertFromString(endtime);
                    
                    list = (from gl in db.log_gamelogin join ua in db.game_user_account on gl.userid equals ua.UserID where gl.logtime >= start && gl.logtime <= end
                            select new LogAllModel
                            {
                                logid = gl.logid,
                                userid = gl.userid.ToString(),
                                username = ua.nick_name,
                                logtime = gl.logtime,
                                serverid = gl.serverid.ToString(),
                                ip = gl.clientip,
                                code = gl.code.ToString()
                            });
                }
                ViewBag.start = starttime;
                ViewBag.end = endtime;
                int pageNumber = page ?? 1;
                int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
                list = list.OrderByDescending(x => x.logtime);
                IPagedList<LogAllModel> pagedList = list.ToPagedList(pageNumber, pageSize);

                return View(pagedList);
            }
            else
            {

                return RedirectToAction("Login", "SuperLogin");
            }
        }
        [HttpPost]
        public ActionResult LogAll(int? page, FormCollection collection)
        {
            if (Session["username"] != null)
            {
                System.ComponentModel.NullableConverter nullableDateTime = new System.ComponentModel.NullableConverter(typeof(DateTime?));
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                var list = (from gl in db.log_gamelogin
                        join ua in db.game_user_account on gl.userid equals ua.UserID
                        select new LogAllModel
                        {
                            logid = gl.logid,
                            userid = gl.userid.ToString(),
                            username = ua.nick_name,
                            logtime = gl.logtime,
                            serverid = gl.serverid.ToString(),
                            ip = gl.clientip,
                            code = gl.code.ToString()
                        });
                ViewBag.start = collection["start"].ToString();
                ViewBag.end = collection["end"].ToString();
                if (collection["start"].ToString()!= "0")
                {
                    DateTime? start = (DateTime?)nullableDateTime.ConvertFromString(collection["start"].ToString());
                    DateTime? end = (DateTime?)nullableDateTime.ConvertFromString(collection["end"].ToString());
                    ViewBag.start = start.ToString();
                    ViewBag.end = end.ToString();
                    list = (from gl in db.log_gamelogin
                                join ua in db.game_user_account on gl.userid equals ua.UserID
                                where gl.logtime >= start && gl.logtime <= end
                                select new LogAllModel
                                {
                                    logid = gl.logid,
                                    userid = gl.userid.ToString(),
                                    username = ua.nick_name,
                                    logtime = gl.logtime,
                                    serverid = gl.serverid.ToString(),
                                    ip = gl.clientip,
                                    code = gl.code.ToString()
                                });
                }
                
                int pageNumber = page ?? 1;
                int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
                list = list.OrderByDescending(x => x.logtime);
                IPagedList<LogAllModel> pagedList = list.ToPagedList(pageNumber, pageSize);
                
                return View(pagedList);
            }
            else
            {

                return RedirectToAction("Login", "SuperLogin");
            }
            
        }

        public ActionResult Award()
        {
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                var list = (from d in db.game_config_award select d);
                
                list = list.OrderBy(x => x.id);
                
                return View(list.ToList());
                //return View();

            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }

        [ValidateInput(false)]
        public ActionResult AWEdit(int? id)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            game_config_award q1 = db.game_config_award.Find(id);
            if (q1 == null)
            {
                return HttpNotFound();
            }
            return View(q1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AWEdit([Bind(Include = "id,award_des,ingot,gold")] game_config_award gca)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (ModelState.IsValid)
            {
                db.Entry(gca).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Award");
            }
            return View(gca);
        }

        public ActionResult AWCreate()
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AWCreate([Bind(Include = "award_des,ingot,gold")] game_config_award gca)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            try
            {
                if (ModelState.IsValid)
                {
                    db.game_config_award.Add(gca);
                    db.SaveChanges();
                    return RedirectToAction("Award");
                }
            }
            catch(System.Data.DataException)
            {
                ModelState.AddModelError("无法保存", "保存出现错误，请联系毛");
            }
            return View(gca);
        }

        public ActionResult AWDelete(int id)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            try
            {
                game_config_award awd = new game_config_award() { id = id };
                db.Entry(awd).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Award", new { saveChangesError = true });
            
            }
            return RedirectToAction("Award");
        }

        public ActionResult Email(int? page)
        {
            
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                var list = (from d in db.game_config_email select d);
                int pageNumber = page ?? 1;
                int allcount = list.Count();
                int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);

                list = list.OrderBy(x => x.id);

                IPagedList<game_config_email> pagedList = list.ToPagedList(pageNumber, pageSize);

                return View(pagedList);
                //return View();

            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }

        public ActionResult EMDelete(int id)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            try
            {
                game_config_email emd = new game_config_email() { id = id };
                db.Entry(emd).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Email", new { id = id, saveChangesError = true });

            }
            return RedirectToAction("Email");
        }
        public ActionResult EMCreate()
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EMCreate([Bind(Include = "title,detail,type")] game_config_email gca)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            try
            {
                if (ModelState.IsValid)
                {
                    db.game_config_email.Add(gca);
                    db.SaveChanges();
                    return RedirectToAction("Email");
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("无法保存", "保存出现错误，请联系毛");
            }
            return View(gca);
        }

        public ActionResult EMEdit(int ? id)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            game_config_email q1 = db.game_config_email.Find(id);
            if (q1 == null)
            {
                return HttpNotFound();
            }
            return View(q1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EMEdit([Bind(Include = "id,title,detail,type")] game_config_email gca)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (ModelState.IsValid)
            {
                db.Entry(gca).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Email");
            }
            return View(gca);
        }



        public ActionResult Product()
        {
            
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                var list = (from d in db.game_config_product select d);
                //int pageNumber = page ?? 1;
                //int allcount = list.Count();
                //int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);

                list = list.OrderBy(x => x.id);

                //IPagedList<game_config_product> pagedList = list.ToPagedList(pageNumber, pageSize);

                return View(list.ToList());
                //return View();

            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }

        public ActionResult PDelete(int id)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            try
            {
                game_config_product emd = new game_config_product() { id = id };
                db.Entry(emd).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Product", new { id = id, saveChangesError = true });

            }
            return RedirectToAction("Product");
        }

        public ActionResult PCreate()
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PCreate([Bind(Include = "product_name,product_price,bind_buy_send,product_num,describe,product_id")] game_config_product gca)//白名单 Exclude：黑名单
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            try
            {
                gca.kind_id = 101;
                gca.key_word = "BoZhongQiPaiIOS";
                if (ModelState.IsValid)
                {
                    db.game_config_product.Add(gca);
                    db.SaveChanges();
                    return RedirectToAction("Product");
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("无法保存", "保存出现错误，请联系毛");
            }
            return View(gca);
        }

        public ActionResult Route()
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (Session["username"] != null)
            {
                var list = (from d in db.game_config_route select d);
                //int pageNumber = page ?? 1;
                //int allcount = list.Count();
                //int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);

                list = list.OrderBy(x => x.id);

                //IPagedList<game_config_route> pagedList = list.ToPagedList(pageNumber, pageSize);

                return View(list.ToList());
                //return View();

            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }

        public ActionResult RouteDelete(int id)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            try
            {
                game_config_route emd = new game_config_route() { id = id };
                db.Entry(emd).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Route", new { id = id, saveChangesError = true });

            }
            return RedirectToAction("Route");
        }

        public ActionResult ChangeGold(int? page)
        {
            
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                var list = (from d in db.game_config_change_gold select d);
                int pageNumber = page ?? 1;
                int allcount = list.Count();
                int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);

                list = list.OrderBy(x => x.id);

                IPagedList<game_config_change_gold> pagedList = list.ToPagedList(pageNumber, pageSize);

                return View(pagedList);
                //return View();

            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }

        public ActionResult ChangeGoldDelete(int id)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            try
            {
                game_config_change_gold emd = new game_config_change_gold() { id = id };
                db.Entry(emd).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("ChangeGold", new { id = id, saveChangesError = true });

            }
            return RedirectToAction("ChangeGold");
        }

        public ActionResult ChangeGoldCreate()
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeGoldCreate([Bind(Include = "ingot,gold,des")] game_config_change_gold gca)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            try
            {
                
                if (ModelState.IsValid)
                {
                    db.game_config_change_gold.Add(gca);
                    db.SaveChanges();
                    return RedirectToAction("ChangeGold");
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("无法保存", "保存出现错误，请联系毛");
            }
            return View(gca);
        }

        public ActionResult Room()
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (Session["username"] != null)
            {
                var list = (from d in db.game_config_room select d);
               
                return View(list.ToList());
                //return View();

            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }

        
        public ActionResult RoomEdit(int? id)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            game_config_room q1 = db.game_config_room.Find(id);
            if (q1 == null)
            {
                return HttpNotFound();
            }
            return View(q1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoomEdit([Bind(Include = "gameid,name,roomid,tablecount,chaircount,roomname,roomtype,roommode,address,port,modulename,ruleenter,rulegame,rulecustom,status")] game_config_room gca)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (ModelState.IsValid)
            {
                db.Entry(gca).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Room");
            }
            return View(gca);
        }
        public ActionResult RoomDelete(int id)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            try
            {
                game_config_room emd = new game_config_room() { id = id };
                db.Entry(emd).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Room", new { id = id, saveChangesError = true });

            }
            return RedirectToAction("Room");
        }

        public ActionResult RoomCreate()
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoomCreate([Bind(Include = "id,gameid,name,roomid,tablecount,chaircount,roomname,roomtype,roommode,address,port,modulename,ruleenter,rulegame,rulecustom,status")] game_config_room gca)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            try
            {

                if (ModelState.IsValid)
                {
                    db.game_config_room.Add(gca);
                    db.SaveChanges();
                    return RedirectToAction("Room");
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("无法保存", "保存出现错误，请联系毛");
            }
            return View(gca);
        }

        public ActionResult TestGame()
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (Session["username"] != null)
            {
                var list = (from d in db.game_config_test_game select d);
                
                return View(list.ToList());
                //return View();

            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }

        public ActionResult TestGameDelete(int id)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            try
            {
                game_config_test_game emd = new game_config_test_game() { user_id = id };
                db.Entry(emd).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("TestGame", new { id = id, saveChangesError = true });

            }
            return RedirectToAction("TestGame");
        }

        public ActionResult TestGameCreate()
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TestGameCreate([Bind(Include = "user_id,count,min_niu,max_niu")] game_config_test_game gca)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            try
            {

                if (ModelState.IsValid)
                {
                    db.game_config_test_game.Add(gca);
                    db.SaveChanges();
                    return RedirectToAction("TestGame");
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("无法保存", "保存出现错误，请联系毛");
            }
            return View(gca);
        }

        public ActionResult TestGameEdit(int? id)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            game_config_test_game q1 = db.game_config_test_game.Find(id);
            if (q1 == null)
            {
                return HttpNotFound();
            }
            return View(q1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TestGameEdit([Bind(Include = "user_id,count,min_niu,max_niu")] game_config_test_game gca)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (ModelState.IsValid)
            {
                db.Entry(gca).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TestGame");
            }
            return View(gca);
        }

        
        public ActionResult IngotDetail(int? page, int? userid, string starttime, string endtime)
        {
            
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                string query = "select sum(abs(ingot_num)) from log_ingot_detail where create_time >= date(now()) and create_time<DATE_ADD(date(now()),INTERVAL 1 DAY)";
                //db.Database.SqlQuery(typeof(int),query);
                List<int?> re = new List<int?>();
                re = db.Database.SqlQuery<int?>(query).ToList();
                //System.Data.Entity.Infrastructure.DbRawSqlQuery<int> result = db.Database.SqlQuery<int>(query);
                if(re.FirstOrDefault().ToString() != "")
                {
                    
                    ViewBag.todaysum = re.FirstOrDefault().ToString();
                }
                else
                {
                    ViewBag.todaysum = 0;
                }
                Session["xiaohaozuanshi"] = ViewBag.todaysum;
                System.ComponentModel.NullableConverter nullableDateTime = new System.ComponentModel.NullableConverter(typeof(DateTime?));
                var list = (from d in db.log_ingot_detail join ua in db.game_user_account on d.user_id equals ua.UserID select new IngotDetailModel
                {
                    id = d.id,
                    userid = d.user_id.ToString(),
                    username = ua.nick_name,
                    ingot_num = d.ingot_num.ToString(),
                    reason = d.reason.ToString(),
                    after_ingot = d.after_ingot.ToString(),
                    before_ingot = d.before_ingot.ToString(),
                    remark = d.remark,
                    create_time = d.create_time
                });
                if (starttime != "0")
                {
                    DateTime? start = (DateTime?)nullableDateTime.ConvertFromString(starttime);
                    DateTime? end = (DateTime?)nullableDateTime.ConvertFromString(endtime);

                    list = (from d in db.log_ingot_detail join ua in db.game_user_account on d.user_id equals ua.UserID where d.create_time >= start && d.create_time <= end
                            select new IngotDetailModel
                            {
                                id = d.id,
                                userid = d.user_id.ToString(),
                                username = ua.nick_name,
                                ingot_num = d.ingot_num.ToString(),
                                reason = d.reason.ToString(),
                                after_ingot = d.after_ingot.ToString(),
                                before_ingot = d.before_ingot.ToString(),
                                remark = d.remark,
                                create_time = d.create_time
                            });
                }
                ViewBag.start = starttime;
                ViewBag.end = endtime;
                int uidd = userid ?? 0;
                ViewBag.userid = userid;
                if (userid > 0)
                {
                    
                    string uid = userid.ToString();
                    list = (from d in list
                            where d.userid == uid
                            select d);
                }
                int pageNumber = page ?? 1;
                int allcount = list.Count();
                int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);

                list = list.OrderByDescending(x => x.id);
                
                IPagedList<IngotDetailModel> pagedList = list.ToPagedList(pageNumber, pageSize);

                return View(pagedList);
                //return View();

            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }
        [HttpPost]
        public ActionResult IngotDetail(int? page, FormCollection collection)
        {
            if (Session["username"] != null)
            {
                ViewBag.todaysum = Session["xiaohaozuanshi"];
                System.ComponentModel.NullableConverter nullableDateTime = new System.ComponentModel.NullableConverter(typeof(DateTime?));
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                ViewBag.start = collection["start"].ToString();
                ViewBag.end = collection["end"].ToString();
                ViewBag.userid = 0;
                var list = (from d in db.log_ingot_detail
                            join ua in db.game_user_account on d.user_id equals ua.UserID
                            select new IngotDetailModel
                            {
                                id = d.id,
                                userid = d.user_id.ToString(),
                                username = ua.nick_name,
                                ingot_num = d.ingot_num.ToString(),
                                reason = d.reason.ToString(),
                                after_ingot = d.after_ingot.ToString(),
                                before_ingot = d.before_ingot.ToString(),
                                remark = d.remark,
                                create_time = d.create_time
                            });
                if (collection["start"].ToString()!="0" && collection["end"].ToString() != "0")
                {
                    DateTime? start = (DateTime?)nullableDateTime.ConvertFromString(collection["start"].ToString());
                    DateTime? end = (DateTime?)nullableDateTime.ConvertFromString(collection["end"].ToString());
                    ViewBag.start = start.ToString();
                    ViewBag.end = end.ToString();
                    list = (from d in db.log_ingot_detail
                                join ua in db.game_user_account on d.user_id equals ua.UserID
                                where d.create_time >= start && d.create_time <= end
                                select new IngotDetailModel
                                {
                                    id = d.id,
                                    userid = d.user_id.ToString(),
                                    username = ua.nick_name,
                                    ingot_num = d.ingot_num.ToString(),
                                    reason = d.reason.ToString(),
                                    after_ingot = d.after_ingot.ToString(),
                                    before_ingot = d.before_ingot.ToString(),
                                    remark = d.remark,
                                    create_time = d.create_time
                                });
                }
                if(string.Empty != collection["userid"].ToString())
                {
                    string userid = collection["userid"].ToString();
                    ViewBag.userid = Convert.ToInt32(userid);
                    list = (from d in list
                            where d.userid == userid
                            select d);
                }

                int pageNumber = page ?? 1;
                int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
                list = list.OrderByDescending(x => x.create_time);
                IPagedList<IngotDetailModel> pagedList = list.ToPagedList(pageNumber, pageSize);

                return View(pagedList);
            }
            else
            {

                return RedirectToAction("Login", "SuperLogin");
            }
        }

        public ActionResult UserAccount()
        {
            
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                //List<game_user_account> list = new List<game_user_account>();
                //if (Session["level"].ToString() == "9")
                //{ list = (from d in db.game_user_account select d).ToList(); }

                //var list = (from d in db.game_user_account select d);

                //var list = (from d in db.game_user_account
                //            join lid in db.log_ingot_detail on d.UserID equals lid.user_id                            
                //            select new UserModel
                //            {
                //                gua = d

                //            });
                //var list = (from a in db.log_ingot_detail
                //            group new { userid = a.user_id, ingot = a.ingot_num } by a.user_id into ingotsum
                //            from m in ingotsum
                //            join a in db.game_user_account on m.userid equals a.UserID
                //            select new UserModel
                //            {
                //                UserID = a.UserID,
                //                gua = a,
                //                use_ingot = ingotsum.Sum(g => m.ingot).ToString()
                //            });
                var list = (from d in db.game_user_account
                            join ingotnum in
                            (from a in db.log_ingot_detail
                             where a.ingot_num < 0
                             group new { userid = a.user_id, a.ingot_num }
                             by new { a.user_id } into b
                             select new
                             {
                                 userid = b.Key.user_id,
                                 count = b.Sum(c => c.ingot_num)
                             }
                             )
                            on d.UserID equals ingotnum.userid into jg
                            from xx in jg.DefaultIfEmpty()
                            //where xx.count <0
                            orderby xx.count
                            select new UserModel
                            {
                                gua = d,
                                UserID = d.UserID,
                                use_ingot = (xx == null ? "0" : xx.count.ToString())
                            }).ToList();


                //var tt = list.OrderBy(p => p.use_ingot).Select((p, idx) => new UserModel
                //{
                //    paiming = idx,
                //    gua = p.gua,                    
                //    use_ingot = p.use_ingot
                //});
                //list = list.OrderBy(x => x.use_ingot);
                int xuhao = 1;
                foreach(var i in list)
                {
                    if(i.use_ingot == "0")
                    {
                        continue;
                    }
                    i.paiming = xuhao++;
                }

                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public ActionResult UserAccount(string id)
        //{
        //    ViewBag.uname = Session["username"].ToString();
        //    ViewBag.level = Session["level"].ToString();
        //    var n = (from d in db.game_user_account select d);
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        int uid = Convert.ToInt32(id);
        //        n = (from d in db.game_user_account where d.UserID == uid select d);
                

        //    }
        //    int pageNumber =  1;
        //    int allcount = n.Count();
        //    int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);

        //    n = n.OrderBy(x => x.UserID);

        //    IPagedList<game_user_account> pagedList = n.ToPagedList(pageNumber, pageSize);
        //    return View(pagedList);

            
        //}

        public ActionResult UserAccountEdit(int? id)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            game_user_account q1 = db.game_user_account.Find(id);
            if (q1 == null)
            {
                return HttpNotFound();
            }
            ViewData["User"] = q1;
            game_user_address q2 = db.game_user_address.Find(id);
            ViewData["User_add"] = q2;

            Session["gold"] = q1.Gold;
            Session["ingot"] = q1.Ingot;
            return View(q1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserAccountEdit(FormCollection f)
        {
            //[Bind(Include = "UserID,Gold,Ingot,zjtype,gitype,num,remake")] game_user_account gca
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            if (f["UserID"].ToString()!="")
            {               
                int uid =Convert.ToInt32(f["UserID"]);
                int bind_number = Convert.ToInt32(f["bind_number"]);
                string phone = f["phone"].ToString();
                string query = @"UPDATE game_user_account SET bind_number = @bind_number WHERE UserID =@uid;";
                MySqlParameter[] paras = new MySqlParameter[]
                {
                    new MySqlParameter("@uid",uid),
                    new MySqlParameter("@bind_number",bind_number)
                };
                db.Database.ExecuteSqlCommand(query, paras);

                string qquery = @"UPDATE game_user_address SET phone = @phone WHERE user_id =@uid;";
                                
                
                MySqlParameter[] pparas = new MySqlParameter[] 
                {
                    new MySqlParameter("@uid",uid),
                    new MySqlParameter("@phone",phone)
                };
                db.Database.ExecuteSqlCommand(qquery, pparas);



                //if (gtype == "gold")
                //{
                //    switch (atype)
                //    {
                //        case "add":
                //            num = Convert.ToInt32(Session["gold"]) + num;
                //            query = @"UPDATE game_user_account SET Gold = @num WHERE UserID =@uid;";
                //            paras = new MySqlParameter[]
                //            {
                //                new MySqlParameter("@uid",uid),
                //                new MySqlParameter("@num",num)
                //            };
                //            db.Database.ExecuteSqlCommand(query, paras);

                //            break;
                //        case "jian":
                //            num = Convert.ToInt32(Session["gold"]) - num;
                //            query = @"UPDATE game_user_account SET Gold = @num WHERE UserID =@uid;";
                //            paras = new MySqlParameter[]
                //            {
                //                new MySqlParameter("@uid",uid),
                //                new MySqlParameter("@num",num)
                //            };
                //            db.Database.ExecuteSqlCommand(query, paras);
                //            break;
                //    }

                //}
                //else
                //{
                //    switch (atype)
                //    {
                //        case "add":
                //            num = Convert.ToInt32(Session["ingot"]) + num;
                //            query = "UPDATE game_user_account SET Ingot = @num WHERE UserID =@uid";
                //            paras = new MySqlParameter[]
                //            {
                //                new MySqlParameter("@uid",uid),
                //                new MySqlParameter("@num",num)
                //            };
                //            db.Database.ExecuteSqlCommand(query, paras);
                //            qquery = @"INSERT INTO log_ingot_detail(ingot_num,kind_id,reason,user_id,create_time,after_ingot,before_ingot,remark) values (@num, 101,8,@uid,@create_time,@after_ingot,@before_ingot,@remake);";
                //            pparas = new MySqlParameter[]
                //            {
                //                new MySqlParameter("@uid",uid),
                //                new MySqlParameter("@num",Convert.ToInt32(f["num"])),
                //                new MySqlParameter("@create_time",dt),
                //                new MySqlParameter("@after_ingot",Convert.ToInt32(Session["ingot"])),
                //                new MySqlParameter("@before_ingot",num),
                //                new MySqlParameter("@remake",remake)                                                                
                //            };
                //            db.Database.ExecuteSqlCommand(qquery, pparas);
                //            break;
                //        case "jian":
                //            num = Convert.ToInt32(Session["ingot"]) - num;
                //            query = "UPDATE game_user_account SET Ingot = @num WHERE UserID =@uid";
                //            paras = new MySqlParameter[]
                //            {
                //                new MySqlParameter("@uid",uid),
                //                new MySqlParameter("@num",num)
                //            };
                //            db.Database.ExecuteSqlCommand(query, paras);
                //            qquery = @"INSERT INTO log_ingot_detail(ingot_num,user_id,create_time,after_ingot,before_ingot,remark) values (@num,@uid,@create_time,@after_ingot,@before_ingot,@remake);";
                //            pparas = new MySqlParameter[]
                //            {
                //                new MySqlParameter("@uid",uid),
                //                new MySqlParameter("@num",Convert.ToInt32(f["num"])),
                //                new MySqlParameter("@create_time",dt),
                //                new MySqlParameter("@after_ingot",Convert.ToInt32(Session["ingot"])),
                //                new MySqlParameter("@before_ingot",num),
                //                new MySqlParameter("@remake",remake)
                //            };
                //            db.Database.ExecuteSqlCommand(qquery, pparas);
                //            break;
                //    }
                //}

                //db.Entry(gca).State = System.Data.Entity.EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("UserAccount");
            }
            return RedirectToAction("UserAccount");
            
        }
        public ActionResult AgentAll(string lv)
        {
            if (Session["username"] != null)
            {
                ViewBag.uname = Session["username"].ToString();
                ViewBag.level = Session["level"].ToString();
                int aid = Convert.ToInt32(Session["aid"]);
               // var list = (from d in ldb.web_admin where d.parentid ==aid  select d);
                var list = (from d in ldb.web_admin
                            //join bl in db.game_user_address on d.userid equals bl.user_id
                            where d.parentid == aid
                       select new AgentModel
                       {
                           id = d.id,
                           wa = d
                           //uga = bl
                       });
                
                //if(lv != null && lv != "9")
                //{
                //    int pid = Convert.ToInt32(Session["aid"]);
                //    list = (from d in ldb.web_admin where d.parentid == pid select d);
                //}
                if(Session["username"].ToString().Trim().ToUpper().IndexOf("MAO")> -1)
                {
                    list = (from d in ldb.web_admin
                            //join bl in db.game_user_address on d.userid equals bl.user_id
                            where d.id != 2
                            select new AgentModel
                            {
                                id = d.id,
                                wa = d
                                //uga = bl
                            });
                    
                    //(from d in ldb.web_admin where d.id != 2 select d);
                }
                //int pageNumber = page ?? 1;
                //int allcount = list.Count();
                //int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);

                //list = list.OrderBy(x => x.id);

                //IPagedList<web_admin> pagedList = list.ToPagedList(pageNumber, pageSize);

                return View(list.ToList());
                //return View();

            }
            else
            {
                return RedirectToAction("Login", "SuperLogin");
            }
        }
        public ActionResult AgentCreate()
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList = new List<SelectListItem>()
                {
                new SelectListItem(){Value="1",Text="一级代理"},
                new SelectListItem(){Value="2",Text="二级代理"},
                new SelectListItem(){Value="3",Text="三级代理"},
                };
            if (Session["username"].ToString().Trim().ToUpper() == "MAO")
            {
                itemList = new List<SelectListItem>()
                {
                new SelectListItem(){Value="9",Text="管理员"},
                new SelectListItem(){Value="1",Text="一级代理"},
                new SelectListItem(){Value="2",Text="二级代理"},
                new SelectListItem(){Value="3",Text="三级代理"},
                };
            }
                        
            ViewBag.nowid = Session["aid"].ToString();
            ViewBag.select = itemList;

            return View();
        }

        //public ActionResult AgentCreate([Bind(Include = "parentid,username,password,userid,level,real_name,phone")] web_admin gca)
        //{
        //    ViewBag.uname = Session["username"].ToString();
        //    ViewBag.level = Session["level"].ToString();
        //    try
        //    {

        //        if (ModelState.IsValid)
        //        {                    
        //            ldb.web_admin.Add(gca);
        //            ldb.SaveChanges();
        //            return RedirectToAction("AgentAll", new { lv = ViewBag.level });
        //        }
        //    }
        //    catch (System.Data.DataException)
        //    {
        //        ModelState.AddModelError("无法保存", "保存出现错误，请联系毛");
        //    }
        //    return View(gca);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgentCreate(FormCollection f)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();

            string username = f["username"];
            string password = f["password"];
            string real_name = f["real_name"];
            string phone = f["phone"];
            string wechat = f["wechat"];
            string area = f["area"];

            int userid = Convert.ToInt32(f["userid"]);
            int level = Convert.ToInt32(f["level"]);            
            int parentid = Convert.ToInt32(f["parentid"]);
            int up_agentid = 0;
            int invitecode = 0;
            float accumulated_income = 0;
            float balance = 0;

            game_user_account gua = db.game_user_account.Find(userid);
            if (gua.bind_number > 0)
            {
                try
                {
                    web_admin wa = (from a in ldb.web_admin where a.invitecode == gua.bind_number select a).FirstOrDefault();
                    up_agentid = wa.id;
                }
                catch
                {

                }               
            }
            string query1 = @"INSERT INTO web_admin (username,`password`,`level`,userid,parentid,invitecode,phone,real_name,accumulated_income,balance,up_agentid,wechat,area) VALUES (@username,@password,@level,@userid,@parentid,@invitecode,@phone,@real_name,@accumulated_income,@balance,@up_agentid,@wechat,@area);";
            MySqlParameter[] paras1 = new MySqlParameter[]
                            {
                                new MySqlParameter("@username",username),
                                new MySqlParameter("@password",password),
                                new MySqlParameter("@level",level),
                                new MySqlParameter("@userid",userid),
                                new MySqlParameter("@parentid",parentid),
                                new MySqlParameter("@invitecode",invitecode),
                                new MySqlParameter("@accumulated_income",accumulated_income),
                                new MySqlParameter("@balance",balance),
                                new MySqlParameter("@phone",phone),
                                new MySqlParameter("@real_name",real_name),
                                new MySqlParameter("@wechat",wechat),
                                new MySqlParameter("@area",area),
                                new MySqlParameter("@up_agentid",up_agentid)

                            };
            ldb.Database.ExecuteSqlCommand(query1, paras1);

            return RedirectToAction("AgentAll", "SuperAdmin", new { lv = ViewBag.level });
        }

        public ActionResult AgentSelect(int? id)
        {
            if(id == null){
                return View();
            }
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();

            if(id > 0)
            {
                web_admin n = (from a in ldb.web_admin where a.id == id select a).FirstOrDefault();
                ViewData["e_admin"] = n;
                game_user_account gua = (from b in db.game_user_account where b.UserID == n.userid select b).FirstOrDefault();
                ViewData["e_user"] = gua;
                log_gamelogin lg = (from c in db.log_gamelogin where c.userid == n.userid orderby c.logid descending select c).FirstOrDefault();
                ViewData["e_gamelogin"] = lg;
                web_admin f_n = (from d in ldb.web_admin where d.id == n.up_agentid select d).FirstOrDefault();
                ViewData["e_f_admin"] = f_n;
                if(n.invitecode == 0)
                {
                    ViewBag.erzi = 0;
                }
                else
                {
                    var erzi = (from e in db.game_user_account where e.bind_number == n.invitecode select e).ToList();
                    ViewBag.erzi = erzi.Count();
                }
                
                return View();
            }
            else
            {
                return View("代理ID错误");
            }
        }

        public ActionResult AgentEdit(int ? did , int ? uid)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            web_admin n = (from a in ldb.web_admin where a.id == did select a).FirstOrDefault();
            ViewData["e_admin"] = n;
            game_user_account gua = (from b in db.game_user_account where b.UserID == uid select b).FirstOrDefault();
            ViewData["e_user"] = gua;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgentEdit(FormCollection f)
        {
            int aid = Convert.ToInt32(f["id"]);
            int uid = Convert.ToInt32(f["UserID"]);
            //int bind_number = Convert.ToInt32(f["bind_number"]);
            int Ingot = Convert.ToInt32(f["Ingot"]);
            int level = Convert.ToInt32(f["level"]);
            int bind_number = Convert.ToInt32(f["bind_number"]);
            string username = f["username"];
            string real_name = f["real_name"];
            string phone = f["phone"];
            string wechat = f["wechat"];
            string area = f["area"];

            web_admin wa = (from a in ldb.web_admin where a.invitecode == bind_number select a).FirstOrDefault();
            if (wa != null)
            {
                string query1 = @"UPDATE game_user_account SET bind_number = @bind_number WHERE UserID =@uid;";
                MySqlParameter[] paras1 = new MySqlParameter[]
                                {
                                new MySqlParameter("@uid",uid),
                                new MySqlParameter("@bind_number",bind_number)
                                    // new MySqlParameter("@bind_number",bind_number)
                                };
                db.Database.ExecuteSqlCommand(query1, paras1);
                string queery3 = @"UPDATE web_admin SET up_agentid = @up_agentid WHERE id =@aid;";
                MySqlParameter[] paras3 = new MySqlParameter[]
                                {
                                new MySqlParameter("@aid",aid),
                                new MySqlParameter("@up_agentid",wa.id)
                                    // new MySqlParameter("@bind_number",bind_number)
                                };
                ldb.Database.ExecuteSqlCommand(queery3, paras3);
            }         
            
            string query2 = @"UPDATE web_admin SET username = @username, real_name = @real_name ,phone = @phone ,level = @level,wechat = @wechat ,area=@area WHERE id =@aid;";
            MySqlParameter[] paras2 = new MySqlParameter[]
                           {
                                new MySqlParameter("@aid",aid),
                                new MySqlParameter("@username",username),
                                new MySqlParameter("@real_name",real_name),
                                new MySqlParameter("@phone",phone),
                                new MySqlParameter("@wechat",wechat),
                                new MySqlParameter("@area",area),
                                new MySqlParameter("@level",level)
                           };
            try
            {
                
                ldb.Database.ExecuteSqlCommand(query2, paras2);
            }
            catch
            {

            }
            return RedirectToAction("AgentSelect",new { id = aid });

        }

        public ActionResult AgentDelete(int id)
        {
            ViewBag.uname = Session["username"].ToString();
            ViewBag.level = Session["level"].ToString();
            try
            {
                web_admin emd = new web_admin() { id = id };
                ldb.Entry(emd).State = System.Data.Entity.EntityState.Deleted;
                ldb.SaveChanges();
            }
            catch
            {
                return RedirectToAction("AgentAll", new { lv = ViewBag.level, saveChangesError = true });

            }
            return RedirectToAction("AgentAll", new { lv = ViewBag.level });
        }
        [HttpPost]
        public ActionResult validate(string id)
        {
            int userid = Convert.ToInt32(id);
            var user1 = db.game_user_account.Where(a => a.UserID == userid);
            var admin = ldb.web_admin.Where(a => a.userid == userid);
            if (user1.Count() > 0)
            {
                if (admin.Count() > 0)
                {
                    return Content("2");
                }
                else { return Content("1"); }
                
            }
            else
            {
                return Content("0");
            }
            
        }
    }
}