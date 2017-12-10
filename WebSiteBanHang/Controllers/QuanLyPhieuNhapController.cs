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

            ViewBag.MaNCC = db.NhaCungCaps;
            ViewBag.ListSanPham = db.SanPhams;
            // Kiểm tra dữ liệu đầu vào bằng javascript hay bên metadata đều được
            // Phải ktra để khớp với kiểu dữ liệu của database

            //Gán đã xóa = false
            model.DaXoa = false;
            db.PhieuNhaps.Add(model);
            db.SaveChanges();
            // SaveChanges lần đầu để  sinh ra mã phiếu nhập gán cho lstChiTietPhieuNhap
            SanPham sp;
            foreach(var item in lstModel)
            {
                // Cập nhật số lượng tồn
                // vì sản phẩm trong lstModel chắc chắn có nên k tạo new SanPham
                sp = db.SanPhams.Single(n => n.MaSP == item.MaSP);
                sp.SoLuongTon +=  item.SoLuongNhap;
                // Gán mã phiếu nhập cho từng chi tiết phiếu nhập
                item.MaPN = model.MaPN;
            }
            // lệnh gán theo list
            db.ChiTietPhieuNhaps.AddRange(lstModel);
            db.SaveChanges();
            return View();
        }
    }
}