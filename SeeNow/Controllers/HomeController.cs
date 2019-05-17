using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeeNow.Models;
using System.Data.Entity;
using System.Net;
using System.Web.Security;



namespace SeeNow.Controllers
{
    public class HomeController : Controller
    {
        private SeeNowEntities db = new SeeNowEntities();

        public ActionResult Index()
        {
            var users = db.users.Include(u => u.profile).Include(u => u.role);
            return View(users.ToList());
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


        public ActionResult Registered()
        {
            ViewBag.profile = new SelectList(db.profile, "profile_id", "profile_path");
            ViewBag.role_id = new SelectList(db.role, "role_id", "role_desc");
            return View();
        }
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Registered([Bind(Include = "account,role_id,password,nick_name,e_mail,score,energy,profile_id,bag_number,lock_flag,validation_flag,resetable")] users users)
        public ActionResult Registered(string account,string role_id,string password,string nick_name,string e_mail,short profile_id)
        {
            users user = new users();
            user.account = account;
            user.role_id = role_id;
            user.password = password;
            user.nick_name = nick_name;
            user.e_mail = e_mail;
            user.profile_id = profile_id;

            db.users.Add(user);
            db.SaveChanges();
            ////if (ModelState.IsValid)
            ////{
            //    db.users.Add(users);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            ////}

            //ViewBag.profile = new SelectList(db.profile, "profile_id", "profile_path", users.profile_id);
            //ViewBag.role_id = new SelectList(db.role, "role_id", "role_desc", users.role_id);
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            //ViewBag.profile_id = new SelectList(db.profile, "profile_id", "profile_name");
            ViewBag.role_id = new SelectList(db.role, "role_id", "role_desc");
            return View();
        }
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string account,string role_id,string password)
        {
            //檢查必填欄位
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("accout", "請輸入必填欄位");
                ModelState.AddModelError("password", "請輸入必填欄位");
                return View();
            }
            //把輸入的密碼加密
            //string base64PD = Convert.ToBase64String(Encoding.UTF8.GetBytes(PD));

            //todo 和DB裡的資料做比對
            var user = db.users.Where(m => m.account == account).FirstOrDefault();
            if (user != null)
            {
                string pwd = user.password;

                if (pwd == password)
                {
                    //登入成功 
                    //進行表單登入 ※之後使用User.Identity.Name的值就是vm.Account帳號的值
                    FormsAuthentication.SetAuthCookie(account, true);
                    //進行表單登入  之後User.Identity.Name的值就是Account帳號的值

                    //第二個參數如果是true則cookie留存30分鐘；false則視窗關閉自動失效
                    //並根據web.config的設定自動跳轉道登入後頁面
                    FormsAuthentication.RedirectFromLoginPage(account, false);

                    //↓這行不會執行到，亂回傳XD
                    return RedirectToAction("Index", "QQA");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(account, false);
                    return Content("Login fail");
                }
            }
            else
            {
                FormsAuthentication.SetAuthCookie(account, false);
                return Content("Login fail");
            }
        }

        public ActionResult GetValidateCode()
        {
            Validation vCode = new Validation();
            byte[] bytes;
            string code;
            vCode.GetValidateCode(out code,out bytes);
            return File(bytes, @"image/jpeg");
        }
    }
}