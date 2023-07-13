using System;
using System.Collections.Generic;
using static System.Console;

    class TimKiem
    {
        /// <summary>
        /// Hàm binary search all order
        /// </summary>
        /// <param name="arrInt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int[] BinarySearchAllWithKey(ref int[] arrInt, int key)
        {
            int[] indexBinaryArr = new int[0];
            int count = 0;
            int vitridautien = BinarySearch(arrInt, key);
            for (int i = vitridautien; i < arrInt.Length; i++)
            {
                if (arrInt[i] == key)
                {
                    Array.Resize(ref indexBinaryArr, indexBinaryArr.Length + 1);
                    indexBinaryArr[count++] = i;
                }
            }
            return indexBinaryArr;
        }
        public static int UnorderLinearSearch(int[] arr, int key)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == key)
                {
                    return i;
                }
            }
            return -1;
        }
        public static int[] SearchAllWithKey(int[] arr, int key)
        {
            // khai bao mang rong chua co phan tu nao
            List<int> arrIndex = new List<int>();
            bool check = false;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == key)
                {
                    arrIndex.Add(i);
                    check = true;
                }
            }
            if (check == false)
            {
                Write($"Khong tim thay {key}");
            }
            return arrIndex.ToArray();
        }
        /// <summary>
        /// Hàm xóa tất cả phần tử tại các vị trí của key
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="key"></param>
        public static void DeleteAllWithKey(ref int[] arr, int key)
        {
            for (int i = 0; i < arr.Length;)
            {
                // tim key
                if (arr[i] == key)
                {
                    for (int j = i; j < arr.Length - 1; j++)
                    {
                        arr[j] = arr[j + 1];
                    }
                    Array.Resize(ref arr, arr.Length - 1);
                }
                else
                {
                    i++;
                }
            }
        }
        /// <summary>
        /// Hàm xóa 1 phần từ
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="position"></param>
        public static void DeleteOne(ref int[] arr, int position)
        {
            for (int j = position; j < arr.Length - 1; j++)
            {
                arr[j] = arr[j + 1];
            }
            Array.Resize(ref arr, arr.Length - 1);
        }
        /// <summary>
        /// Hàm xóa tất cả vị trí với thuật toán search all linear search
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="key"></param>
        public static void DeleteAllWithSearchAllLinearSearch(ref int[] arr, int key)
        {
            int[] indexArr = SearchAllWithKey(arr, key);
            Ultilities.XuatLine(indexArr);
            if (indexArr.Length != 0)
            {
                for (int i = 0; i < indexArr.Length; i++)
                {
                    DeleteOne(ref arr, indexArr[i] - i);
                }
            }
        }
        /// <summary>
        /// Hàm xóa tất cả vị trí với thuật toán search one 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool DeleteAllWithSearchOneLinearSearch(ref int[] arr, int key)
        {
            int vT = -1;
            bool isDelete = false;
            int count = 0;// cach 2: neu count tang 1 -> bi xoa > 1 lan
            do
            {
                vT = UnorderLinearSearch(arr, key);
                if (vT != -1)
                {
                    DeleteOne(ref arr, vT);
                    isDelete = true;
                    count++;
                }

            } while (vT != -1);

            return isDelete;
        }
        /// <summary>
        /// Hàm tìm kiếm tuyến tính ( sắp xếp tăng dần )
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int IncreaseLinearSearch(int[] arr, int key)
        {
            // xuat ket qua
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == key)
                    return i;
                if (arr[i] > key)
                {
                    break;
                }
            }
            return -1;
        }
        /// <summary>
        /// Hàm tìm kiếm tuyến tính ( sắp xếp giảm dần )
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int DecreaseLinearSearch(int[] arr, int key)
        {
            // xuat ket qua
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == key)
                    return i;
                if (arr[i] < key)
                {
                    break;
                }
            }
            return -1;
        }
        /// <summary>
        /// Hàm Tìm kiếm nhị phân ( sắp xếp tăng dần ) 
        /// </summary>
        /// <param name="arrInt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static int IncreaseOrderBinarySearch(int[] arrInt, int key)
        {
            // xuat ket qua
            int left = 0;
            int right = arrInt.Length - 1;
            int mid;
            while (left <= right)
            {
                mid = (left + right) / 2;
                if (arrInt[mid] == key)
                    return mid;
                else if (arrInt[mid] < key)
                    left = mid + 1;
                else right = mid - 1;
            }
            return -1;
        }
        /// <summary>
        /// Hàm Tìm kiếm nhị phân ( sắp xếp giảm dần )
        /// </summary>
        /// <param name="arrInt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static int DecreaseOrderBinarySearch(int[] arrInt, int key)
        {
            // xuat ket qua
            int left = 0;
            int right = arrInt.Length - 1;
            int mid;
            while (left <= right)
            {
                mid = (left + right) / 2;
                if (arrInt[mid] == key)
                    return mid;
                else if (arrInt[mid] < key)
                    right = mid - 1;
                else left = mid + 1;
            }
            return -1;
        }
        public static int BinarySearch(int[] arrInt, int key)
        {
            // xuat ket qua
            int left = 0;
            int right = arrInt.Length - 1;
            int mid = -1;
            while (left <= right)
            {
                mid = (left + right) / 2;
                if (arrInt[mid] == key)
                    return mid;
                else if (arrInt[mid] < key)
                    left = mid + 1;
                else right = mid - 1;
            }
            return -1;
        }
    }
