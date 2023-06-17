using DoAnCoSo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCoSo.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DangNhap() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string user, string pass) 
        {
            //check db
            CarDealertDataContext db = new CarDealertDataContext();
            int demTaiKhoan = db.KhachHangs.Count(m => m.TenDangNhap.ToLower()== user.ToLower() && m.MatKhau == pass);
            return View();

            //check code
            if(demTaiKhoan == 1)
            {
                Session["user"] = user;
                ViewBag.user = user;
                TempData["error"] = new KhachHang();

                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Tài khoản hoặc mật khẩu không đúng";
                return View();
            }
        }
    }
}