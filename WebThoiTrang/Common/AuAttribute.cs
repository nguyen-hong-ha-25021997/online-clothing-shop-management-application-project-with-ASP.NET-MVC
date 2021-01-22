using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThoiTrang.Models;

namespace WebThoiTrang.Common
{
    public class AuAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filtercontext)
        {
            int id = int.Parse( HttpContext.Current.Session["groupID"].ToString());
            if (id == 1)
            {
                string[] lstpermissionSP = { "SanPhams-ChinhSua",
                    "SanPhams-HeThong",
                    "SanPhams-HienThiChiTiet",
                    "SanPhams-HienThiHoaDon",
                    "SanPhams-HoaDon",
                    "SanPhams-KhachHang",
                    "SanPhams-LoaiSanPham",
                    "SanPhams-SuaDon",
                    "SanPhams-SuaLoaiSP",
                    "SanPhams-ThemLoaiSP",
                    "SanPhams-ThemMoi",
                    "SanPhams-XemKhachHang",
                    "SanPhams-Xoa",
                    "SanPhams-XoaHoaDon",
                    "SanPhams-XoaKhachHang",
                    "SanPhams-XoaLoaiSP"};
                string[] lstpermissionND = { "nguoidungs-ChiTietNguoiDung",
                    "nguoidungs-NguoiDung",
                    "nguoidungs-SuaThongTinNguoiDung",
                    "nguoidungs-ThemNguoiDung",
                    "nguoidungs-XoaNguoiDung"};
                string actionNamSP = "SanPhams-" + filtercontext.ActionDescriptor.ActionName;
                string actionNamND = "nguoidungs-" + filtercontext.ActionDescriptor.ActionName;
                if (!lstpermissionSP.Contains(actionNamSP)&&!lstpermissionND.Contains(actionNamND))
                {
                    filtercontext.Result = new RedirectResult("~/AdLogIn/sos");
                }
            }
            if (id == 2)
            {
                string[] lstpermissionSP = { "SanPhams-HeThong",
                    "SanPhams-HienThiChiTiet",
                    "SanPhams-HienThiHoaDon",
                    "SanPhams-HoaDon",
                    "SanPhams-KhachHang",
                    "SanPhams-LoaiSanPham",
                    "SanPhams-SuaDon",
                    "SanPhams-XemKhachHang",
                };
                string actionNam = "SanPhams-" + filtercontext.ActionDescriptor.ActionName;
                if (!lstpermissionSP.Contains(actionNam))
                {
                    filtercontext.Result = new RedirectResult("~/AdLogIn/sos");
                }
            }
        }
    }
}