using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Linq;

/**
 * Sok Kim Thanh 
 * 30/05/2023 10:27 CH
 * Danh sách liên kết đơn tham số kiểu generic
 */
namespace BT_TONGHOP_SokKimThanh
{
    public class MyLinkedList<ThamSoKieu>
    {
        private Node<ThamSoKieu> first;
        private Node<ThamSoKieu> last;
        private int size = 0;



        public MyLinkedList()
        {
            first = null;
            last = null;
            size = 0;
        }

        public Node<ThamSoKieu> First { get => first; private set { first = value; } }
        public Node<ThamSoKieu> Last { get => last; private set { first = value; } }
        public int Count { get => size; private set { size = value; } }

        /// <summary>
        /// Hàm int danh sách liên kết
        /// </summary>
        public void Print()
        {
            if (first != null)
            {
                Node<ThamSoKieu> p = first;
                while (p != null)
                {
                    Console.WriteLine(p.Data.ToString());
                    p = p.Next;
                    Thread.Sleep(10);
                }
                Console.WriteLine();
            }
            else
            {
                throw new Exception("Danh sach rong");
            }


        }
        /// <summary>
        /// Hàm add first để thêm một nút mới vào đầu danh sách liên kết
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Node<ThamSoKieu> AddFirst(ThamSoKieu data)
        {
            Node<ThamSoKieu> newNode = new Node<ThamSoKieu>(data);
            // kiem tra danh sach rong
            if (first == null)
            {
                first = newNode;
                last = newNode;
            }
            else
            {
                // truong next cua newNode tro den first
                newNode.Next = first;
                // first tro den newNode
                first = newNode;
            }
            size++;
            return newNode;
        }
        /// <summary>
        /// Hàm add last để thêm một nút mới vào cuối danh sách liên kết
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Node<ThamSoKieu> AddLast(ThamSoKieu data)
        {
            Node<ThamSoKieu> newNode = new Node<ThamSoKieu>(data);
            // kiem tra danh sach rong
            if (first == null)
            {
                first = newNode;
                last = newNode;
            }
            else
            {
                // truong next cua last tro den newnode
                last.Next = newNode;
                // last tro den newNode
                last = newNode;
            }
            size++;
            return newNode;
        }
        /// <summary>
        /// Hàm thêm vào sau
        /// </summary>
        /// <param name="pre"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Node<ThamSoKieu> AddAfter(Node<ThamSoKieu> pre, ThamSoKieu data)
        {
            Node<ThamSoKieu> newNode;
            //TH1: pre la null
            if (pre != null)
            {
                //TH2: danh sach rong hoac pre chinh la last 
                if (first == null || pre == last)
                {
                    newNode = AddLast(data);
                }
                else
                {
                    //TH3: pre nam o doan giua cua danh sach
                    newNode = new Node<ThamSoKieu>(data);
                    // truong next cua newnode tro vao truong next cua pre
                    newNode.Next = pre.Next;
                    // truong next cua pre tro vao dau cua newnode
                    pre.Next = newNode;
                    size++;
                }
            }
            else
            {
                throw new Exception("pre khong duoc phep trong");
            }
            return newNode;
        }
        /// <summary>
        /// Ham add before 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Node<ThamSoKieu> AddBefore(Node<ThamSoKieu> node, ThamSoKieu data)
        {
            Node<ThamSoKieu> newNode;
            //TH1: pre la null
            if (node != null)
            {
                //TH2: danh sach rong hoac pre chinh la last 
                if (first == null || node == first)
                {
                    newNode = AddFirst(data);
                }
                else
                {
                    //TH3: pre nam o doan giua cua danh sach
                    // tim node phia truoc
                    Node<ThamSoKieu> nodePhiaTruoc = FindNodeBefore(node);
                    // them vao sau node tim thay
                    newNode = AddAfter(nodePhiaTruoc, data);
                }
            }
            else
            {
                throw new Exception("pre khong duoc phep trong");
            }
            return newNode;
        }
        // RemoveFirst
        // remove first check  TH: danh sach rong 
        public Node<ThamSoKieu> RemoveFirst()
        {
            //TH1: danh sach rong
            Node<ThamSoKieu> nodeTemp;
            if (first == null)
            {
                throw new Exception("Danh sach rong");
            }
            else
            {
                // node tam tro vao first
                nodeTemp = first;
                // first tro vao truong next cua first
                first = first.Next;
                size--;
            }
            return nodeTemp;
        }
        // RemoveFirst
        // remove first check  TH: danh sach rong 
        public Node<ThamSoKieu> RemoveLast()
        {
            //TH1: danh sach rong
            Node<ThamSoKieu> temp = last;
            if (Count == 0)
            {
                throw new ArgumentNullException("Danh sach rong");
            }
            else
            {    // tim node trước node last (last trỏ vào node phía trước nó)
                last = FindNodeBefore(last);
                // trường next của last trỏ vào null (xóa node)
                last.Next = null;
                size--;
            }
            return temp;
        }

