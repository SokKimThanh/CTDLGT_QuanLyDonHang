using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_TONGHOP_SokKimThanh
{
    class DanhSachHangHoa
    {
        private ArrayList<HangHoa> data;
        private int capacity;
        private int size;
        private string filePath;

        internal ArrayList<HangHoa> Data { get => data; set => data = value; }
        public int MaxSize { get => capacity; }
        public string FilePath { get => filePath; set => filePath = value; }
        public int Count { get => size; }

        public DanhSachHangHoa(int maxSize)
        {
            this.capacity = maxSize;
            this.data = new ArrayList<HangHoa>(capacity);
            this.filePath = "DonHang.txt";
            size = 0;
        }
        //indexer
        public HangHoa this[int index]
        {
            get
            {
                if (index < 0 || index >= size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return Data[index];
            }
            set
            {
                this[index] = value;
            }
        }


        // Hàm Them thêm một phần tử vào cuối mảng Data
        public void ThemHangHoa(HangHoa item)
        {
            Data.AddLast(item);
            size++;
        }

        // Hàm Them thêm một phần tử vào cuối mảng Data
        public bool ThemHangHoaBool(HangHoa item)
        {
            size++;
            return Data.AddLastBool(item);
        }
        // Hàm Xoa xóa phần tử đầu tiên trong mảng items
        public bool XoaHangHoa(HangHoa item)
        {
            size--;
            return Data.Remove(item);
        }

        public void XuatDS()
        {
            try
            {
                if (Count == 0)
                {
                    ThongBao.p_alert_writeline("--Danh sach rong--".ToUpper());
                    return;
                }

                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < data.Count; i++)
                {
                    Console.WriteLine(data[i].ToString());
                }
            }
            catch (NullReferenceException ex)
            {
                ThongBao.p_alert_writeline(ex.Message);
            }
        }


        // tìm kiếm hàng hóa theo ma
        public HangHoa SearchByMaHang(string maHang)
        {
            try
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    if (data[i].MaHang.ToLower().CompareTo(maHang.ToLower()) == 0)
                    {
                        return data[i];
                    }
                }
            }
            catch (Exception ex)
            {
                ThongBao.p_alert_writeline(ex.Message);
            }
            return null;
        }
        // tìm kiếm hàng hóa theo tên
        public HangHoa SearchByTenHang(string tenHang)
        {
            try
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    if (data[i].TenHang.ToLower().CompareTo(tenHang.ToLower()) == 0)
                    {
                        return data[i];
                    }
                }
            }
            catch (Exception ex)
            {
                ThongBao.p_alert_writeline(ex.Message);
            }
            return null;
        }

        private static HangHoa parseLine(string line)
        {
            string[] arr = line.Split('#');
            HangHoa item = new HangHoa();
            item.MaHang = arr[0];
            item.TenHang = arr[1];
            item.SoLuongNhapKho = int.Parse(arr[2]);
            item.NgayNhapKho = new DateTime(arr[3][2], arr[3][1], arr[3][0]);
            item.GiaBan = double.Parse(arr[4]);
            item.SoLuongNhapKho = int.Parse(arr[5]);
            item.NoiSanXuat = arr[6];
            return item;
        }
        public void DocFile(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    HangHoa item = parseLine(line);
                    ThemHangHoa(item);
                }
            }
        }
        public void GhiFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < data.Count; i++)
                {
                    writer.WriteLine(data[i].InRaFile());
                }
            }
        }

        public bool SuaSoLuongHangHoa(HangHoa resultSetHangHoa, int soLuong)
        {

            // khong hop le
            if (resultSetHangHoa == null)
            {
                return false;
            }
            // tim kiem
            foreach (HangHoa item in data.Data)
            {
                if (item == resultSetHangHoa)
                {
                    item.SoLuongNhapKho = soLuong;
                    return true;
                }
            }
            return false;
        }
    }
}
