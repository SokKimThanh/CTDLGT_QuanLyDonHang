

using System;

namespace BT_TONGHOP_SokKimThanh
{
    public class DanhSachGioHang
    {
        // fields
        private MyLinkedList<GioHangItem> list;
        private int size;
        // constructor
        public DanhSachGioHang()
        {
            this.list = new MyLinkedList<GioHangItem>();
            size = 0;
        }

        public int Count { get => size; set => size = value; }

        // properties
        internal MyLinkedList<GioHangItem> Data { get => list; set => list = value; }

        /// <summary>
        /// Hàm thêm đơn hàng mới
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Node<GioHangItem> Them(GioHangItem item)
        {
            size++;
            return list.AddLast(item);
        }

        /// <summary>
        /// hàm xóa đơn hàng
        /// </summary>
        /// <returns></returns>
        public Node<GioHangItem> Xoa()
        {
            size--;
            return list.RemoveFirst();
        }
        public string GetMaHang()
        {
            string mahang = string.Empty;
            // cap nhat so luong
            for (Node<GioHangItem> p = list.First; p != null; p = p.Next)
            {
                mahang += p.Data.HangHoa.MaHang + ",";
            }
            return mahang.Remove(mahang.Length - 1); ;
        }
        public double TongThanhTien()
        {
            double tongThanhTien = 0.0;
            // cap nhat so luong
            for (Node<GioHangItem> p = list.First; p != null; p = p.Next)
            {
                tongThanhTien += p.Data.ThanhTien();
            }
            return tongThanhTien;
        }

        /// <summary>
        /// Hàm xuất danh sách 
        /// </summary>
        public void XuatDS()
        {
            // cap nhat so luong
            Console.ForegroundColor = ConsoleColor.White;
            for (Node<GioHangItem> p = list.First; p != null; p = p.Next)
            {
                Console.WriteLine($"{p.Data.HangHoa.TenHang,-15}{p.Data.SoLuong,-15}\tThanh tien: {p.Data.ThanhTien().ToString("C"),-15}");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            ThongBao.hr();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{"",-20}{"-TAM TINH: ",20}{"",-7}");
            Console.WriteLine(TongThanhTien().ToString("C"));
        }
    }
}
