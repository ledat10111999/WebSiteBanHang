using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
namespace WebSiteBanHang.Controllers
{
    public class QuanLyPhieuNhapController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: QuanLyPhieuNhap
        [HttpGet]
        public ActionResult NhapHang()
        {
            ViewBag.MaNCC = db.NhaCungCaps;
            ViewBag.ListSanPham = db.SanPhams;
            return View();
        }

        [HttpPost]
        public ActionResult NhapHang(PhieuNhap model,IEnumerable<ChiTietPhieuNhap> lstModel)
        {
            return View();
        }
    }
}