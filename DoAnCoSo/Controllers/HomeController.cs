using PagedList.Mvc;
using DoAnCoSo.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
namespace DoAnCoSo.Controllers
{
    public class HomeController : Controller
    {
        dbCarDealerDataContext db = new dbCarDealerDataContext();
        
        public ActionResult Index(int? page, int? size, string searchString)
        {
            //var all_car = (from Car in db.SanPhams select Car).OrderBy(m => m.MaXe);
            //


            ViewBag.Keyword = searchString;
            //if (!string.IsNullOrEmpty(searchString)) all_car = (IOrderedQueryable<Car>)all_car.Where(a => a.TenXe.Contains(searchString));


            List<SelectListItem> items = new List<SelectListItem>();
           items.Add(new SelectListItem { Text = "3", Value = "3" });
            items.Add(new SelectListItem { Text = "6", Value = "6" });
            items.Add(new SelectListItem { Text = "12", Value = "12" });
            items.Add(new SelectListItem { Text = "24", Value = "24" });
            items.Add(new SelectListItem { Text = "48", Value = "48" });
            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
           ViewBag.size = items;
            ViewBag.currentSize = size;
            if (page == null) page = 1;
            var all_car = (from Car in db.SanPhams select Car).OrderBy(m => m.MaXe);
            if (!string.IsNullOrEmpty(searchString)) all_car = (IOrderedQueryable<SanPham>)all_car.Where(a => a.TenXe.Contains(searchString));
           // var all_car = from car in db.SanPhams select car;
            int PageSize = (size ?? 6);
            int pageNum = page ?? 1;
            return View(all_car.ToPagedList(pageNum, PageSize));

         
        }
        //public ActionResult SapXepTheoTen(int? page, string searchString)
        //{
        //    ViewBag.Keyword = searchString;

        //    if (page == null) page = 1;
        //    var all_car = (from Car in db.SanPhams select Car).OrderBy(m => m.MaXe);
        //    if (!string.IsNullOrEmpty(searchString)) all_car = (IOrderedQueryable<SanPham>)all_car.Where(a => a.TenXe.Contains(searchString));
        //    int pageSize = 3;
        //    int pageNum = page ?? 1;
        //    return View(all_car.ToPagedList(pageNum, pageSize));
        //}
        public ActionResult Detail(int id)
        {
            var D_xe = db.SanPhams.Where(m => m.MaXe == id).First();
            return View(D_xe);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}