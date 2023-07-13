using System;

using static System.Console;
/**
 * Sok Kim Thanh
 * Date 30/05/2023
 * đóng gói lớp tiện ích
 */
public static class Ultilities
{

    public static void XuatLine(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Write($"{arr[i]} ");
        }
        Console.WriteLine();
    }
    public static string XuatString(int[] arr)
    {
        string sum = string.Empty;
        for (int i = 0; i < arr.Length; i++)
        {
            sum += $"{arr[i]}\t";
        }
        return sum.Trim();
    }
    public static string XuatTitle(string[] arr)
    {
        string sum = string.Empty;
        for (int i = 0; i < arr.Length; i++)
        {
            sum += $"{arr[i],-15}";
        }
        return sum.Trim();
    }
    public static int NhapSoNguyen()
    {
        int n;
        do
        {
            Console.ForegroundColor = ConsoleColor.White;
        } while (!int.TryParse(Console.ReadLine(), out n) || n <= 0);
        return n;
    }
    public static string NhapChuoi()
    {
        string s;
        do
        {
            Console.ForegroundColor = ConsoleColor.White;
            s = Console.ReadLine();
        } while (s == null);
        return s;
    }


    /// <summary>
    /// Hàm nhập mảng số nguyên random
    /// </summary>
    /// <param name="n"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public static int[] NhapRandomMangSoNguyen(int n)
    {
        Random d = new Random();
        int[] arr = new int[n];
        // nhap ran dom thong tin
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = d.Next(0, 20);
        }
        return arr;
    }

    public static void Swap(ref int v1, ref int v2)
    {
        int tmp = v1;
        v1 = v2;
        v2 = tmp;
    }
    /// <summary>
    /// Hàm tìm vị trí nhỏ nhất posMin
    /// </summary>
    /// <param name="start">vi tri bat dau tim min</param>
    /// <param name="arr">danh sach tim min</param>
    /// <returns>posMin</returns>
    public static int FindPosMin(int[] arr, int start)
    {
        // kiem tra tinh hop le vi tri bat dau
        if (start < 0 && start > arr.Length)
        {
            return -1;// khong tim thay min
        }
        // khai bao 
        int minValue;// tim min
        int posMin = start;// cap nhat lai vi tri can tim min
        minValue = arr[start];
        for (int i = start + 1; i < arr.Length; i++)
        {
            // cap nhat min, pos neu thoa dieu kien
            if (arr[i] < minValue)
            {
                minValue = arr[i];// luu gia tri min de tiep tuc dieu kien
                posMin = i;// luu vi tri min de tiep tuc dieu kien
            }
        }
        return posMin;
    }
}

