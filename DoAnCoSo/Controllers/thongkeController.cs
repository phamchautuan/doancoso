using DoAnCoSo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCoSo.Controllers
{
    public class thongkeController : Controller
    {
        CarDealertDataContext db = new CarDealertDataContext();
        // GET: thongke
        public ActionResult Index()
        {
            int ThongKeDonHang = db.DonHangs.Count();
            int TongSanPham = db.SanPhams.Count();
            int[] data = new int[] { ThongKeDonHang, TongSanPham, };
            ViewBag.Data = data;
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SanPham s)
        {
            var E_maxe = Convert.ToInt32(collection["map"]);
            var E_tenxe = collection["TenXe"];
            var E_hinh = collection["Hinh"];
            var E_hinh1 = collection["Hinh1"];
            var E_hinh2 = collection["Hinh2"];
            var E_hinh3 = collection["Hinh3"];
            var E_giaban = Convert.ToDecimal(collection["GiaBan"]);
            var E_soluongton = Convert.ToInt32(collection["SoLuongTon"]);
            var E_dongco = collection["DongCo"];
            var E_hopso = collection["HopSo"];
            var E_congsuat = collection["CongSuat"];
            var E_chieudai = Convert.ToDecimal(collection["ChieuDai"]);
            var E_chieurong = Convert.ToDecimal(collection["ChieuRong"]);
            if (string.IsNullOrEmpty(E_tenxe))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {

                s.MaXe = E_maxe;
                s.TenXe = E_tenxe.ToString();
                s.Hinh = E_hinh.ToString();
                s.Hinh1 = E_hinh1.ToString();
                s.Hinh2 = E_hinh2.ToString();
                s.Hinh3 = E_hinh3.ToString();
                s.GiaBan = E_giaban;
                s.DongCo = E_dongco;
                s.SoLuongTon = E_soluongton;
                s.DongCo = E_dongco;
                s.HopSo = E_hopso;
                s.CongSuat = E_congsuat;
                s.ChieuDai = E_chieudai;
                s.ChieuRong = E_chieurong;

                db.SanPhams.InsertOnSubmit(s);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult TongSanPham()
        {
            var sp = db.SanPhams;
            return View(sp);
        }

        public ActionResult Delete(int id)
        {
            var D_Car = db.SanPhams.First(m => m.MaXe == id);
            return View(D_Car);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_car = db.SanPhams.Where(m => m.MaXe == id).First();
            db.SanPhams.DeleteOnSubmit(D_car);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var E_xem = db.SanPhams.First(m => m.MaXe == id);
            return View(E_xem);
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            //var E_maphim = collection["maphim"];
            var E_xe = db.SanPhams.First(m => m.MaXe == id);
            var E_tenxe = collection["TenXe"];
            var E_hinh = collection["Hinh"];
            var E_hinh1 = collection["Hinh1"];
            var E_hinh2 = collection["Hinh2"];
            var E_hinh3 = collection["Hinh3"];
            var E_giaban = Convert.ToDecimal(collection["GiaBan"]);
            var E_soluongton = Convert.ToInt32(collection["SoLuongTon"]);
            var E_dongco = collection["DongCo"];
            var E_hopso = collection["HopSo"];
            var E_congsuat = collection["CongSuat"];
            var E_chieudai = Convert.ToDecimal(collection["ChieuDai"]);
            var E_chieurong = Convert.ToDecimal(collection["ChieuRong"]);
            E_xe.MaXe = id;
            if (string.IsNullOrEmpty(E_tenxe))
            {
                ViewData["Error"] = "Don't Empty";
            }
            else
            {
                E_xe.TenXe = E_tenxe;
                E_xe.Hinh = E_hinh;
                E_xe.Hinh1 = E_hinh1;
                E_xe.Hinh2 = E_hinh2;
                E_xe.Hinh3 = E_hinh3;
                E_xe.GiaBan = E_giaban;
                E_xe.SoLuongTon = E_soluongton;
                E_xe.DongCo = E_dongco;
                E_xe.HopSo = E_hopso;

                UpdateModel(E_xe);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
    }
}