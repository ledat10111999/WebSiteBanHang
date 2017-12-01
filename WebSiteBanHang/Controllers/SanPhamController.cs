using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class SanPhamController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult SanPham1()
        {
            var listSanPhamLTM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
            ViewBag.ListLT = listSanPhamLTM;

            var listSanPhamDT = db.SanPhams.Where(n => n.MaLoaiSP == 1);
            ViewBag.ListDT = listSanPhamDT;

            var listSanPhamLMT = db.SanPhams.Where(n => n.MaLoaiSP == 3);
            ViewBag.ListMT = listSanPhamLMT;
            return View();
        }

        public ActionResult SanPham2()
        {
            var listSanPhamLTM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
            ViewBag.ListSP = listSanPhamLTM;
            return View();
        }
        [ChildActionOnly]
        public ActionResult SanPhamPartial()
        {
            //var listSanPhamLTM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
            //return PartialView(listSanPhamLTM);
            return PartialView();
        }
    }
}