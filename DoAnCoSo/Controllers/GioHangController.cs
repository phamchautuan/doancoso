using DoAnCoSo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCoSo.Controllers
{
    public class GioHangController : Controller
    {
        CarDealertDataContext db = new CarDealertDataContext();
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<GioHang> lstGioHang = Laygiohang();
            GioHang sanpham = lstGioHang.Find(n => n.MaXe == id);
            if (sanpham == null)
            {
                sanpham = new GioHang(id);
                lstGioHang.Add(sanpham);
                  return RedirectToAction("GioHang");
               // return Redirect(strURL);

            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
                //return RedirectToAction("index");
            }
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tsl = lstGioHang.Sum(n => n.iSoluong);
            }
            return tsl;
        }
        public ActionResult TongDonHang()
        {
            var ct = db.DonHangs;
            return View(ct);
        }

        public int TongSLSanPham()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tsl = lstGioHang.Count;
            }
            return tsl;
        }

        private double TongTien()
        {
            double tt = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tt = lstGioHang.Sum(n => n.dThanhtien);
            }
            return tt;
        }

        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongslsanpham = TongSLSanPham();
            return View(lstGiohang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongslsanpham = TongSLSanPham();
            return PartialView();
        }

        public ActionResult XoaGiohang(int id)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.MaXe == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.MaXe == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.MaXe == id);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(collection["txtSoLg"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
                //return RedirectToAction("GioHangTrong", "GioHang");
            }
            List<GioHang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                ViewData["ErrorMessage"] = "Có lỗi xảy ra";
               return RedirectToAction("GioHangTrong", "GioHang");
            }
            ViewBag.TongsoLuong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.TongsoLuongsanpham = TongSLSanPham();
            return View(lstGiohang);
        }

        public ActionResult DatHang(FormCollection collection)
        {
            DonHang dh = new DonHang();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            SanPham s = new SanPham();

            List<GioHang> gh = Laygiohang();
            //var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
            dh.MaKH = kh.MaKH;
            dh.NgayDat = DateTime.Now;
            //dh.NgayGiao = DateTime.Parse(ngaygiao);
            dh.GiaoHang = false;
            dh.ThanhToan = false;
            //dh.MaDon = db.DonHangs.OrderByDescending(x=>x.MaDon).First().MaDon + 1;
            db.DonHangs.InsertOnSubmit(dh);
            db.SubmitChanges();
            foreach (var item in gh)
            {
                CTDonHang ctdh = new CTDonHang();
                ctdh.MaDon = dh.MaDon;
                ctdh.MaXe = item.MaXe;
                ctdh.SoLuong = item.iSoluong;
                ctdh.GiaBan = (decimal)item.GiaBan;
                s = db.SanPhams.Single(n => n.MaXe == item.MaXe);

                s.SoLuongTon -= ctdh.SoLuong;
                db.SubmitChanges();
                db.CTDonHangs.InsertOnSubmit(ctdh);
            }
            db.SubmitChanges();
            Session["GioHang"] = null;
           
            return RedirectToAction("XacnhanDonhang", "GioHang");
        }

        public ActionResult XacnhanDonhang()
        {
            return View();
        }

        public ActionResult GioHangEmpty()
        {
            return View();
        }


    }
}