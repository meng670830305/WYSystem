using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WYsystem.Models;

namespace WYsystem.Controllers
{
    public class LoginController : Controller
    {
        //DB　接続対象
        private WYEntities db = new WYEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //ログイン機能
        [HttpPost]
        public ActionResult Index(string username,string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                ViewBag.notice = "アカウントを入力してください。";
                return View();
            }
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.notice = "パスワードを入力してください。";
                return View();
            }

            w_admin admin = db.w_admin.FirstOrDefault(p=>p.username ==username);

            if (admin == null)
            {
                ViewBag.notice = "アカウント或はパスワードが正しくではありません。";
                return View();
            }
            else if (admin.password != password)
            {
                ViewBag.notice = "アカウント或はパスワードが正しくではありません。";
                return View();
            }
            else {
                //パスワード記憶cookie or session
                Session["username"] = admin.username;
                Session["nickname"] = admin.nickname;

                return Redirect("/Admin/index");
            }
        }

        public ActionResult Logout()
        {
            Session["username"] = null;
            Session["nickname"] = null;
            return Redirect("/Login/Index");
        }
    }
}