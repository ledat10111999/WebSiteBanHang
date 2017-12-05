using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class GioHang
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string HinhAnh { get; set; }

        public GioHang()
        {

        }

        public GioHang(int iMaSP)
        {
            using (QuanLyBanHangEntities db = new QuanLyBanHangEntities())
            {
                this.MaSP = iMaSP;
                SanPham sp = db.SanPhams.Single(n => n.MaSP == iMaSP);
                this.TenSP = sp.TenSP;
                this.DonGia = sp.DonGia.Value;
                this.HinhAnh = sp.HinhAnh;
                this.ThanhTien = DonGia * SoLuong;

            }
        }
        public GioHang(int iMaSP,int sl)
        {
            using (QuanLyBanHangEntities db = new QuanLyBanHangEntities())
            {
                this.MaSP = iMaSP;
                SanPham sp = db.SanPhams.Single(n => n.MaSP == iMaSP);
                this.TenSP = sp.TenSP;
                this.DonGia = sp.DonGia.Value;
                this.HinhAnh = sp.HinhAnh;
                this.SoLuong = sl;
                this.ThanhTien = DonGia * SoLuong;

            }

            
        }

    }
}