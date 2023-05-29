using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHotel.Models
{
    public class HotelModel
    {
        string _TenPhong;
        public string TenPhong { get => _TenPhong; set { _TenPhong = value; } }

        string _TinhTrang;
        public string TinhTrang { get => _TinhTrang; set { _TinhTrang = value; } }
        string _TenNguoiThue;
        public string TenNguoiThue { get => _TenNguoiThue; set { _TenNguoiThue = value; } }

        string _ThoiGianThue;
        public string ThoiGianThue { get => _ThoiGianThue; set { _ThoiGianThue = value; } }

        string _GiaTien;
        public string GiaTien { get => _GiaTien; set { _GiaTien = value; } }
        
        public HotelModel(string _TenPhong,string _TinhTrang,string _TenNguoiThue,string _ThoiGianThue,string _GiaTien)
        {
            this._TenPhong = _TenPhong;
            this._TinhTrang = _TinhTrang;
            this._TenNguoiThue = _TenNguoiThue;
            this._ThoiGianThue = _ThoiGianThue;
            this._GiaTien = _GiaTien;
        }
    }
}
