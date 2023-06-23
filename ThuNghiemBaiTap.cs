using System;
using System.Threading;
/**
 * Sok Kim thanh
 * Created date: 30/05/2023 1:00 ch
 * modified: 31/05/2023 6:46 Ch
 * modified: 01/06/2023 8:13 SA
 * modified: 01/06/2023 6:00 CH
 */
namespace BT_TONGHOP_SokKimThanh
{
    public static class ThuNghiemBaiTap
    {
        // khai bao 
        static int countDangNhap = 0;// dem so lan dang nhap 
        // lua chon tieu chi tim kiem
        static int luaChonMenu;// global - reset after while
        // tieu de
        static private string[] arrColumnHangHoa = { "maHang", "tenHang", "noiSanXuat", "mauSac", "giaBan", "ngayNhapKho", "soLuong" };

        // danh sach hang hoa
        static DanhSachHangHoa dsHangHoa = new DanhSachHangHoa(10);

        // danh sach don hang
        static DanhSachDonHang dsDonHang = new DanhSachDonHang();

        // danh sách tài khoản
        static DanhSachTaiKhoan dsTaiKhoan = new DanhSachTaiKhoan(10);

        static DanhSachMenu dsMenuChinh = new DanhSachMenu();// menu chinh
        static DanhSachMenu dsMenuDatHang = new DanhSachMenu();//  menu Đặt hàng (wizard step)


