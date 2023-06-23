using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BT_TONGHOP_SokKimThanh
{
    public class DanhSachHangHoaLinkedList : ISapXepTimKiem
    {
        private MyLinkedList<HangHoa> data;

        public MyLinkedList<HangHoa> Data { get => data; private set => data = value; }

        public DanhSachHangHoaLinkedList()
        {
            this.Data = new MyLinkedList<HangHoa>();
        }

        public void XuatDS()
        {
            try
            {
                Data.Print();
            }
            catch (Exception ex)
            {
                ThongBao.p_alert_writeline(ex.Message);
            }
        }

        public void ThemDau(HangHoa item)
        {
            Data.AddFirst(item);
        }

        public void ThemCuoi(HangHoa item)
        {
            Data.AddLast(item);
        }

        /// <summary>
        /// Them vào giữa
        /// </summary>
        /// <param name="pre"></param>
        /// <param name="item"></param>
        public void ThemSau(Node<HangHoa> pre, HangHoa item)
        {
            data.AddAfter(pre, item);
        }



        /*
         *      Điều kiện list.Count > 0 trong hàm RemoveLast 
         * được sử dụng để kiểm tra xem danh sách có phần
         * tử nào hay không trước khi thực hiện thao tác 
         * loại bỏ phần tử cuối cùng. Nếu danh sách không 
         * có phần tử nào (tức là list.Count == 0),
         * thì không thể loại bỏ phần tử cuối cùng được 
         * và hàm sẽ không làm gì cả.
         *      Nếu bạn bỏ qua điều kiện này và thực hiện thao tác loại
         * bỏ phần tử cuối cùng trên một danh sách rỗng, bạn
         * sẽ gặp lỗi InvalidOperationException với thông 
         * báo lỗi “The LinkedList is empty.” (Danh sách liên kết rỗng).
         * Điều kiện list.Count > 0 giúp tránh lỗi này và 
         * đảm bảo rằng hàm RemoveLast chỉ thực hiện thao 
         * tác loại bỏ phần tử cuối cùng khi danh sách có 
         * ít nhất một phần tử.
         */
        public Node<HangHoa> XoaCuoi()
        {
            Node<HangHoa> newNode = null;
            try
            {
                newNode = Data.RemoveLast();
            }
            catch (Exception ex)
            {
                ThongBao.p_alert_writeline(ex.Message);
            }
            return newNode;
        }

        public Node<HangHoa> XoaDau()
        {

            Node<HangHoa> newNode = null;
            try
            {
                newNode = Data.RemoveFirst();
            }
            catch (Exception ex)
            {
                ThongBao.p_alert_writeline(ex.Message);
            }
            return newNode;
        }


        public Node<HangHoa> Xoa(HangHoa item)
        {
            Node<HangHoa> newNode = null;
            try
            {
                newNode = Data.Remove(item);
            }
            catch (Exception ex)
            {
                ThongBao.p_alert_writeline(ex.Message);
            }
            return newNode;
        }

        private void Swap(ref Node<HangHoa> a, ref Node<HangHoa> b)
        {
            HangHoa temp = a.Data;
            a.Data = b.Data;
            b.Data = temp;
        }

        /* Hàm InterChangeSort để sắp xếp một danh sách liên kết đơn theo thứ tự tăng dần*/
        /// <summary>
        /// Sắp xếp đổi chỗ trực tiếp
        /// </summary>
        public void InterChangeSort()
        {
            if (Data.Count > 1)
            {
                for (Node<HangHoa> i = Data.First; i != Data.Last; i = i.Next)
                {
                    for (Node<HangHoa> j = i.Next; j != null; j = j.Next)
                    {
                        if (i.Data.CompareToGiaBan(j.Data) > 0)
                        {
                            Swap(ref i, ref j);
                        }
                    }
                }
            }
        }

        public void BinarySearch(HangHoa key)
        {

        }

        //Tim Node co gia tri nho nhat tu startNode dden last
        public Node<HangHoa> FindMinNode(Node<HangHoa> startNode)
        {
            Node<HangHoa> minNode = null;
            if (startNode == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                Node<HangHoa> p = null;
                HangHoa min = startNode.Data;
                minNode = startNode;
                for (p = startNode; p != null; p = p.Next)
                {
                    if (min.CompareToMaHang(p.Data) > 0)
                    {
                        min = p.Data;
                        minNode = p;
                    }
                }
            }
            return minNode;
        }
        /// <summary>
        /// săp xếp lựa chọn trực tiếp
        /// </summary>
        /// <param name="value"></param>
        public void SelectionSort(HangHoa value)
        {
            Node<HangHoa> minNode = data.First;
            //Tim vi tri phan tu min trong khoang tu i den cuoi mang
            for (Node<HangHoa> p = data.First; p != data.Last; p = p.Next)
            {
                //Tim vi tri min
                minNode = FindMinNode(p);
                //Doi cho phan tu min va p
                if (minNode != null && p.Data.CompareToGiaBan(minNode.Data) > 0)
                {
                    Swap(ref p, ref minNode);
                }
            }
        }

        /// <summary>
        /// Tìm theo mã hàng, tên hàng
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Node<HangHoa> SequentialSearch(string maHangHoa)
        {
            for (Node<HangHoa> p = Data.First; p != null; p = p.Next)
            {
                if (p.Data.MaHang.ToLower().CompareTo(maHangHoa.ToLower()) == 0)
                {
                    return p;
                }
            }
            return null;
        }

        //Xoa tat ca cac node co gia tri value trong day
        public void RemoveAll(HangHoa value)
        {
            data.RemoveAll(value);
        }
        //Xoa day
        public void Clear()
        {
            data.Clear();
        }
        //Kiem tra phan tu value co trong day hay khong
        public bool Contains(HangHoa value)
        {
            return data.Contains(value);
        }
    }
}
