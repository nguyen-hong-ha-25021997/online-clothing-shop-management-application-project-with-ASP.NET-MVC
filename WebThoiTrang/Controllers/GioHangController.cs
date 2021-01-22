using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using WebThoiTrang.Models;
namespace WebQuanAo.Controllers
{

    public class GioHangController : Controller
    {
        WebBanQuanAoEntities db = new WebBanQuanAoEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
        //lay gio hang
        #region Giỏ hàng
        public List<GioHang> laygiohang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;//ép kiểu sesion giỏ hàng khi giỏ hàng tồn tại ,nếu không tồn tại trả về null
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //them gio hang
        public ActionResult themgiohang(int masp, string url)
        {
            //lấy ra sesion giỏ hàng
            List<GioHang> lstgiohang = laygiohang();
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSanPham == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //kiểm tra sản phẩm đã tồn tại trong giỏ hàng chưa
            GioHang sanpham = lstgiohang.Find(n => n.maSP == masp);
            if (sanpham == null)
            {
                sanpham = new GioHang(masp);
                lstgiohang.Add(sanpham);
                return Redirect(url);
            }
            else
            {
                sanpham.soluong++;
                return Redirect(url);
            }
        }
        //cập nhật giỏ hàng
        public ActionResult capnhatgiohang(int maSP, FormCollection f)
        {
            //Kiểm tra mã sản phẩm
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSanPham == maSP);
            //nếu get sai mã sản phẩm thì trả về trang lỗi
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng ra
            List<GioHang> lstgiohang = laygiohang();
            //Kiểm tra sản phẩm có tồn tại trong sesion giỏ hàng k
            GioHang sanpham = lstgiohang.SingleOrDefault(n => n.maSP == maSP);
            if (sanpham != null)
            {
                sanpham.soluong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");//không sử dụng View vì k có định nghĩa kiểu Model
        }
        public ActionResult xoagiohang(int maSP)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSanPham == maSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng ra session
            List<GioHang> lstgiohang = laygiohang();
            GioHang sanpham = lstgiohang.SingleOrDefault(n => n.maSP == maSP);
            //
            if (sanpham != null)
            {
                lstgiohang.RemoveAll(n => n.maSP == maSP);
            }
            if (lstgiohang.Count == 0)
            {
                return RedirectToAction("TrangChu", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("TrangChu", "Home");
            }
            List<GioHang> lstgiohang = laygiohang();
            return View(lstgiohang);
        }
        public ActionResult Home()
        {
            if (Session["GioHang"] != null)
            {
                Session["GioHang"] = null;

            }
            return RedirectToAction("TrangChu", "Home");
        }
        private int tongsoluong()
        {
            int soluong = 0;
            List<GioHang> lstgiohang = Session["GioHang"] as List<GioHang>;
            if (lstgiohang != null)
            {
                soluong = lstgiohang.Sum(n => n.soluong);
            }
            return soluong;
        }
        public PartialViewResult TongSL()
        {
            ViewBag.TongSL = tongsoluong();
            return PartialView();
        }
        private double tongtien()
        {
            double tien = 0;
            List<GioHang> lstgiohang = Session["GioHang"] as List<GioHang>;
            if (lstgiohang != null)
            {
                tien = lstgiohang.Sum(n => n.thanhtien);
            }
            return tien;
        }
        public PartialViewResult TongTien()
        {
            ViewBag.TongTien = tongtien();
            return PartialView();
        }
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("TrangChu", "Home");
            }
            List<GioHang> lstgiohang = laygiohang();
            return View(lstgiohang);
        }
        #endregion
        #region Đặt hàng

        [HttpPost]
        public ActionResult DatHang()
        {


            //Kiem tra dang nhap
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            //Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("TrangChu", "Home");

            }
            //Thêm đơn đặt hàng

            //Kiểm tra tài khoản 
            HoaDon dk = new HoaDon();

            HoaDon hd = (HoaDon)Session["HoaDon"];
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            List<GioHang> gh = laygiohang();
            dk.MaKhachHang = kh.MaKhachHang;
            dk.NgayMua = DateTime.Now;
            dk.DiaChi = hd.DiaChi;
            dk.SDT = hd.SDT;

            db.HoaDons.Add(dk);

            db.SaveChanges();//luu vao csdl


            foreach (var item in gh)
            {
                ChiTietHoaDon ct = new ChiTietHoaDon();
                ct.MaHoaDon = dk.MaHoaDon;
                ct.MaSanPham = item.maSP;
                ct.SoLuong = item.soluong.ToString();
                ct.DonGia = item.dongia.ToString();
                db.ChiTietHoaDons.Add(ct);

            }




            //Luu lai chi tiet don hang


            db.SaveChanges();


            return RedirectToAction("TrangChu", "Home");
        }
        #endregion


    }
}