using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebThoiTrang.Models;

namespace WebThoiTrang.Models
{
    public class GioHang
    {
        WebBanQuanAoEntities db = new WebBanQuanAoEntities();
        public int maSP { get; set; }
        public string tenSP { get; set; }
        public string hinhanh { get; set; }
        public int dongia { get; set; }
        public int soluong { get; set; }
        public double thanhtien
        {
            get { return dongia * soluong; }
        }
        //ham tao cho gio hang
        public GioHang(int imaSP)
        {
            maSP = imaSP;//gán tham số truyền vào
            SanPham sp = db.SanPhams.Single(n => n.MaSanPham == maSP);
            tenSP = sp.TenSanPham;
            hinhanh = sp.HinhAnh;
            dongia = Int32.Parse(sp.DonGia);
            soluong = 1;

        }

    }
}