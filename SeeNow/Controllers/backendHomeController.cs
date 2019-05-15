using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SeeNow.Models;

namespace SeeNow.Controllers
{
    /// <summary>
    /// 後台首頁、登入頁...
    /// </summary>
    [Authorize]
    public class backendHomeController : Controller
    {
        SeeNowEntities db = new SeeNowEntities();

        /// <summary>
        /// 呈現後台使用者登入頁
        /// </summary>
        /// <param name="ReturnUrl">使用者原本Request的Url</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login()//string ReturnUrl
        {
            //ReturnUrl字串是使用者在未登入情況下要求的的Url
            //manager vm = new manager() {  }; //ReturnUrl = ReturnUrl
            ////return PartialView(vm);
            //return View(vm);
            return View();
        }

        ///// <summary>
        ///// 後台使用者進行登入
        ///// </summary>
        ///// <param name="vm"></param>
        ///// <param name="u">使用者原本Request的Url</param>
        ///// <returns></returns>
        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult Login(manager vm)
        //{

        //    //沒通過Model驗證(必填欄位沒填，DB無此帳密)
        //    if (!ModelState.IsValid)
        //    {
        //        return View(vm);
        //    }

        //    //都成功...
        //    //進行表單登入 ※之後使用User.Identity.Name的值就是vm.Account帳號的值
        //    FormsAuthentication.SetAuthCookie(vm.manager_id, false);
        //    //保哥文章使用 FormsAuthentication.RedirectFromLoginPage(帳號, false); 來登入也是可以
        //    //但要留意↑會做Redirect，如果後續有使用到Request.UrlReferrer.AbsoluteUri的話，值會改變


        //    //導向預設Url(Web.config裡的defaultUrl值)或 
        //    //使用者原先Request的Url(登入頁網址的QueryString：ReturnUrl，此QueryString在表單驗證機制下對於未登入的使用者會自動產生)
        //    return Redirect(FormsAuthentication.GetRedirectUrl(vm.manager_id, false));

        //}

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string Account, string PD, string ReturnUrl)
        {
            //檢查必填欄位
            if (string.IsNullOrEmpty(Account) || string.IsNullOrEmpty(PD))
            {
                ModelState.AddModelError("manager_id", "請輸入必填欄位");
                ModelState.AddModelError("password", "請輸入必填欄位");
                return View();
            }
            //把輸入的密碼加密
            //string base64PD = Convert.ToBase64String(Encoding.UTF8.GetBytes(PD));
            
            //todo 和DB裡的資料做比對
            var manager = db.manager.Where(m => m.manager_id == Account).FirstOrDefault();
            if (manager != null)
            {
                string pwd = manager.password;

                if (pwd == PD)
                {
                    //登入成功 
                    //進行表單登入 ※之後使用User.Identity.Name的值就是vm.Account帳號的值
                    FormsAuthentication.SetAuthCookie(Account, true);
                    //進行表單登入  之後User.Identity.Name的值就是Account帳號的值

                    //第二個參數如果是true則cookie留存30分鐘；false則視窗關閉自動失效
                    //並根據web.config的設定自動跳轉道登入後頁面
                    FormsAuthentication.RedirectFromLoginPage(Account, false);

                    //↓這行不會執行到，亂回傳XD
                    return Content("Login success");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(Account, false);
                    return Content("Login fail");
                }
            }
            else
            {
                FormsAuthentication.SetAuthCookie(Account, false);
                return Content("Login fail");
            }




            
        }

        /// <summary>
        /// 後台使用者登出動作
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [OutputCache(Duration = 1)]
        public ActionResult Logout()
        {
            //清除Session中的資料
            Session.Abandon();
            //登出表單驗證
            FormsAuthentication.SignOut();
            //導至登入頁
            return RedirectToAction("Login", "backendHome");
        }

        /// <summary>
        /// 登入後預設進入的畫面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //登入帳號
            ViewData["Login_Account"] = User.Identity.Name;
            //是否登入(boolean值)
            ViewData["isLogin"] = User.Identity.IsAuthenticated;

            return View();
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

        public ActionResult LayuiIndex() {
            return View();
        }

    }
}