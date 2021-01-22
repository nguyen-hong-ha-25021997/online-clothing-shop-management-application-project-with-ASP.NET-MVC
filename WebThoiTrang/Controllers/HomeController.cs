using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThoiTrang.Models;

namespace WebThoiTrang.Controllers
{
    public class HomeController : Controller
    {
        WebBanQuanAoEntities db = new WebBanQuanAoEntities();

        public static string loaiSP;
        //Trang chur
        public ActionResult TrangChu()
        {
            return View();
        }
        //San pham ban chay
        public PartialViewResult SanPhamBanChay(int? page)
        {
            //Tạo số sản phẩm của trang
            int pageSize = 4;
            //tao so trang
            int pageNumber = (page ?? 1);//nesu khong du 8 san pham thi mac dinh la 1 trang
            return PartialView(db.SanPhams.Where(n => n.GhiChu == "Bán Chạy").ToList().OrderBy(n => n.DonGia).ToPagedList(pageNumber, pageSize));
        }
        //san pham sales
        public PartialViewResult SanPhamSalesShow()
        {
            return PartialView(db.SanPhams.Where(n => n.GhiChu == "Sales").Take(4).ToList());
        }
        public PartialViewResult SanPhamSales(int? page)
        {
            //Tạo số sản phẩm của trang
            int pageSize = 3;
            //tao so trang
            int pageNumber = (page ?? 1);//nesu khong du 8 san pham thi mac dinh la 1 trang
            return PartialView(db.SanPhams.Where(n => n.GhiChu == "Sales").ToList().OrderBy(n => n.DonGia).ToPagedList(pageNumber, pageSize));


        }
        //san pham moi
        public PartialViewResult SanPhamMoi(int? page)
        {
            //Tạo số sản phẩm của trang
            int pageSize = 7;
            //tao so trang
            int pageNumber = (page ?? 1);//nesu khong du 8 san pham thi mac dinh la 1 trang
            return PartialView(db.SanPhams.Where(n => n.GhiChu == "Mới").ToList().OrderBy(n => n.DonGia).ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult DanhMuc()
        {
            return PartialView(db.LoaiSanPhams.ToList());
        }
        //chi tiet
        public PartialViewResult XemChiTietSanPham(int MaSanPham = 0)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSanPham == MaSanPham);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return PartialView(sp);
        }

        //cac san pham
        public ViewResult CacSanPham(string type, int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            loaiSP = type;
            return View(db.SanPhams.Where(n => n.Kieu == type).ToList().ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult SanPhamMoiShow()
        {
            return PartialView(db.SanPhams.Where(n => n.GhiChu == "Mới").Take(4).ToList());
        }
        //tung loai san pham
        public ViewResult TungLoaiSanPham(string loai, int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            loaiSP = loai;


            return View(db.SanPhams.Where(n => n.MaLoaiSP == loai).ToList().ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult dau()
        {
            return PartialView();
        }
        public PartialViewResult cuoi()
        {
            return PartialView();
        }
    }
}