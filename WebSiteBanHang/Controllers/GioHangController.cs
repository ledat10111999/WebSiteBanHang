using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // Lây danh sách giỏ hàng
        public List<itemGioHang> LayGioHang()
        {
            List<itemGioHang> lstGioHang = Session["GioHang"] as List<itemGioHang>;
            if(lstGioHang == null)
            {
                //Nếu session bằng  null thì khởi tạo gio hàng
                lstGioHang = new List<itemGioHang>();
                // Gán lại giỏ hàng cho session
                Session["GioHang"] = lstGioHang;
            }
            // nếu giỏ hàng khác null ( đã có sản phẩm trong giỏ ) thì trả về  list
            return lstGioHang;
        }
        //  Thêm sản phẩm bằng cách thông thường ( Load lại trang bằng URL)
        public ActionResult ThemGioHang(int MaSP,string strURL)
        {
            // Kiểm tra trong csdl 
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //Trả về trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // nếu != null thì Lấy giỏ hàng
            List<itemGioHang> lstGioHang = LayGioHang();
            // Xét trường hợp sản phẩm được chọn đã có trong giỏ hàng -> tăng số lượng và cập nhật thành tiền
            itemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if(spCheck != null)
            {
                // Kiểm tra số lượng tồn kho
                if(spCheck.SoLuong > sp.SoLuongTon)
                {
                    // trả về thông báo hết hàng
                    return View("ThongBao");
                }
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                // trả về trang URL hiện tại
                return Redirect(strURL);
            }
            // nếu sp không có trong giỏ hàng -> tạo sp theo MaSP mới rồi add vào giỏ hàng hiện tại
            itemGioHang itemGH = new itemGioHang(MaSP);
            // Kiểm tra số lượng tồn kho
            if (itemGH.SoLuong > sp.SoLuongTon)
            {
                // trả về thông báo hết hàng
                return View("ThongBao");
            }
            lstGioHang.Add(itemGH);
            return Redirect(strURL);

        }
        // Tính tổng số lượng
        public double TinhTongSoLuong()
        {
            // Lấy giỏ hàng từ Session 
            List<itemGioHang> lstGioHang = Session["GioHang"] as List<itemGioHang>;
            if(lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.SoLuong);
        }

        // Tính tổng tiền
        public decimal TinhTongTien()
        {
            List<itemGioHang> lstGioHang = Session["GioHang"] as List<itemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ThanhTien);
        }

        public ActionResult GioHangPartial()
        {
            //Kiểm tra nếu tổng số lượng = 0 thì trả về View 0
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            // Gán trả về ViewBag
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();

            return PartialView();
        }


        // GET: GioHang
        public ActionResult XemGioHang()
        {
            List<itemGioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }

        
    }
}