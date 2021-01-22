using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThoiTrang.Models;
namespace WebThoiTrang.Controllers
{
    public class KhachHangController : Controller
    {
        WebBanQuanAoEntities db = new WebBanQuanAoEntities();
        // GET: KhachHang
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKi()
        {
            return View();
        }
        [HttpPost] // truyen gia tri values cho ham DangKi

        public ActionResult DangKi(KhachHang kh)
        {
            db.KhachHangs.Add(kh); //chen du lieu vao bang khach hang
            db.SaveChanges();//luu vao data


            return View();
        }

        [HttpGet]
        public PartialViewResult DangNhap()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult DangNhap(HoaDon hd, FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f.Get("txtMatKhau").ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Đăng nhập thành công!";
                Session["TaiKhoan"] = kh;//luu lai tai khoan
                HoaDon hdkh = new HoaDon();
                KhachHang h = (KhachHang)Session["TaiKhoan"];
                //  List<GioHang> gh = laygiohang();
                hdkh.MaKhachHang = h.MaKhachHang;
                hdkh.NgayMua = DateTime.Now;
                hdkh.DiaChi = hd.DiaChi;
                hdkh.SDT = hd.SDT;

                Session["HoaDon"] = hdkh;

                return RedirectToAction("Home", "GioHang");
            }
            ViewBag.ThongBao = "Tên tài khoản không đúng!";
            return PartialView();
        }
    }
}