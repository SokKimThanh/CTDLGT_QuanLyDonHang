using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_TONGHOP_SokKimThanh
{
    public interface ISapXepTimKiem
    {
        void InterChangeSort();
        //void SelectionSort();
        void BinarySearch(HangHoa key);
        Node<HangHoa> SequentialSearch(string maHangHoa);
    }
}
