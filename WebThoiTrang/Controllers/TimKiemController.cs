using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThoiTrang.Models;
using PagedList;
using PagedList.Mvc;
namespace WebQuanAo.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        WebBanQuanAoEntities db = new WebBanQuanAoEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult KetQuaTimKiem(string tukhoa, int? page)
        {

            ViewBag.TuKhoa = tukhoa;
            List<SanPham> listkq = db.SanPhams.Where(n => n.TenSanPham.Contains(tukhoa) || n.DonGia.Contains(tukhoa) || n.Kieu.Contains(tukhoa)).ToList();


            int pageSize = 4;
            int pageNumber = (page ?? 1);
            if (listkq.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm";

            }

            return View(listkq.OrderBy(n => n.DonGia).ToPagedList(pageNumber, pageSize));

        }
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f, int? page)
        {
            string tukhoa = f["txtTimKiem"].ToString();

            List<SanPham> listkq = db.SanPhams.Where(n => n.TenSanPham.Contains(tukhoa) || n.DonGia.Contains(tukhoa) || n.Kieu.Contains(tukhoa)).ToList();
            ViewBag.TuKhoa = tukhoa;

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            if (listkq.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm";

            }

            return View(listkq.OrderBy(n => n.DonGia).ToPagedList(pageNumber, pageSize));

        }
    }
}