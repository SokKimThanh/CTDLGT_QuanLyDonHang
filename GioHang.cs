

namespace BT_TONGHOP_SokKimThanh
{
    public class GioHangItem
    {
        private HangHoa hangHoa;
        private int soLuong;

        public GioHangItem(HangHoa hangHoa, int soLuong)
        {
            // tru so luong don hang
            this.hangHoa = hangHoa;
            this.soLuong = soLuong;
            hangHoa.SoLuongNhapKho -= SoLuong;
        }

        public int SoLuong { get => soLuong; set => soLuong = value; }
        internal HangHoa HangHoa { get => hangHoa; set => hangHoa = value; }

        public double ThanhTien()
        {
            return SoLuong * this.HangHoa.GiaBan;
        }

        public override string ToString()
        {
            return $"{hangHoa.ToString()}{soLuong,-15}{ThanhTien().ToString("C"),-15}";
        }
    }
}