        // ket qua tim kiem
        static HangHoa resultSetHangHoa;
        static GioHangItem resultSetGioHangItem;
        static DonHang resultSetDonHang;
        /// <summary>
        /// Ham thu nghiem tim kiem theo tieu chi
        /// </summary>
        public static void ThuNghiemTimKiemTheoTieuChi()
        {
            // khai bao
            string maHang;
            string tenHang;
            DanhSachMenu dsMenuDonHangTieuChi = new DanhSachMenu();//  menu Đơn hàng ( danh sách hóa đơn)

            string[] arrMenuTimTheoTieuChi = new string[] {
                            "Tim theo ma",
                            "Tim theo ten",
                            "Tim theo gia tien",
                            "Tim theo noi san xuat",
                            "Tim theo mau sac",
                            "Tim theo ngay nhap kho",
                            "Tim theo so luong",
                            "-1.Quay ve",
                        };
            // them menu
            for (int i = 0; i < arrMenuTimTheoTieuChi.Length; i++)
            {
                dsMenuDonHangTieuChi.AddMenu(new Menu(arrMenuTimTheoTieuChi[i]));
            }
            // in ket qua
            do
            {
                /**********************************************
                *               CLEAR ALL SCREEN              *
                ***********************************************/
                Console.Clear();
                // tao menu chuong trinh 
                Console.ForegroundColor = ConsoleColor.Yellow;
                ThongBao.h1_center_1("tim kiem thong tin hang hoa theo tieu chi".ToUpper());
                Console.ForegroundColor = ConsoleColor.Green;

                /// menu chinh
                dsMenuDonHangTieuChi.Show();
                // in tieu de
                Console.ForegroundColor = ConsoleColor.Yellow;
                // in tieu de
                for (int i = 0; i < arrColumnHangHoa.Length; i++)
                {
                    Console.Write($"{arrColumnHangHoa[i],-15}");
                }
                Console.WriteLine();

                // in danh sach hang hoa
                Console.ForegroundColor = ConsoleColor.White;
                dsHangHoa.XuatDS();
                // in so luong
                ThongBao.WriteLine("So luong: " + dsHangHoa.Count);
                // nhap lenh
                Console.ForegroundColor = ConsoleColor.Yellow;
                ThongBao.p_alert_write(ThongBao.pressKeyToEnterCommandOrExitVI);
                Console.ForegroundColor = ConsoleColor.White; int.TryParse(Console.ReadLine(), out luaChonMenu);
                try
                {
                    ThongBao.h1_center_1(dsMenuDonHangTieuChi.ShowMenuBySTT(luaChonMenu - 1).toString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                switch (luaChonMenu)
                {
                    case 1:// tim theo ma

                        // in ket qua
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        ThongBao.p_success_write($"{"Ma hang hoa: "}");
                        Console.ForegroundColor = ConsoleColor.White;
                        maHang = Ultilities.NhapChuoi();
                        resultSetHangHoa = dsHangHoa.SearchByMaHang(maHang);

                        if (resultSetHangHoa != null)
                        {
                            ThongBao.p_success_writeline($"Tim thay {maHang}!");
                            ThongBao.WriteLine(resultSetHangHoa.ToString());
                        }
                        else
                        {
                            ThongBao.p_alert_writeline("Khong tim thay!");
                        }
                        Console.ReadKey();
                        break;
                    case 2:// tim theo ten

                        // in ket qua
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        ThongBao.p_success_write($"{"Ten hang hoa: "}");
                        Console.ForegroundColor = ConsoleColor.White;
                        tenHang = Ultilities.NhapChuoi();
                        resultSetHangHoa = dsHangHoa.SearchByTenHang(tenHang);

                        if (resultSetHangHoa != null)
                        {
                            ThongBao.p_success_writeline($"Tim thay {tenHang}!");

                            ThongBao.WriteLine(resultSetHangHoa.ToString());
                            Console.ReadKey();
                        }
                        else
                        {
                            ThongBao.p_alert_writeline("Khong tim thay!");
                        }
                        Console.ReadKey();
                        break;
                    default: break;
                }
            } while (luaChonMenu != -1);
        }



        /// <summary>
        /// ham thu nghiem đặt hàng auto và đặt hàng manual
        /// </summary>
        public static void ThuNghiemDatHang()
        {
            // tao menu chuong trinh 
            // khai bao
            string[] arrDatHang = new string[] {
                            "Kiem tra san pham ton tai",
                            "Kiem tra so luong ton kho",
                            "Them san pham bo vao gio hang",
                            "Thanh toan hoa don",
                            "-1. Quay ve",
                        };
            // them menu
            for (int i = 0; i < arrDatHang.Length; i++)
            {
                dsMenuDatHang.AddMenu(new Menu(arrDatHang[i]));
            }
            // in ket qua
            do
            // quay lai buoc 0: NHAP LUA CHON DAT DON HANG
            {
                /**********************************************
                *               CLEAR ALL SCREEN              *
                ***********************************************/
                Console.Clear();
                // tao menu chuong trinh 
                ThongBao.h1_center_1("Bat dau chuong trinh: Thu nghiem dat hang");
                /***********************************************
                * QUAY LAI BUOC 0: NHAP LUA CHON DAT DON HANG  *
                ************************************************/

                ThongBao.p_success_write(ThongBao.pressKeyToContinue);
                Console.ReadKey();
                do
                // quay lai buoc 6: XU LY DON HANG MOI
                {
                    /**********************************************
                    *               CLEAR ALL SCREEN              *
                    ***********************************************/
                    Console.Clear();

                    // xử lý đơn hàng mới
                    ThongBao.h1_center_1("buoc 6: Xu ly don hang moi");

                    // làm mới giỏ hàng sau khi chốt đơn
                    ThongBao.p_alert_writeline("Tao gio hang moi!");
                    DanhSachGioHang dsGioHang = new DanhSachGioHang();


                    ThongBao.p_success_write(ThongBao.pressKeyToContinue);
                    Console.ReadKey();
                    /***********************************************
                    *      QUAY LAI BUOC 6: XU LY DON HANG MOI     *
                    ************************************************/


                    do
                    //quay lai buoc 3 gom: buoc 1 & 2
                    //buoc 1: NHAP MA HANG VA
                    //buoc 2: NHAP SO LUONG
                    {
                        /**********************************************
                        *               CLEAR ALL SCREEN              *
                        ***********************************************/
                        Console.Clear();
                        // tao menu chuong trinh 
                        ThongBao.h1_center_1("Bat dau quy trinh dat hang");
                        /***********************************************
                        *  QUAY LAI BUOC 3: NHAP MA HANG VA SO LUONG   *
                        ************************************************/
                        /// menu chinh
                        dsMenuDatHang.Show();
                        // in tieu de
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        // in tieu de
                        for (int i = 0; i < arrColumnHangHoa.Length; i++)
                        {
                            Console.Write($"{arrColumnHangHoa[i],-15}");
                        }
                        Console.WriteLine();
                        // in danh sach hang hoa
                        Console.ForegroundColor = ConsoleColor.White;
                        dsHangHoa.XuatDS();

                        //Tiep tuc chuong trinh sang buoc 1
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(ThongBao.pressKeyToBeContinue);
                        /************************************************
                         *         BUOC 1: NHAP MA HANG HOA             *
                         ******************************************************************************************************************************
                        /// "Đoạn code này kiểm tra mã hàng hóa. 
                        /// 
                        /// Nó yêu cầu người dùng nhập mã hàng hóa và kiểm tra xem mã hàng đó có tồn tại trong danh sách hàng hóa hay không. 
                        /// 
                        /// Nếu không tìm thấy mã hàng đó trong danh sách thì đoạn code sẽ thông báo “Không tìm thấy” và yêu cầu người dùng nhập lại. 
                        /// 
                        /// Nếu tìm thấy mã hàng đó trong danh sách thì đoạn code sẽ hiển thị thông tin hàng hóa và thoát khỏi vòng lặp." 
                        /// 
                        /// ***************************************************************************************************************************/
                        // Kiểm tra mã hàng
                        ThongBao.h1_center_1("Buoc 1: Vui long nhap ma hang!");
                        // Khai báo
                        string maHang;

                        do
                        // Bước 1: Nhập mã hàng hóa
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Nhap ma hàng: ");
                            maHang = Ultilities.NhapChuoi();
                            resultSetHangHoa = dsHangHoa.SearchByMaHang(maHang);
                            if (resultSetHangHoa == null)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Khong tim thay {0}!", maHang);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Tim thay {0}!", maHang);
                                ThongBao.WriteLine(resultSetHangHoa.ToString());
                                /*
                                 * "Lệnh break trong đoạn code không dư thừa. 
                                 * Nó được sử dụng để thoát khỏi vòng lặp do-while
                                 * khi tìm thấy mã hàng hóa trong danh sách. 
                                 * Nếu không có lệnh break, vòng lặp sẽ tiếp tục 
                                 * chạy cho đến khi điều kiện (resultSetHangHoa == null) == true trở thành false. 
                                 * Lệnh break giúp người dùng thoát khỏi vòng lặp 
                                 * ngay lập tức khi tìm thấy mã hàng hóa mà không 
                                 * cần phải chờ đợi điều kiện vòng lặp trở thành false." 
                                 */
                                break;
                            }
                        }
                        // TRUE: Vui lòng nhập lại mã hàng hóa
                        // FALSE: THOAT KHỎI VÒNG LẶP DO_WHILE
                        while ((resultSetHangHoa == null) == true);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(ThongBao.pressKeyToBeContinue);
                        Console.ReadKey();



                        /************************************************
                         *        BUOC 2: NHAP SO LUONG MUA HANG        *
                         * **********************************************
                         * Đoạn code này kiểm tra số lượng hàng hóa     *
                         * trong kho.                                   *
                         * Nó yêu cầu người dùng nhập số lượng hàng     *
                         * hóa muốn                                     *
                         * mua và kiểm tra xem số lượng đó có hợp lệ    *
                         * hay không.                                   *
                         * Nếu số lượng nhập vào lớn hơn số lượng hàng  *
                         * hóa trong                                    *
                         * kho thì đoạn code sẽ thông báo “Không hợp lệ”* 
                         * và yêu cầu người dùng nhập lại.              *
                         * Nếu số lượng nhập vào hợp lệ thì đoạn code   *
                         * sẽ thêm                                      *
                         * sản phẩm vào giỏ hàng và hiển thị thông tin  *
                         * giỏ hàng.                                    *
                         ***********************************************/

                        /**********************************************
                        *               CLEAR ALL SCREEN              *
                        ***********************************************/
                        Console.Clear();//Qua  buoc 2
                        // Kiểm tra số lượng
                        ThongBao.h1_center_1("Buoc 2: Kiem Tra so luong ton kho");
                        int soluong;

                        do
                        {
                            ThongBao.hr();
                            ThongBao.WriteLine(resultSetHangHoa.ToString());
                            ThongBao.hr();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Nhap so luong mua hang: ");
                            int.TryParse(Console.ReadLine(), out soluong);
                            // danh sach gio hang

                            if (soluong <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("So luong: " + soluong);
                                break;
                            }

                            // SL nhập không hợp lệ!
                            if (resultSetHangHoa.SoLuongNhapKho < soluong)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("SL Khong hop le {0}!", soluong);
                            }
                            // SL nhập hợp lệ!
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("SL Hop le {0}!", soluong);

                                // Bỏ sản phẩm vào giỏ hàng
                                resultSetGioHangItem = new GioHangItem(resultSetHangHoa, soluong);
                                Console.WriteLine();
                                dsGioHang.Them(resultSetGioHangItem);

                                Console.ForegroundColor = ConsoleColor.White;
                                /***********************************************
                                *            BUOC 4: IN GIO HANG               *
                                ************************************************/
                                // xuat danh sach item trong gio hang
                                ThongBao.h1_center_1("Gio hang hien co: ".ToUpper() + dsGioHang.Count + " SP");
                                dsGioHang.XuatDS();

                                /**
                                 * "Lệnh break trong đoạn code không dư thừa. 
                                 * Nó được sử dụng để thoát khỏi vòng lặp do-while 
                                 * khi số lượng nhập vào hợp lệ. Nếu không có 
                                 * lệnh break, vòng lặp sẽ tiếp tục chạy cho 
                                 * đến khi điều kiện (resultSetHangHoa.SoLuongNhapKho > soluong) == false trở thành false. 
                                 * Lệnh break giúp người dùng thoát khỏi vòng 
                                 * lặp ngay lập tức khi số lượng nhập vào hợp 
                                 * lệ mà không cần phải chờ đợi điều kiện vòng
                                 * lặp trở thành false." 
                                 */
                                break;
                            }
                        }
                        // TRUE: Vui lòng nhập lại số lượng
                        // FALSE: THOAT VONG LAP
                        while ((soluong == 0) == true || (resultSetHangHoa.SoLuongNhapKho > soluong) == false);
                        // DA NHAP XONG SO LUONG 
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(ThongBao.pressKeyToBeContinue);



                        /***********************************************
                         *      BUOC 3: MUA TIEP SAN PHAM HAY KHONG    *
                         ***********************************************/
                        ThongBao.h1_center_1("Buoc 3: mua tiep san pham hay khong?");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("1. tiep tuc");
                        Console.Write($"Vui long nhap command hoac '-1' de ket thuc buoc 3: ");
                        Console.ForegroundColor = ConsoleColor.White; int.TryParse(Console.ReadLine(), out luaChonMenu);

                        // thoát khỏi vòng lặp do-while bước số 1 & 2
                        if (luaChonMenu == -1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Dung lai");

                        }
                        // quay lại bước số 3: Mua hàng tiếp
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Tiep tuc", soluong);
                            ThongBao.WriteLine(resultSetHangHoa.ToString());
                        }
                    }
                    // vui long nhap lai ma hang hoa de mua tiep san pham
                    while (luaChonMenu != -1);
                    // THOAT KHOI MENU GIO HANG

