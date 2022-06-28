using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WYsystem.Models;

namespace WYsystem.Controllers
{
    public class AnnouncementController : Controller
    {
        private WYEntities db = new WYEntities();

        // GET: Announcement
        public ActionResult Index()
        {
            return View(db.w_announcement.OrderByDescending(p=>p.createtime).ToList());
        }

        // GET: Announcement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            w_announcement w_announcement = db.w_announcement.Find(id);
            if (w_announcement == null)
            {
                return HttpNotFound();
            }
            return View(w_announcement);
        }

        // GET: Announcement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Announcement/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,number,title,createtime,contents,uid,nickname")] w_announcement w_announcement)
        {
            if (ModelState.IsValid)
            {
                db.w_announcement.Add(w_announcement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(w_announcement);
        }

        // GET: Announcement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            w_announcement w_announcement = db.w_announcement.Find(id);
            if (w_announcement == null)
            {
                return HttpNotFound();
            }
            return View(w_announcement);
        }

        // POST: Announcement/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,number,title,createtime,contents,uid,nickname")] w_announcement w_announcement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(w_announcement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(w_announcement);
        }

       

        // POST: Announcement/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            w_announcement w_announcement = db.w_announcement.Find(id);
            db.w_announcement.Remove(w_announcement);
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
