using System;
/**
 * Sok Kim Thanh
 * Date 18/05/2023
 * Database thu nghiem 
 */
namespace BT_TONGHOP_SokKimThanh
{
    /// <summary>
    /// Thu nghiem chay chuong trinh
    /// </summary>
    class Program
    {
        const int alignLeft = -20;// canh le trai
        const int center = -40;

        static void Main(string[] args)
        {
            // chuong trinh dang nhap
            //ThuNghiemBaiTap.ThuNghiemMainMenu();
            ThuNghiemDanhSachHangHoaLinkedList();
            ThongBao.p_exit();
            Console.ReadKey();
        }
        /// <summary>
        /// Thu nghiem danh sach linked list hang hoa
        /// </summary>
        public static void ThuNghiemDanhSachHangHoaLinkedList()
        {
            DanhSachHangHoaLinkedList dsHangHoa = new DanhSachHangHoaLinkedList();

            for (int i = 0; i < 10; i++)
            {
                HangHoa hh = new HangHoa();
                hh.NhapTuDong();
                dsHangHoa.ThemCuoi(hh);
            }
            dsHangHoa.XuatDS();
            int loopCount = dsHangHoa.Data.Count;
            Node<HangHoa> lastNode;


            for (int i = 0; i < loopCount; i++)
            {
                lastNode = dsHangHoa.XoaCuoi();
                if (lastNode != null)
                {
                    Console.WriteLine(i + " " + dsHangHoa.Data.Count + "\n" + lastNode.Data);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                HangHoa hh = new HangHoa();
                hh.NhapTuDong();
                dsHangHoa.ThemDau(hh);
            }
            Console.ForegroundColor = ConsoleColor.White;
            dsHangHoa.XuatDS();
            ThongBao.p_success_writeline($"{dsHangHoa.Data.Count}");
            string maHangHoa;
            do
            {
                ThongBao.p_success_write("Nhap ma hang hoa hoac -1 de ket thuc: ");
                maHangHoa = Console.ReadLine();
                if (maHangHoa == "-1")
                {
                    break;
                }
                Node<HangHoa> noe = dsHangHoa.SequentialSearch(maHangHoa);
                if (noe == null)
                {
                    ThongBao.p_alert_writeline("Khong tim thay");
                }
                else
                {
                    ThongBao.p_success_writeline("Tim thay");
                    HangHoa hangHoa = noe.Data;
                    dsHangHoa.Xoa(hangHoa);
                    ThongBao.p_success_writeline($"{dsHangHoa.Data.Count}");
                }
            } while (dsHangHoa.Data.Count > 0 || maHangHoa != "-1");

            Console.ForegroundColor = ConsoleColor.White;
            dsHangHoa.InterChangeSort();
            dsHangHoa.XuatDS();
            ThongBao.p_success_writeline($"{dsHangHoa.Data.Count}");
        }
        /// <summary>
        /// ham thu nghiem danh sach don hang
        /// </summary>
        public static void ThuNghiemDanhSachDonHang()
        {
            Node<int> node1 = new Node<int>(3);
            Node<int> node2 = new Node<int>(4);
            node1.Next = node2;
            node2.Next = node1;
            Console.WriteLine(node1.toString() + "\n" + node2.toString());


            MyLinkedList<int> list1 = new MyLinkedList<int>();
            list1.AddFirst(3);
            list1.AddFirst(4);
            list1.AddFirst(5);
            list1.Print(); // 5 4 3

            MyLinkedList<string> list2 = new MyLinkedList<string>();
            list2.AddFirst("xin chao");
            list2.AddFirst("ban nho");
            list2.Print(); // ban nho xin chao

            MyLinkedList<DonHang> list3 = new MyLinkedList<DonHang>();
            string[] maHang = { "cs000336", "cs000336" };
            int soLuong = 3;
            double tongTien = 333333.3;
            string tenKH = "NGuyen van a";
            string diachiKH = "Ha noi dong da";
            string sdt = "88889999";
            int nam = 2030;
            int thang = 12;
            int ngay = 1;
            DateTime ngayDatHang = new DateTime(nam, thang, ngay);

            DonHang donHang001 = new DonHang(maHang, soLuong, tongTien, tenKH, diachiKH, sdt, ngayDatHang);
            list3.AddFirst(donHang001);
            list3.Print();


            // thu nghiem danh sach don hang linked list
            DanhSachDonHang danhsachDH = new DanhSachDonHang();
            danhsachDH.ThemDH(donHang001);
            danhsachDH.XuatDS();

        }
    }
}