        public Node<ThamSoKieu> Remove(ThamSoKieu value)
        {
            // Dam bao tim thay node ton tai
            Node<ThamSoKieu> nodeToDelete = FindFirstValue(value);

            //TH1: p la null
            if (nodeToDelete != null)
            {
                //th2: p la first
                if (nodeToDelete == first)
                {
                    RemoveFirst();
                }
                // th3: p la last
                else if (nodeToDelete == last)
                {
                    RemoveLast();
                }
                // th4: p nam o giua
                else
                {
                    // tim thang phia truoc thang bi xoa
                    Node<ThamSoKieu> nodePhiaTruoc = FindNodeBefore(nodeToDelete);
                    // truong next cua thang phia truoc tro vao truong next cua thang bi xoa
                    nodePhiaTruoc.Next = nodeToDelete.Next;
                    size--;
                }
            }
            else
            {
                throw new Exception("node can xoa khong ton tai trong danh sach");
            }
            return nodeToDelete;
        }

        public Node<ThamSoKieu> FindFirstValue(ThamSoKieu key)
        {
            if (key == null)
            {
                throw new Exception("key khong duoc de trong");
            }

            for (Node<ThamSoKieu> p = First; p != null; p = p.Next)
            {
                if (p.Data.Equals(key))
                {
                    return p;
                }
            }
            return null;
        }

        public Node<ThamSoKieu> FindNodeBefore(Node<ThamSoKieu> newNode)
        {
            Node<ThamSoKieu> nodeHienTai = first;
            if (newNode == null || nodeHienTai.Next == null)
            {
                throw new NullReferenceException("Danh sach rong");
            }
            while (nodeHienTai.Next != newNode)
            {
                nodeHienTai = nodeHienTai.Next;
            }
            return nodeHienTai;
        }

        public void ReadFromFile(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    AddFirst((ThamSoKieu)Convert.ChangeType(line, typeof(ThamSoKieu)));
                }
            }
        }

        public void WriteToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                Node<ThamSoKieu> current = First;
                while (current != null)
                {
                    writer.WriteLine(current.Data);
                    current = current.Next;
                }
            }
        }
        //Insertion sort
        /*
            1) Create an empty sorted (or result) list
            2) Traverse the given list, do following for every node.
                a) Insert current node in sorted way in sorted or result list.
            3) Change head of given linked list to head of sorted (or result) list.
        */
        public MyLinkedList<ThamSoKieu> InsertionSort()
        {
            Node<ThamSoKieu> p = null;
            MyLinkedList<ThamSoKieu> sortedList = new MyLinkedList<ThamSoKieu>();

            Node<ThamSoKieu> q = null;
            for (p = first; p != null; p = p.Next)
            {

                for (q = sortedList.First; q != null; q = q.Next)
                {
                    if (p.Data.Equals(q.Data))
                    {
                        break;
                    }
                }
                //Them gia tri cuar phan tu p vao day da sap xep
                sortedList.AddBefore(q, p.Data);
            }
            return sortedList;
        }

        //Xoa tat ca cac node co gia tri value trong day
        public void RemoveAll(ThamSoKieu value)
        {
            Node<ThamSoKieu> p = null;
            do
            {
                p = Remove(value);
            } while (p != null);
        }


        //Xoa day
        public void Clear()
        {
            first = null;
            last = null;
            size = 0;
        }


        //Kiem tra phan tu value co trong day hay khong
        public bool Contains(ThamSoKieu value)
        {
            Node<ThamSoKieu> p = FindFirstValue(value);
            if (p == null)
            {
                return false;
            }
            return true;
        }
    }
}
