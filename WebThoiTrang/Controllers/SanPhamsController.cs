using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThoiTrang.Models;
using WebThoiTrang.Common;
using PagedList;
using PagedList.Mvc;
namespace WebThoiTrang.Controllers
{
    [AuAttribute]
    public class SanPhamsController : Controller
    {
        WebBanQuanAoEntities db = new WebBanQuanAoEntities();
        // GET: SanPhams
        public ActionResult HeThong(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(db.SanPhams.ToList().ToList().ToPagedList(pageNumber, pageSize));
        }
        //Sản phẩm
        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.ToList().OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "MaLoaiSP");

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(SanPham sp, HttpPostedFileBase fileupload)
        {
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP");
            if (fileupload == null)
            {
                ViewBag.ThongBao = "Chọn hình ảnh";
                return View();
            }
            if (ModelState.IsValid)
            {
                var filename = Path.GetFileName(fileupload.FileName);//lưu tên file
                var path = Path.Combine(Server.MapPath("~/Images"), filename);//lưu đường dẫn 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileupload.SaveAs(path);
                }
                sp.HinhAnh = fileupload.FileName;
                db.SanPhams.Add(sp);

                db.SaveChanges();

            }

            return RedirectToAction("HeThong", "SanPhams");

        }
        [HttpGet]
        public ActionResult ChinhSua(int masp)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSanPham == masp);
            return View(sp);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(SanPham sp)
        {
            SanPham sp1 = db.SanPhams.SingleOrDefault(n => n.MaSanPham == sp.MaSanPham);
            sp1.TenSanPham = sp.TenSanPham;
            sp1.MaHienThi = sp.MaHienThi;
            sp1.MaLoaiSP = sp.MaLoaiSP;
            sp1.TinhTrang = sp.TinhTrang;
            sp1.DonGia = sp.DonGia;
            sp1.GhiChu = sp.GhiChu;
            sp1.Size = sp.Size;

            db.SaveChanges();
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP");

            return RedirectToAction("HeThong", "SanPhams");

        }

        [HttpGet]
        public ActionResult Xoa(int masp)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSanPham == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int masp)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSanPham == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SanPhams.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("HeThong", "SanPhams");
        }
        public ActionResult HienThiChiTiet(int masp)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSanPham == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else return View(sp);
        }
        //Khách hàng
        public ActionResult KhachHang(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(db.KhachHangs.ToList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult XemKhachHang(int makh)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKhachHang == makh);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else return View(kh);
        }
        [HttpGet]
        public ActionResult XoaKhachHang(int makh)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKhachHang == makh);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        [HttpPost, ActionName("XoaKhachHang")]
        public ActionResult XacNhanXoaKhachHang(int makh)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKhachHang == makh);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.KhachHangs.Remove(kh);
            db.SaveChanges();
            return RedirectToAction("KhachHang", "SanPhams");
        }
        //Loại sản phẩm
        public ActionResult LoaiSanPham(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(db.LoaiSanPhams.ToList().ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult ThemLoaiSP()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemLoaiSP(LoaiSanPham l)
        {
            db.LoaiSanPhams.Add(l);
            db.SaveChanges();
            return RedirectToAction("LoaiSanPham", "SanPhams");
        }
        [HttpGet]
        public ActionResult SuaLoaiSP(string maloaisp)
        {
            LoaiSanPham lsp = db.LoaiSanPhams.SingleOrDefault(n => n.MaLoaiSP == maloaisp);
            if (lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(lsp);
        }
        [HttpPost]
        public ActionResult SuaLoaiSP(LoaiSanPham l)
        {
            LoaiSanPham lsp = db.LoaiSanPhams.SingleOrDefault(n => n.MaLoaiSP == l.MaLoaiSP);
            lsp.MaLoaiSP = l.MaLoaiSP;
            lsp.TenLoaiSP = l.TenLoaiSP;
            lsp.SoLuong = l.SoLuong;
            db.SaveChanges();
            return RedirectToAction("LoaiSanPham", "SanPhams");
        }
        [HttpGet]
        public ActionResult XoaLoaiSP(string malsp)
        {
            LoaiSanPham lsp = db.LoaiSanPhams.SingleOrDefault(n => n.MaLoaiSP == malsp);
            if (lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(lsp);
        }

        [HttpPost, ActionName("XoaLoaiSP")]
        public ActionResult XacNhanXoaLSP(string malsp)
        {
            LoaiSanPham lsp = db.LoaiSanPhams.SingleOrDefault(n => n.MaLoaiSP == malsp);
            if (lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.LoaiSanPhams.Remove(lsp);
            db.SaveChanges();
            return RedirectToAction("LoaiSanPham", "SanPhams");
        }
        //Hóa đơn
        public ActionResult HoaDon(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(db.HoaDons.ToList().ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult ThemMoiHoaDon()
        {
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.MaKhachHang), "MaKhachHang", "MaKhachHang");
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ThemMoiHoaDon(HoaDon h)
        {

            ViewBag.MaKhachHang = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.MaKhachHang), "MaKhachHang", "MaKhachHang");
            db.HoaDons.Add(h);
            db.SaveChanges();
            return RedirectToAction("HoaDon", "SanPhams");
        }
        public ActionResult SuaDon(int mahd)
        {
            HoaDon hd = db.HoaDons.SingleOrDefault(n => n.MaHoaDon == mahd);

            return View(hd);
        }
        [HttpPost]

        public ActionResult SuaDon(HoaDon h)
        {
            HoaDon hd = db.HoaDons.SingleOrDefault(n => n.MaHoaDon == h.MaHoaDon);
            hd.MaKhachHang = h.MaKhachHang;
            hd.SDT = h.SDT;
            hd.NgayMua = h.NgayMua;
            hd.NgayGiao = h.NgayGiao;
            hd.DiaChi = h.DiaChi;
            hd.DaThanhToan = h.DaThanhToan;

            db.SaveChanges();
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.TenKhachHang), "MaKhachHang", "TenKhachHang");

            return RedirectToAction("HoaDon", "SanPhams");

        }
        [HttpGet]
        public ActionResult XoaHoaDon(int ma)
        {
            HoaDon hd = db.HoaDons.SingleOrDefault(n => n.MaHoaDon == ma);
            if (hd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hd);
        }

        [HttpPost, ActionName("XoaHoaDon")]
        public ActionResult XacNhanXoaHoaDon(int ma)
        {
            HoaDon hd = db.HoaDons.SingleOrDefault(n => n.MaHoaDon == ma);
            if (hd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.HoaDons.Remove(hd);
            db.SaveChanges();
            return RedirectToAction("HoaDon", "SanPhams");
        }
        public ActionResult HienThiHoDon(int mahd)
        {
            ChiTietHoaDon hd = db.ChiTietHoaDons.SingleOrDefault(n => n.MaHoaDon == mahd);
            if (hd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else return View(hd);
        }

    }
}

