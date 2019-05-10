using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using SeeNow.Models;
using SeeNow.ViewModels;

namespace SeeNow.Controllers
{
    public class setting_menuController : Controller
    {
        private SeeNowEntities db = new SeeNowEntities();

       
        public ActionResult menuPartial()
        {
          
            var result = from p in db.setting_menu
                         join c in db.setting_menu on p.id equals c.parent_id into pc
                         from c in pc.DefaultIfEmpty()
                         select new { Id=p.id ,
                             TopMenu = p.menu_name_tw,
                             SecondMenu = c.menu_name_tw,
                             View = c.view,
                             Page = c.page,
                             OrderNum = c.order_num };
            //setting_menuViewModel vm = new setting_menuViewModel();
            

            ViewBag.settingmenu = result;
            return View();
        }

        // GET: setting_menu
        public ActionResult Index()
        {
            return View(db.setting_menu.ToList());
        }

        // GET: setting_menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            setting_menu setting_menu = db.setting_menu.Find(id);
            if (setting_menu == null)
            {
                return HttpNotFound();
            }
            return View(setting_menu);
        }

        // GET: setting_menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: setting_menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,menu_name_tw,view,page,parent_id,order_num")] setting_menu setting_menu)
        {
            if (ModelState.IsValid)
            {
                db.setting_menu.Add(setting_menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(setting_menu);
        }

        // GET: setting_menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            setting_menu setting_menu = db.setting_menu.Find(id);
            if (setting_menu == null)
            {
                return HttpNotFound();
            }
            return View(setting_menu);
        }

        // POST: setting_menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,menu_name_tw,view,page,parent_id,order_num")] setting_menu setting_menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(setting_menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(setting_menu);
        }

        // GET: setting_menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            setting_menu setting_menu = db.setting_menu.Find(id);
            if (setting_menu == null)
            {
                return HttpNotFound();
            }
            return View(setting_menu);
        }

        // POST: setting_menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            setting_menu setting_menu = db.setting_menu.Find(id);
            db.setting_menu.Remove(setting_menu);
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
