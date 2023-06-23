using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * Sok Kim Thanh
 * date 30/05/2023
 * Lớp node được viết theo lập trình tổng quát
 */
namespace BT_TONGHOP_SokKimThanh
{
    /// <summary>
    /// Node có tham số kiểu tùy biến
    /// </summary>
    /// <typeparam name="ThamSoKieu"></typeparam>
    public class Node<ThamSoKieu>
    {
        private ThamSoKieu data;
        private Node<ThamSoKieu> next;



        public Node(ThamSoKieu data)
        {
            this.data = data;
            this.next = null;
        }

        public ThamSoKieu Data { get => data; set => data = value; }
        public Node<ThamSoKieu> Next { get => next; set => next = value; }

        /// <summary>
        /// check data node hiện tại và data node kế tiếp
        /// </summary>
        /// <returns>string</returns>
        public string toString()
        {
            if (next == null)
            {
                throw new Exception("Khong co node tiep theo");
            }

            return $"data: {data} - next: {next.data}";
        }
    }
}
