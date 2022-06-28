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
    public class WadminController : Controller
    {
        private WYEntities db = new WYEntities();

        // GET: Wadmin
        public ActionResult Index()
        {
            return View(db.w_admin.ToList());
        }

        // GET: Wadmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            w_admin w_admin = db.w_admin.Find(id);
            if (w_admin == null)
            {
                return HttpNotFound();
            }
            return View(w_admin);
        }

        // GET: Wadmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wadmin/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,password,nickname,power,createtime")] w_admin w_admin)
        {
                db.w_admin.Add(w_admin);
            int res = db.SaveChanges();
            if (res > 0)
            {
                return Content("<script>alert('アカウントを登録しました。');window.location.href='/Wadmin/Index';</script>");
            }
            else
            {
                ViewBag.notice = "アカウント登録が失敗しました。";
            }


            return View(w_admin);
        }

        // GET: Wadmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            w_admin w_admin = db.w_admin.Find(id);
            if (w_admin == null)
            {
                return HttpNotFound();
            }
            return View(w_admin);
        }

        // POST: Wadmin/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,password,nickname,power,createtime")] w_admin w_admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(w_admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(w_admin);
        }


        // POST: Wadmin/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            w_admin w_admin = db.w_admin.Find(id);
            db.w_admin.Remove(w_admin);
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
