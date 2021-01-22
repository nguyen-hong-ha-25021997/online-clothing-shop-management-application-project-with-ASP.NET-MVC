using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebThoiTrang.Models;

namespace WebThoiTrang.Controllers
{
    public class NguoiDungsController : Controller
    {
        private WebBanQuanAoEntities db = new WebBanQuanAoEntities();

        // GET: NguoiDungs
        public ActionResult NguoiDung()
        {
            var nguoidungs = db.nguoidungs.Include(n => n.GroupU);
            return View(nguoidungs.ToList());
        }
        [HttpGet]
        // GET: NguoiDungs/Details/5
        public ActionResult ChiTietND(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nguoidung nguoidung = db.nguoidungs.Find(id);
            if (nguoidung == null)
            {
                return HttpNotFound();
            }
            return View(nguoidung);
        }
        [HttpGet]
        // GET: NguoiDungs/Create
        public ActionResult ThemND()
        {
            ViewBag.groupID = new SelectList(db.GroupUs, "groupID", "groupName");
            return View();
        }

        // POST: NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemND([Bind(Include = "ID,username,pass,groupID,ten")] nguoidung nguoidung)
        {
            if (ModelState.IsValid)
            {
                db.nguoidungs.Add(nguoidung);
                db.SaveChanges();
                return RedirectToAction("NguoiDung", "NguoiDungs");
            }

            ViewBag.groupID = new SelectList(db.GroupUs, "groupID", "groupName", nguoidung.groupID);
            return View(nguoidung);
        }
        [HttpGet]
        // GET: NguoiDungs/Edit/5
        public ActionResult SuaThongTinND(string id)
        {
            nguoidung nguoidung = db.nguoidungs.FirstOrDefault(x => x.ID == id);
            if (nguoidung == null)
            {
                return HttpNotFound();
            }
            ViewBag.groupID = new SelectList(db.GroupUs, "groupID", "groupName", nguoidung.groupID);
            return View(nguoidung);
        }

        // POST: NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaThongTinND([Bind(Include = "ID,username,pass,groupID,ten")] nguoidung nguoidung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoidung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("NguoiDung", "NguoiDungs");
            }
            ViewBag.groupID = new SelectList(db.GroupUs, "groupID", "groupName", nguoidung.groupID);
            return View(nguoidung);
        }
        [HttpGet]
        // GET: NguoiDungs/Delete/5
        public ActionResult XoaND(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nguoidung nguoidung = db.nguoidungs.Find(id);
            if (nguoidung == null)
            {
                return HttpNotFound();
            }
            return View(nguoidung);
        }

        // POST: NguoiDungs/Delete/5
        [HttpPost, ActionName("Xoa")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            nguoidung nguoidung = db.nguoidungs.Find(id);
            db.nguoidungs.Remove(nguoidung);
            db.SaveChanges();
            return RedirectToAction("NguoiDung", "NguoiDungs");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
