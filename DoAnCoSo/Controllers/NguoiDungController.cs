using DoAnCoSo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DoAnCoSo.Controllers
{
    public class NguoiDungController : Controller
    {
        CarDealertDataContext db = new CarDealertDataContext();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang khachhang)
        {

            var hoten = collection["HoTen"];
            var matkhau = collection["matkhau"];
            var Email = collection["Email"];
            var dienthoai = collection["dienthoai"];
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận!";

            }
            else
            {
                if (!matkhau.Equals(matkhau))
                {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khảu xác nhận phải giống nhau;";
                }
                else
                {

                    khachhang.TenKH = hoten;
                    khachhang.MatKhau = matkhau;
                    khachhang.SDT = dienthoai;
                    khachhang.Email = Email;
                    db.KhachHangs.InsertOnSubmit(khachhang);
                    db.SubmitChanges();
                    return RedirectToAction("index", "Home");
                }
            }
            return this.DangKy();
        }
        //[HttpGet]
        //public ActionResult DangNhap()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public ActionResult DangNhap(FormCollection collection)
        //{

        //    var Email = collection["email"];
        //    var matkhau = collection["matkhau"];
        //    KhachHang khachhang = db.KhachHangs.SingleOrDefault(n => n.Email == Email && n.MatKhau == matkhau);
        //    if (khachhang != null)
        //    {
        //        ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
        //        Session["TaiKhoan"] = khachhang;
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
        //        return this.DangNhap();
        //    }

        //}
        public ActionResult DangNhap()
        {
            if (Session["KhachHang"] == null)
            {
                return View();
            }
            else
                return Redirect("/");
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var Email = collection["email"];
            var matkhau = collection["matkhau"];
            var data = db.KhachHangs.SingleOrDefault(n => n.Email == Email && n.MatKhau == matkhau);
            if (data == null)
            {
                ViewBag.Error = "Thông tin đăng nhập không đúng";
                return View();
            }
            if (data.GroupID == true)
            {
                ViewBag.Done = "Chào mừng Admin";
                Session["KhachHang"] = data.TenDangNhap + "," + data.TenKH + "," + data.MaKH + ",true";
                return RedirectToAction("Index","thongke");
            }
            else
            {
                ViewBag.Done = "Đăng nhập thành công";
                string data_account = data.TenDangNhap + "," + data.TenKH + "," + data.MaKH + ",";
                //Session["TaiKhoan"] = data;
                Session.Add("KhachHang", data_account);
                return Redirect("/");
            }
        }

        public ActionResult DangXuat()
        {
            Session.Abandon();
            return RedirectToAction("DangNhap", "NguoiDung");
        }
    }
}

    
