using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WYsystem.Models;

namespace WYsystem.Controllers
{
    public class AdminController : Controller
    {
        private WYEntities db = new WYEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["nickname"]==null)
            {
                return Redirect("/Login/Index");
            }
            return View();
        }
        //団地管理
        public ActionResult RoomIndex()
        {
            //団地情報取得
            w_room_info info = db.w_room_info.FirstOrDefault();

            return View(info);
        }
        //団地登録
        public ActionResult AddRoom()
        {
            //団地情報取得
            w_room_info info = db.w_room_info.FirstOrDefault();

            return View(info);
        }

        //団地登録
        [HttpPost]
        public ActionResult AddRoom(w_room_info room)
        {
            ViewBag.notice = "";
            //団地情報登録
            db.w_room_info.Add(room);
            //変更実行
            int res = db.SaveChanges();
            if(res > 0)
            {
                return Content("<script>alert('登録しました。');window.location.href='/Admin/RoomIndex';</script>");
            }
            else
            {
                ViewBag.notice = "登録失敗しました。";
            }

            return View();
        }


        //団地編集
        public ActionResult UpdateRoom()
        {
            //団地情報取得
            w_room_info info = db.w_room_info.FirstOrDefault();

            if(info == null)
            {
                return Content("<script>alert('団地情報を見つかりません。。');window.location.href='/Admin/RoomIndex';</script>");
            }

            return View(info);
        }
        //団地編集
        [HttpPost]
        public ActionResult UpdateRoom(w_room_info info)
        {
            db.Entry(info).State = EntityState.Modified;
            if(db.SaveChanges() > 0)
                {
                    return Content("<script>alert('変更されました。');window.location.href='/Admin/RoomIndex';</script>");
                }
            else
                {
                    ViewBag.notice = "変更失敗しました。";
                }
                return View();
        }

    }
}