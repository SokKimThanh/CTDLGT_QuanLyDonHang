using System;

using System.IO;
using System.Xml.Linq;

/**
 * Sok Kim Thanh
 * 30/05/2023 4:12 CH
 * Viet lop danh sach don hang theo kieu linked list
 * modified: 01/06/2023 8:12 SA
 * modified: 01/06/2023 5:58 CH
 */
namespace BT_TONGHOP_SokKimThanh
{
    /// <summary>
    /// Lớp Danh sách đơn hàng mô tả theo cấu trúc danh sách liên kết đơn
    /// </summary>
    public class DanhSachDonHang
    {
        // fields
        private MyLinkedList<DonHang> list;
        private int size;
        private string _filePath = "DonHang.txt";

        // constructor
        public DanhSachDonHang()
        {
            this.list = new MyLinkedList<DonHang>();
            size = 0;
        }

        // properties
        internal MyLinkedList<DonHang> Data { get => list; set => list = value; }
        internal string FilePath { get => _filePath; set => _filePath = value; }
        public int Count { get => size; }

        /// <summary>
        /// Hàm thêm đơn hàng mới
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Node<DonHang> ThemDH(DonHang item)
        {
            size++;
            return list.AddLast(item);
        }

        /// <summary>
        /// hàm xóa đơn hàng ở đầu danh sách
        /// </summary>
        /// <returns></returns>
        public Node<DonHang> XoaDH()
        {
            Node<DonHang> result = new Node<DonHang>(new DonHang());
            try
            {
                result = list.RemoveFirst();

            }
            catch (Exception ex)
            {
                ThongBao.p_alert_writeline(ex.Message);
            }
            size--;
            return result;
        }

        /// <summary>
        /// Hàm xuất danh sách 
        /// </summary>
        public void XuatDS()
        {
            Console.ForegroundColor = ConsoleColor.White;
            list.Print();
        }


        /// <summary>
        ///    Phương thức nhận đầu vào là một chuỗi line
        /// và trả về một đối tượng kiểu DonHang.
        /// 
        ///    Phương thức này tách chuỗi đầu vào bằng ký tự # và gán các
        /// phần tử mảng kết quả cho các thuộc tính của một đối tượng 
        /// DonHang mới.
        /// 
        ///    Phương thức cũng phân tích cú pháp ngày đặt hàng từ chuỗi 
        /// đầu vào và khởi tạo một đối tượng DateTime mới với năm,
        /// tháng và ngày đã được phân tích cú pháp. 
        /// 
        /// Cuối cùng, phương thức trả về đối tượng DonHang đã được khởi tạo.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static DonHang parseDonHang(string line)
        {
            string[] arr = line.Split('#');
            int nam = int.Parse(arr[6].Split('/')[2]);
            int thang = int.Parse(arr[6].Split('/')[1]);
            int ngay = int.Parse(arr[6].Split('/')[0]);
            DonHang item = new DonHang
            {
                setMaHang = arr[0],
                TongSoLuongGioHang = int.Parse(arr[1]),
                TongTien = double.Parse(arr[2]),
                TenKH = arr[3],
                DiaChiKH = arr[4],
                SDT = arr[5],

                NgayDatHang = new DateTime(nam, thang, ngay)
            };
            return item;
        }

        /// <summary>
        /// Hàm đọc file từ file path 
        /// </summary>
        public void DocFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        DonHang data = parseDonHang(line);
                        ThemDH(data);
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        /// <summary>
        /// Hàm ghi file vào file path
        /// </summary>
        public void GhiFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    for (Node<DonHang> p = Data.First; p != null; p = p.Next)
                    {
                        writer.WriteLine(p.Data.InRaFile());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}