                    // kiem tra gio hang co san pham khong?
                    if (dsGioHang.Count == 0)
                    {
                        //end
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Gio hang: " + ThongBao.pressKeyToExit);
                        Console.ReadKey();
                        break;
                    }

                    /**********************************************
                    *        BUOC 5: THANH TOAN DON HANG          *
                    ***********************************************/
                    ThongBao.h1_center_1("Buoc 5: thanh toan don hang");
                    ThongBao.p_alert_writeline("Nhap don hang thanh cong. Don Hang dang cho xu ly!");
                    resultSetDonHang = new DonHang();
                    resultSetDonHang.NhapDonHang(dsGioHang);
                    resultSetDonHang.ToString();
                    dsDonHang.ThemDH(resultSetDonHang);

                    // THOAT KHOI DON HANG
                    /**********************************************
                    *     BUOC 6: XAC NHAN KET THUC DON HANG      *
                    ***********************************************/

                    ThongBao.h1_center_1("Buoc 6: ket thuc don hang?");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("1. tiep tuc");
                    Console.Write($"Vui long nhap command hoac '-1' de chot don buoc 6: ");
                    Console.ForegroundColor = ConsoleColor.White; int.TryParse(Console.ReadLine(), out luaChonMenu);

                    if (luaChonMenu == -1)
                    {

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Xu ly don hang moi");
                        ThongBao.hr();
                        ThongBao.p_alert_writeline("Xu ly don hang moi");

                        ThongBao.WriteLine(resultSetHangHoa.ToString());
                        break;
                    }
                }
                // vui long nhap lai don hang
                while (luaChonMenu != -1);
                // kiem tra danh sach don hang co hoa don khong?
                if (dsDonHang.Count == 0)
                {
                    //end
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Don hang: " + ThongBao.pressKeyToExit);
                    Console.ReadKey();
                    break;
                }
                /**********************************************
                *             BUOC 7: IN RA FILE              *
                ***********************************************/
                // xuat danh sach item trong danh sach hoa don
                ThongBao.h1_center_1("Danh sach hoa don hien co: ".ToUpper() + dsDonHang.Count + " HD");
                dsDonHang.XuatDS();
                Console.ForegroundColor = ConsoleColor.Red;
                ThongBao.hr();
                ThongBao.p_alert_writeline("in ra file DonHang.txt");
                dsDonHang.GhiFile();
            }
            // vui long nhap lai menu
            while (luaChonMenu != -1);
        }

        /// <summary>
        /// Ham thu nghiem chuong trinh quan ly thong tin tong hop
        /// Ham duoc goi tai vi tri: program.cs
        /// access modifier: internal
        /// </summary>
        /// <param name="arrMenu"></param>
        internal static void ThuNghiemMainMenu()
        {
            string[] arrMenu = new string[] {
                "Hien thi thong tin hang hoa",
                "Tim Kiem thong tin hang hoa",
                "Dat hang",
                "Quan ly",
                "-1.Thoat chuong trinh",
            };

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < arrMenu.Length; i++)
            {
                dsMenuChinh.AddMenu(new Menu(arrMenu[i]));
            }
            do
            {
                /**********************************************
                *               CLEAR ALL SCREEN              *
                ***********************************************/
                Console.Clear();
                // tao menu chuong trinh 
                ThongBao.h1_center_1("Chuong trinh quan ly thong tin tong hop");


                /// menu chinh
                dsMenuChinh.Show();

                // nhap lenh
                Console.ForegroundColor = ConsoleColor.Yellow;
                ThongBao.hr();
                ThongBao.p_alert_write(ThongBao.pressKeyToEnterCommandOrExitVI);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White; int.TryParse(Console.ReadLine(), out luaChonMenu);
                try
                {
                    ThongBao.h1_center_1(dsMenuChinh.ShowMenuBySTT(luaChonMenu - 1).toString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                switch (luaChonMenu)
                {
                    case 1: //Hien thi thong tin hang hoa

                        // nhap item hang hoa vao danh sach
                        for (int i = 0; i < dsHangHoa.MaxSize; i++)
                        {
                            HangHoa hh = new HangHoa();
                            hh.NhapTuDong();
                            dsHangHoa.ThemHangHoa(hh);
                            Thread.Sleep(10);
                        }

                        // in so luong hang hoa trong danh sach hang hoa
                        Console.WriteLine("So luong hang hoa: {0}", dsHangHoa.Count);

                        // in tieu de cot hang hoa
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        for (int i = 0; i < arrColumnHangHoa.Length; i++)
                        {
                            Console.Write($"{arrColumnHangHoa[i],-15}");
                        }
                        Console.WriteLine();

                        // in danh sach
                        Console.ForegroundColor = ConsoleColor.White;
                        dsHangHoa.XuatDS();

                        luaChonMenu = 0;
                        Console.ReadKey();
                        break;

                    case 2: // tim kiem thong tin hàng hóa bằng tên hàng hóa
                        ThuNghiemTimKiemTheoTieuChi();
                        luaChonMenu = 0;//reset
                        Console.ReadKey();
                        break;
                    case 3: // dat hang  
                        ThuNghiemDatHang();
                        luaChonMenu = 0;//reset
                        break;

                    case 4: // Kiem tra quan ly tai khoan 
                        ThuNghiemLuaChonDangNhapDangKy();
                        luaChonMenu = 0;//reset
                        Console.ReadKey();
                        break;
                    default: break;

                }

            }
            // TRUE  exit 
            // FALSE continue
            while (luaChonMenu != -1);
        }
        /// <summary>
        /// Ham chon lua chon: 
        /// 1.chuc nang dang nhap 
        /// 2.chuc nang dang ky
        /// </summary>
        public static void ThuNghiemLuaChonDangNhapDangKy()
        {
            /**********************************************
            *              DANG NHAP * DANG KY            *
            ***********************************************/
            // khai bao danh sach menu
            DanhSachMenu dsMenuQuanLy = new DanhSachMenu();
            string[] arrMenu = new string[] {
                "Dang nhap",
                "Dang ky",
                "-1. Quay ve",
            };

            //them menu
            for (int i = 0; i < arrMenu.Length; i++)
            {
                dsMenuQuanLy.AddMenu(new Menu(arrMenu[i]));
            }

            do
            {
                /**********************************************
                *               CLEAR ALL SCREEN              *
                ***********************************************/
                Console.Clear();
                // tao menu chuong trinh 
                ThongBao.h1_center_1("Chuong trinh lua chon dang nhap || dang ky");


                /// menu chinh
                Console.ForegroundColor = ConsoleColor.White;
                dsMenuQuanLy.Show();

                // nhap lenh
                ThongBao.hr();
                ThongBao.p_alert_write(ThongBao.pressKeyToEnterCommandOrExitVI);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White; int.TryParse(Console.ReadLine(), out luaChonMenu);
                try
                {
                    ThongBao.h1_center_1(dsMenuQuanLy.ShowMenuBySTT(luaChonMenu - 1).toString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                switch (luaChonMenu)
                {
                    case 1: // Dang nhap
                        ThongBao.h1_center_1("Dang nhap".ToUpper());
                        ThuNghiemDangNhap();
                        //end

                        ThongBao.p_success_writeline(ThongBao.pressKeyToContinue);
                        Console.ReadKey();
                        break;
                    case 2: // Dang ky
                        ThongBao.h1_center_1("Dang ky".ToUpper());
                        ThuNghiemDangKy();
                        //end

                        ThongBao.p_success_writeline(ThongBao.pressKeyToContinue);
                        Console.ReadKey();
                        break;
                    default:
                        {
                            Console.ReadKey();
                            break;
                        }
                }

            } while (luaChonMenu != -1);
        }
        public static void ThuNghiemChucNangQuanLyHangHoa()
        {
            /**********************************************
            *    THU NGHIEM CHUC NANG QUAN LY DON HANG    *
            ***********************************************/
            string maHang;
            // khai bao danh sach menu
            DanhSachMenu dsMenuQuanLyHangHoa = new DanhSachMenu();
            string[] arrMenu = new string[] {
                "Them 1 hang hoa",
                "Xoa 1 hang hoa",
                "Sua 1 hang hoa",
                "HienThi",
                "-1.Quay ve",
            };

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < arrMenu.Length; i++)
            {
                dsMenuQuanLyHangHoa.AddMenu(new Menu(arrMenu[i]));
            }
            do
            {
                /**********************************************
                *               CLEAR ALL SCREEN              *
                ***********************************************/
                Console.Clear();
                // tao menu chuong trinh 
                ThongBao.h1_center_1("THU NGHIEM CHUC NANG QUAN LY hang hoa".ToUpper());


                /// menu chinh
                dsMenuQuanLyHangHoa.Show();

                // nhap lenh
                ThongBao.hr();
                ThongBao.p_alert_write(ThongBao.pressKeyToEnterCommandOrExitVI);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White; int.TryParse(Console.ReadLine(), out luaChonMenu);
                try
                {
                    ThongBao.h1_center_1(dsMenuQuanLyHangHoa.ShowMenuBySTT(luaChonMenu - 1).toString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                switch (luaChonMenu)
                {
                    case 1: // Them
                        ThongBao.p_success_writeline("--THEM: Nhap thong tin hang hoa");
                        resultSetHangHoa = new HangHoa();
                        resultSetHangHoa.NhapTuDong();
                        dsHangHoa.ThemHangHoa(resultSetHangHoa);
                        // hien thi
                        dsHangHoa.XuatDS();

                        luaChonMenu = 0;
                        ThongBao.p_exit();
                        break;
                    case 2: // Xoa
                        dsHangHoa.XuatDS();
                        ThongBao.p_success_write("--XOA: Nhap ma hang: ");

                        maHang = Ultilities.NhapSoNguyen().ToString();

                        resultSetHangHoa = dsHangHoa.SearchByMaHang(maHang);
                        if (resultSetHangHoa != null)
                        {
                            ThongBao.p_success_writeline($"Tim thay {resultSetHangHoa.MaHang}".ToUpper());
                            resultSetHangHoa.ToString();
                            bool check = dsHangHoa.XoaHangHoa(resultSetHangHoa);
                            if (check)
                            {
                                ThongBao.p_success_writeline("Xoa thanh cong!");
                            }
                            else
                            {
                                ThongBao.p_alert_writeline("Xoa that bai");
                            }
                            dsHangHoa.XuatDS();
                        }
                        else
                        {
                            ThongBao.p_alert_writeline($"Khong tim thay {maHang}!");
                        }
                        //end
                        luaChonMenu = 0;
                        ThongBao.p_exit();
                        break;
                    case 3: // Sua
                        dsHangHoa.XuatDS();
                        ThongBao.p_success_write("--SUA: Nhap ma hang: ");

                        maHang = Ultilities.NhapSoNguyen().ToString();

                        resultSetHangHoa = dsHangHoa.SearchByMaHang(maHang);
                        if (resultSetHangHoa != null)
                        {
                            ThongBao.p_success_writeline($"Tim thay {resultSetHangHoa.MaHang}".ToUpper());
                            resultSetHangHoa.ToString();

                            ThongBao.p_success_write("--SUA: Nhap so luong: ");
                            int soLuong = Ultilities.NhapSoNguyen();

                            bool check = dsHangHoa.SuaSoLuongHangHoa(resultSetHangHoa, soLuong);
                            if (check)
                            {
                                ThongBao.p_success_writeline("Sua thanh cong!");
                            }
                            else
                            {
                                ThongBao.p_alert_writeline("Sua that bai");
                            }
                            dsHangHoa.XuatDS();
                        }
                        else
                        {
                            ThongBao.p_alert_writeline($"Khong tim thay {maHang}!");
                        }
                        //end
                        luaChonMenu = 0;
                        ThongBao.p_exit();
                        break;
                    case 4:
                        ThongBao.hr();
                        dsHangHoa.XuatDS();
                        dsHangHoa.GhiFile("HangHoa.txt");
                        ThongBao.p_exit();
                        break;
                    default: break;
                }

            } while (luaChonMenu != -1);
        }
        public static void ThuNghiemChucNangQuanLy()
        {
            /**********************************************
            *        THU NGHIEM CHUC NANG QUAN LY         *
            ***********************************************/
            // khai bao danh sach menu
            DanhSachMenu dsMenuQuanLyThongTin = new DanhSachMenu();
            string[] arrMenu = new string[] {
                "Xu ly don hang",
                "Quan ly hang hoa",
                "-1. Quay ve",
            };

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < arrMenu.Length; i++)
            {
                dsMenuQuanLyThongTin.AddMenu(new Menu(arrMenu[i]));
            }
            do
            {
                /**********************************************
                *               CLEAR ALL SCREEN              *
                ***********************************************/
                Console.Clear();
                // tao menu chuong trinh 
                ThongBao.h1_center_1("Chuong trinh quan ly thong tin");


                /// menu chinh
                dsMenuQuanLyThongTin.Show();

                // nhap lenh
                ThongBao.hr();
                ThongBao.p_alert_write(ThongBao.pressKeyToEnterCommandOrExitVI);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White; int.TryParse(Console.ReadLine(), out luaChonMenu);
                try
                {
                    ThongBao.h1_center_1(dsMenuQuanLyThongTin.ShowMenuBySTT(luaChonMenu - 1).toString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                switch (luaChonMenu)
                {
                    case 1: // xu ly don hang (xoa 1 pt dau danh sach)
                        ThongBao.hr();
                        ThongBao.p_success_write("So Luong: " + dsDonHang.Count);

                        dsDonHang.XoaDH();
                        ThongBao.hr();
                        ThongBao.p_alert_write("So Luong: " + dsDonHang.Count);

                        if (dsDonHang.Count >= 0)
                        {
                            ThongBao.p_success_writeline("Xoa thanh cong!");
                            ThongBao.h1_center_1("Don hang ban dau sau khi xoa");
                            ThongBao.hr();
                            dsDonHang.XuatDS();
                        }
                        else
                        {
                            ThongBao.p_alert_writeline("Xoa that bai!");
                        }
                        //end
                        luaChonMenu = 0;
                        ThongBao.p_exit();
                        break;
                    case 2: // Quan ly hang hoa
                        ThuNghiemChucNangQuanLyHangHoa();
                        //end
                        luaChonMenu = 0;
                        ThongBao.p_exit();
                        break;
                    default: break;
                }

            } while (luaChonMenu != -1);
        }

        /// <summary>
        /// ham thu nghiem dang nhap
        /// </summary>
        public static void ThuNghiemDangKy()
        {
            string username;
            string password;
            string repassword;
            TaiKhoan account;
            do
            {
                /**********************************************
                *               CLEAR ALL SCREEN              *
                ***********************************************/
                Console.Clear();

                /**********************************************
                *             GUI GIAO DIEN DANG KY           *
                ***********************************************/
                // GUI GIAO DIEN DANG NHAP
                ThongBao.h1_center_1("Dang ky tai khoan moi");
                // nhap ten dang nhap

                ThongBao.p_success_write("Ten dang nhap: ");
                Console.ForegroundColor = ConsoleColor.White;
                username = Ultilities.NhapChuoi();

                // nhap mat khau

                ThongBao.p_success_write("Mat khau: ");
                Console.ForegroundColor = ConsoleColor.White;
                password = Ultilities.NhapChuoi();

                // nhap lai mat khau

                ThongBao.p_success_write("Re Mat khau: ");
                Console.ForegroundColor = ConsoleColor.White;
                repassword = Ultilities.NhapChuoi();

                account = new TaiKhoan(username, password, repassword);

                // check tai khoan khi dang ky
                if (account.KiemTraDangKy() != true)
                {
                    ThongBao.p_alert_writeline("Nhap mat khau khong chinh xac!");
                    ThongBao.h1_center_1("Buoc 2: ket thuc dang ky?");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("1. tiep tuc");
                    Console.Write($"Vui long nhap command hoac '-1' de ket thuc dang ky: ");
                    Console.ForegroundColor = ConsoleColor.White; int.TryParse(Console.ReadLine(), out luaChonMenu);
                }
                else
                {
                    ThongBao.p_success_writeline("Dang ky thanh cong!");
                    dsTaiKhoan.Add(account);
                    dsTaiKhoan.GhiFile();
                    break;
                }
            }
            // nhap sai dang ky 3 lan thoat chuong trinh
            while (luaChonMenu != -1 || (account.KiemTraDangKy() != true) == true);
        }


        /// <summary>
        /// ham thu nghiem dang nhap
        /// </summary>
        public static void ThuNghiemDangNhap()
        {
            string username;
            string password;
            do
            {
                /**********************************************
                *               CLEAR ALL SCREEN              *
                ***********************************************/
                Console.Clear();
                ThongBao.p_alert_writeline("So lan dang nhap sai: " + countDangNhap);
                /**********************************************
                *             GUI GIAO DIEN DANG NHAP         *
                ***********************************************/
                // GUI GIAO DIEN DANG NHAP
                ThongBao.h1_center_1("Dang nhap he thong");

                // nhap ten dang nhap
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                ThongBao.p_success_write("Ten dang nhap: ");
                Console.ForegroundColor = ConsoleColor.White;
                username = Ultilities.NhapChuoi();

                // nhap mat khau
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                ThongBao.p_success_write("Mat khau: ");
                Console.ForegroundColor = ConsoleColor.White;
                password = Ultilities.NhapChuoi();

                TaiKhoan account = new TaiKhoan(username, password);
                // check tai khoan khi dang nhap
                bool conn = Connection.getConnection(account, "Admin.txt");

                if (conn == true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    ThongBao.p_success_writeline("Dang nhap thanh cong!");
                    ThuNghiemChucNangQuanLy();
                    break;//CHUONG TRINH QUAN LY THONG TIN TONG HOP
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    ThongBao.p_alert_writeline("Ten dang nhap hoac mat khau khong chinh xac!");
                    countDangNhap++;
                }
            }
            // nhap sai dang nhap 3 lan thoat chuong trinh
            while (countDangNhap < 3);

            // show thong bao
            if (countDangNhap == 3)
            {
                ThongBao.h1_center_1("BAN DANG NHAP SAI " + countDangNhap + " LAN. THOAT CHUONG TRINH".ToUpper());
            }
        }
    }
}
