using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeeNow.Models;
using System.Data.Entity;
using System.Net;

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
            ViewBag.profile_id = new SelectList(db.profile, "profile_id", "profile_name");
            ViewBag.role_id = new SelectList(db.role, "role_id", "role_desc");
            return View();
        }
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registered([Bind(Include = "account,role_id,password,nick_name,e_mail,score,energy,profile_id,bag_number,lock_flag,validation_flag,resetable")] users users)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.profile_id = new SelectList(db.profile, "profile_id", "profile_name", users.profile_id);
            ViewBag.role_id = new SelectList(db.role, "role_id", "role_desc", users.role_id);
            return View(users);
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}