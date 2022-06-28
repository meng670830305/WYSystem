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
    public class SystemController : Controller
    {
        private WYEntities db = new WYEntities();

        // GET: System
        public ActionResult Index(string type="")
        {
            IEnumerable<w_system_params> list = db.w_system_params;
            if (!string.IsNullOrEmpty(type))
            {
                list = list.Where(p => p.type == type);
            }

            return View(list.ToList());
        }

        // GET: System/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            w_system_params w_system_params = db.w_system_params.Find(id);
            if (w_system_params == null)
            {
                return HttpNotFound();
            }
            return View(w_system_params);
        }

        // GET: System/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: System/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,code,name,type")] w_system_params w_system_params)
        {
                db.w_system_params.Add(w_system_params);
                int res = db.SaveChanges();
                if (res > 0)
                {
                    return Content("<script>alert('Systemパラメータ保存しました。');window.location.href='/System/Index';</script>");
                }
                else
                {
                    ViewBag.notice = "Systemパラメータ保存失敗しました。";
                }

            return View(w_system_params);
        }

        // GET: System/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            w_system_params w_system_params = db.w_system_params.Find(id);
            if (w_system_params == null)
            {
                return HttpNotFound();
            }
            return View(w_system_params);
        }

        // POST: System/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,code,name,type")] w_system_params w_system_params)
        {
            if (ModelState.IsValid)
            {
                db.Entry(w_system_params).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(w_system_params);
        }


        // POST: System/Delete/5
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            w_system_params w_system_params = db.w_system_params.Find(id);
            db.w_system_params.Remove(w_system_params);
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
