using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SeeNow.Models;

namespace SeeNow.Controllers
{
    public class usersController : Controller
    {
        private SeeNowEntities db = new SeeNowEntities();

        // GET: users
        public ActionResult Index()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            //var users = db.users.Include(u => u.profile).Include(u => u.role).Where(u=>u.account== Session["user_id"].ToString()).FirstOrDefault();
            //return View(users);
            ViewBag.profile = new SelectList(db.profile, "profile_id", "profile_path");
            ViewBag.role_id = new SelectList(db.role, "role_id", "role_desc");

            users users = db.users.Find(Session["user_id"].ToString());
            return View(users);
        }

        // GET: users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: users/Create
        public ActionResult Create()
        {
            ViewBag.profile_id = new SelectList(db.profile, "profile_id", "profile_name");
            ViewBag.role_id = new SelectList(db.role, "role_id", "role_desc");
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "account,role_id,password,nick_name,e_mail,score,energy,profile_id,bag_number,lock_flag,validation_flag,resetable")] users users)
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

        // GET: users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.profile_id = new SelectList(db.profile, "profile_id", "profile_name", users.profile_id);
            ViewBag.role_id = new SelectList(db.role, "role_id", "role_desc", users.role_id);
            return View(users);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "account,role_id,password,nick_name,e_mail,score,energy,profile_id,bag_number,lock_flag,validation_flag,resetable")] users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.profile_id = new SelectList(db.profile, "profile_id", "profile_name", users.profile_id);
            ViewBag.role_id = new SelectList(db.role, "role_id", "role_desc", users.role_id);
            return View(users);
        }

        // GET: users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            users users = db.users.Find(id);
            db.users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
