using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeeNow.Models;
using System.Data.Entity;
using System.Net;
using System.Web.Security;
using System.Net.Mail;


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


        #region Registered 初始註冊頁面
        public ActionResult Registered()
        {
            ViewBag.profile = new SelectList(db.profile, "profile_id", "profile_path");
            ViewBag.role_id = new SelectList(db.role, "role_id", "role_desc");
            return View();
        }
        #endregion

        #region Registered 註冊
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registered(string account,string role_id,string password,string nick_name,string e_mail,short profile_id)
        {
            //檢查帳號或email是否存在，存在則不可重複申請
            var user = db.users.Where(m => m.account == account || m.e_mail== e_mail).FirstOrDefault();
            if (user != null)
            {
                Response.Write("<script>alert('帳號或信箱重複，請修改!！');</script>");
                ViewBag.profile = new SelectList(db.profile, "profile_id", "profile_path");
                ViewBag.role_id = new SelectList(db.role, "role_id", "role_desc");
                return View();
            }
            else
            {

                users newuser = new users();
                newuser.account = account;
                newuser.role_id = role_id;
                newuser.password = password;
                newuser.nick_name = nick_name;
                newuser.e_mail = e_mail;
                newuser.profile_id = profile_id;

                db.users.Add(newuser);
                db.SaveChanges();

                ViewBag.mail = e_mail;
                ViewBag.account = account;

                SendAuthMail(e_mail, account);
                return View("SendAuthMail");
                
            }
            
        }
        #endregion

        #region Login
        public ActionResult Login()
        {
            //ViewBag.profile_id = new SelectList(db.profile, "profile_id", "profile_name");
            ViewBag.role_id = new SelectList(db.role, "role_id", "role_desc");
            return View();
        }
        #endregion

        #region Login post
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string account,string role_id,string password,string txtcode)
        {
            if (Session["ValiCode"] == null)
            {
                ViewBag.msg="閒置時間過長，請重新輸入!!";
                return View();
            }
            if (Session["ValiCode"].ToString() != txtcode)
            {
                ViewBag.msg="驗證碼錯誤，請重新輸入!!";
                return View();
            }
            else
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
                        //FormsAuthentication.SetAuthCookie(account, true);
                        //進行表單登入  之後User.Identity.Name的值就是Account帳號的值

                        //第二個參數如果是true則cookie留存30分鐘；false則視窗關閉自動失效
                        //並根據web.config的設定自動跳轉道登入後頁面
                        //FormsAuthentication.RedirectFromLoginPage(account, false);

                        //↓這行不會執行到，亂回傳XD
                        return RedirectToAction("Index", "QQA");
                    }
                    else
                    {
                        //FormsAuthentication.SetAuthCookie(account, false);
                        ViewBag.msg = "帳號或密碼錯誤!!";
                        return View();
                        //return Content("Login fail");
                    }
                }
                else
                {
                    //FormsAuthentication.SetAuthCookie(account, false);
                    ViewBag.msg = "帳號或密碼錯誤!!";
                    return View();
                    //return Content("Login fail");
                }
            }
        }
        #endregion

        public ActionResult GetValidateCode()
        {
            Validation vCode = new Validation();
            byte[] bytes;
            string code;
            vCode.GetValidateCode(out code,out bytes);
            Session["ValiCode"] = code;
            return File(bytes, @"image/jpeg");
        }

        #region SendAuthMail 寄認證信 無頁面
        protected void SendAuthMail(string toMail, string account)
        {
            //string toMail; string account;
            //toMail = ViewBag.mail;
            //account = ViewBag.account;

            SmtpClient myMail = new SmtpClient("msa.hinet.net");
            MailAddress from = new MailAddress("metartron+seenow@gmail.com", "SeeNow服務中心");
            MailAddress to = new MailAddress(toMail);
            MailMessage Msg = new MailMessage(from, to);
            Msg.Subject = "SeeNow會員註冊認證信";
            Msg.Body = "請點擊下列超連結完成會員註冊認證<br /> <br /><a href='http://10.10.3.200/"+Url.Action("AuthOK","Home")+"?account=" + account + "' >請點我</a>";

            Msg.IsBodyHtml = true;

            myMail.Send(Msg);

        }
        #endregion

        #region AuthOK 認證信驗證頁面
        public ActionResult AuthOK(string account)
        {
            var user = db.users.Where(m => m.account == account).FirstOrDefault();
            if (user != null)
            {
                if (user.validation_flag != true)
                {
                    user.validation_flag = true;
                    db.SaveChanges();

                    Response.Write("<header class='masthead'><div class='container'><h2 class='text-center'>恭喜您完成會員認證！</h2></div></header>");
                    return View();
                }
                else
                {
                    Response.Write("<header class='masthead'><div class='container'><h2 class='text-center'>會員已認證過，請不要重複認證!！!</h2></div></header>");
                    return View();
                }
                //return RedirectToAction("Login");
            }
            else
            {
                Response.Write("<header class='masthead'><div class='container'><h2 class='text-center'>查無此會員,請註冊!!</h2></div></header>");
                return View();
                //return RedirectToAction("Registered");
            }

            
        }
        #endregion
    }
}