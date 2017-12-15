using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class ThanhViensController : Controller
    {
        private QuanLyBanHangEntities db = new QuanLyBanHangEntities();

        // GET: ThanhViens
        public ActionResult Index()
        {
            var thanhViens = db.ThanhViens.Where(t => t.MaLoaiTV == 2);
            return View(thanhViens.ToList());
        }

        // GET: ThanhViens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhVien thanhVien = db.ThanhViens.Find(id);
            if (thanhVien == null)
            {
                return HttpNotFound();
            }
            return View(thanhVien);
        }

        // GET: ThanhViens/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiTV = new SelectList(db.LoaiThanhViens, "MaLoaiTV", "TenLoai");
            ViewBag.TinhTrang = new SelectList(db.TinhTrangThanhViens, "MaTinhTrang", "TenTinhTrang");
            return View();
        }

        // POST: ThanhViens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaThanhVien,TaiKhoan,MatKhau,HoTen,DiaChi,Email,SoDienThoai,CauHoi,CauTraLoi,MaLoaiTV,SoLuongTin,SoLuongTinDaDang,TinhTrang")] ThanhVien thanhVien)
        {
            if (ModelState.IsValid)
            {
                db.ThanhViens.Add(thanhVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiTV = new SelectList(db.LoaiThanhViens, "MaLoaiTV", "TenLoai", thanhVien.MaLoaiTV);
            ViewBag.TinhTrang = new SelectList(db.TinhTrangThanhViens, "MaTinhTrang", "TenTinhTrang", thanhVien.TinhTrang);
            return View(thanhVien);
        }

        // GET: ThanhViens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhVien thanhVien = db.ThanhViens.Find(id);
            if (thanhVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiTV = new SelectList(db.LoaiThanhViens, "MaLoaiTV", "TenLoai", thanhVien.MaLoaiTV);
            ViewBag.TinhTrang = new SelectList(db.TinhTrangThanhViens, "MaTinhTrang", "TenTinhTrang", thanhVien.TinhTrang);
            return View(thanhVien);
        }

        // POST: ThanhViens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaThanhVien,TaiKhoan,MatKhau,HoTen,DiaChi,Email,SoDienThoai,CauHoi,CauTraLoi,MaLoaiTV,SoLuongTin,SoLuongTinDaDang,TinhTrang")] ThanhVien thanhVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thanhVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiTV = new SelectList(db.LoaiThanhViens, "MaLoaiTV", "TenLoai", thanhVien.MaLoaiTV);
            ViewBag.TinhTrang = new SelectList(db.TinhTrangThanhViens, "MaTinhTrang", "TenTinhTrang", thanhVien.TinhTrang);
            return View(thanhVien);
        }

        // GET: ThanhViens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhVien thanhVien = db.ThanhViens.Find(id);
            if (thanhVien == null)
            {
                return HttpNotFound();
            }
            return View(thanhVien);
        }

        // POST: ThanhViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThanhVien thanhVien = db.ThanhViens.Find(id);
            db.ThanhViens.Remove(thanhVien);
            db.SaveChanges();
            return RedirectToAction("Index");
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
