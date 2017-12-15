using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
namespace WebSiteBanHang.Controllers
{
    public class TinhTrangGiaoHangController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: TinhTrangGiaoHang
        public ActionResult XacNhan()
        {
            var lst = db.DonDatHangs.Where(n => n.MaTrangThai == 1).OrderBy(n => n.NgayDat);
            return View(lst);
        }

        public ActionResult GiaoHang()
        {
            var lst = db.DonDatHangs.Where(n => n.MaTrangThai == 2).OrderBy(n => n.NgayDat);
            return View(lst);
        }

        public ActionResult ThanhCong()
        {
            var lst = db.DonDatHangs.Where(n => n.MaTrangThai==3).OrderBy(n => n.NgayDat);
            return View(lst);
        }

        public ActionResult Huy()
        {
            
            var lst = db.DonDatHangs.Where(n => n.MaTrangThai==4).OrderBy(n => n.NgayDat);
            return View(lst);
        }

        [HttpGet]
        public ActionResult DuyetDonHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang model = db.DonDatHangs.SingleOrDefault(n => n.MaDDH == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTrangThai = new SelectList(db.TrangThaiGiaoHangs.OrderBy(n => n.MaTrangThai), "MaTrangThai", "TenTrangThai", model.MaTrangThai);
            // Lấy ds chi tiết đơn hàng để hiển thị cho người dùng thấy
            var lstChiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDDH == id);
            ViewBag.ListChiTietDH = lstChiTietDH;
            return View(model);
        }

        [HttpPost]
        public ActionResult DuyetDonHang(DonDatHang ddh)
        {
            
            // Lấy dữ liệu của đơn hàng đó
            DonDatHang ddhUpdate = db.DonDatHangs.Single(n => n.MaDDH == ddh.MaDDH);

            ViewBag.MaTrangThai = new SelectList(db.TrangThaiGiaoHangs.OrderBy(n => n.MaTrangThai), "MaTrangThai", "TenTrangThai", ddhUpdate.MaTrangThai);

            ddhUpdate.DaHuy = ddh.DaHuy;
            ddhUpdate.TinhTrangGiaoHang = ddh.TinhTrangGiaoHang;
            ddhUpdate.MaTrangThai =ddh.MaTrangThai;
            db.SaveChanges();

            // Lấy ds chi tiết đơn hàng để hiển thị cho người dùng thấy
            var lstChiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.ListChiTietDH = lstChiTietDH;

            

            //GuiEmail("Xác nhận đơn hàng", "ducnghia1205@gmail.com", "kiembtcmp@gmail.com", "zewang.help", "Đơn hàng của bạn đã được đặt thành công");
            return View(ddhUpdate);
        }

        public void GuiEmail(string Title, string ToEmail, string FromEmail, string PassWord, string Content)
        {
            // goi email
            MailMessage mail = new MailMessage();
            mail.To.Add(ToEmail); // Địa chỉ nhận
            mail.From = new MailAddress(ToEmail); // Địa chửi gửi
            mail.Subject = Title;  // tiêu đề gửi
            mail.Body = Content;                 // Nội dung
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; // host gửi của Gmail
            smtp.Port = 587;               //port của Gmail
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            (FromEmail, PassWord);//Tài khoản password người gửi
            smtp.EnableSsl = true;   //kích hoạt giao tiếp an toàn SSL
            smtp.Send(mail);   //Gửi mail đi
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}