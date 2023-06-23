using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_TONGHOP_SokKimThanh
{
    public class DonHang
    {
        public static int _SoThuTu;
        private string[] _MaHang;
        private int _SoLuong; //tong so luong mua hang hoa
        private double _TongTien; // tong tien mua hang hoa
        private string _TenKH;
        private string _DiaChiKH;
        private string _SDT;
        private DateTime _NgayDatHang;
        public DonHang()
        {
            _SoThuTu++;
        }
        public DonHang(string[] maHang, int soLuong, double tongTien, string tenKH, string diaChiKH, string sdt, DateTime ngayDatHang)
        {
            _MaHang = maHang;
            _SoLuong = soLuong;
            _TongTien = tongTien;
            _TenKH = tenKH;
            _DiaChiKH = diaChiKH;
            _SDT = sdt;
            _NgayDatHang = ngayDatHang;
            _SoThuTu++;
        }
        /// <summary>
        /// get ma hang ra kieu string
        /// </summary>
        public string getMaHang
        {
            get
            {
                string sum = "";
                for (int i = 0; i < _MaHang.Length; i++)
                {
                    sum += _MaHang[i] + ",";
                }

                return sum;
            }
        }

        /// <summary>
        /// set ma hang theo kieu string
        /// </summary>
        public string setMaHang
        {
            set
            {
                string[] arr = value.Split(',');
                _MaHang = arr;
            }
        }

        /// <summary>
        /// get set ma hang theo kieu array string
        /// </summary>
        public string[] MaHang { get; set; }

        public int TongSoLuongGioHang { get => _SoLuong; set => _SoLuong = value; }
        public string TenKH { get => _TenKH; set => _TenKH = value; }
        public string SDT { get => _SDT; set => _SDT = value; }
        public DateTime NgayDatHang { get => _NgayDatHang; set => _NgayDatHang = value; }
        public double TongTien { get => _TongTien; set => _TongTien = value; }
        public string DiaChiKH { get => _DiaChiKH; set => _DiaChiKH = value; }

        public string InRaFile()
        {
            return $"#{_SoThuTu,-5}" +
                $"{/*xoa ky tu cuoi cung*/getMaHang.Remove(getMaHang.Length - 1) }#" +
                $"{TongSoLuongGioHang,-10}#{TongTien.ToString("C"),-25}#" +
                $"{TenKH,-15}#{SDT,-15}#{NgayDatHang.ToString("dd/MM/yyyy"),-10}#";
        }

        public override string ToString()
        {
            return $"#{_SoThuTu,-5}" +
                $"{/*xoa ky tu cuoi cung*/getMaHang.Remove(getMaHang.Length - 1),-10}" +
                $"{TongSoLuongGioHang,-10}{TongTien.ToString("C"),-25}" +
                $"{TenKH,-15}{SDT,-15}{NgayDatHang.ToString("dd/MM/yyyy"),-10}";
        }
        /// <summary>
        /// ham Tao don hang moi
        /// </summary>
        /// <param name="dsGioHang"></param>
        public void NhapDonHang(DanhSachGioHang dsGioHang)
        {
            // khai bao
            Random d = new Random();
            string maHang = string.Empty;

            // xu ly ma hang
            for (Node<GioHangItem> p = dsGioHang.Data.First; p != null; p = p.Next)
            {
                maHang += p.Data.HangHoa.MaHang + ",";
                this.TongSoLuongGioHang += p.Data.SoLuong;
                this.TongTien += p.Data.ThanhTien();
            }
            this.setMaHang = dsGioHang.GetMaHang();
            string[] arr = { "Le Duy Anh Tu", "Sok Kim Thanh", "Hoang Van Dung" };
            TenKH = arr[d.Next(arr.Length)];
            SDT = d.Next(100000000, 900000000).ToString();
            /* lưu ý rằng không phải tất cả các tháng đều có 31 ngày và 
            * tháng 2 có thể có 28 hoặc 29 ngày tùy thuộc vào năm. 
            * 
            * Điều này có thể dẫn đến một ngoại lệ ArgumentOutOfRangeException
            * khi khởi tạo đối tượng DateTime với một ngày không hợp lệ
            * 
            * cho tháng đã chọn.*/
            // Random ngay
            try
            {
                int nam = DateTime.Now.Year;// nam hien tai
                int thang = d.Next(1, 13);// 1-12 
                int soNgayTrongThang = DateTime.DaysInMonth(nam, thang);// tính số ngày trong tháng trước khi random
                int ngay = d.Next(1, soNgayTrongThang + 1);
                this.NgayDatHang = new DateTime(nam, thang, ngay);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
