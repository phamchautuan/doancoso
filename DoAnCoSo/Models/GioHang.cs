using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DoAnCoSo.Models
{
    public class GioHang
    {
        dbCarDealerDataContext data = new dbCarDealerDataContext();

        public int MaXe { get; set; }

        [Display(Name = "Tên xe")]
        public string TenXe { get; set; }

        [Display(Name = "")]
        public string Hinh { get; set; }

        [Display(Name = "Giá bán")]

        public double GiaBan { get; set; }

        [Display(Name = "Số lượng")]
        public int iSoluong { get; set; }

        [Display(Name = "Thành tiền")]
        public double dThanhtien
        {
            get { return iSoluong * GiaBan; }
        }
        public GioHang(int id)
        {
            MaXe = id;
            SanPham car = data.SanPhams.Single(n => n.MaXe == MaXe);
            TenXe = car.TenXe;
            Hinh = car.Hinh;
            GiaBan = double.Parse(car.GiaBan.ToString());
            iSoluong = 1;
        }
    }
}