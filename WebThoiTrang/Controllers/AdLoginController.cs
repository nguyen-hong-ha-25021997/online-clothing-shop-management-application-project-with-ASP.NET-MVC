using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThoiTrang.Models;
namespace WebThoiTrang.Controllers
{
    public class AdLogInController : Controller
    {
        WebBanQuanAoEntities db = new WebBanQuanAoEntities();
        // GET: AdLogIn
        public ActionResult Index()
        {
            return View();
        }
        //admin đăng nhập
        [HttpGet]
        public ActionResult AdLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdLogin(FormCollection form)
        {
            string sTaiKhoan = form["user"].ToString();
            string sMatKhau = form.Get("pass").ToString();

            var u = db.nguoidungs.SingleOrDefault(n => n.username == sTaiKhoan && n.pass == sMatKhau);
            if (u != null)
            {
                Session["groupID"] = u.groupID;
                return RedirectToAction("HeThong", "SanPhams");
            }
            else
            {
                ModelState.AddModelError("", "Tên tài khoản không đúng!");
            }
            return View();
        }
        public ActionResult sos()
        {
            return View();
        }
    }
}