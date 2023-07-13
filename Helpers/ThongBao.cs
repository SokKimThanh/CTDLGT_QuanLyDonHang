/*
 * Phần thông báo được tổng hợp để tránh lặp đi lặp lại nhiều lần không cần thiết.
 */
using System;
using static System.Console;
/**
 * Sok Kim Thanh
 * Modified: 01/06/2023 6:39 CH
 */
/// <summary>
/// Lớp tĩnh thông báo. Gửi Thông điệp menu khi nhấn key trên bàn phím
/// </summary>
public static class ThongBao
{
    // khai bao
    private const int alignLeft = -15;// canh le trai
    private const int center = -30;// canh giua



    public static string pressKeyToBye = "Good bye!";
    public static string pressKeyToContinue = "Press any key to continue.";
    public static string pressKeyToExit = "Press any key to exit.";
    public static string pressKeyToEnterCommandOrExitEN = "Please enter your command or '-1' to exit or back: ";
    public static string pressKeyToEnterCommandOrExitVI = $"Vui long nhap command hoac '-1' de quay ve hoac ket thuc chuong trinh: ";
    public static string pressKeyToBeContinue = "To be continue.";
    public static string done = "Done.";

    public static int AlignLeft => alignLeft;

    public static int Center => center;

    internal static void PrintRequestMenu(string[] arrMenu, int stt)
    {
        WriteLine(arrMenu[stt]);
    }

    /// <summary>
    /// Canh lề trái đoạn văn thông báo quan trọng xuống hàng
    /// </summary>
    /// <param name="paragraph">Đoạn văn</param>
    public static void p_alert_writeline(string paragraph)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{"",alignLeft}{paragraph}");
    }
    /// <summary>
    /// Canh lề trái đoạn văn thông báo quan trọng xuống hàng
    /// </summary>
    /// <param name="paragraph">Đoạn văn</param>
    public static void p_success_write(string paragraph)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{"",alignLeft}{paragraph}");
    }

    /// <summary>
    /// Canh lề trái đoạn văn thông báo quan trọng xuống hàng
    /// </summary>
    /// <param name="paragraph">Đoạn văn</param>
    public static void p_success_writeline(string paragraph)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{"",alignLeft}{paragraph}");
    }

    /// <summary>
    /// Canh lề trái đoạn văn thông báo quan trọng không xuống hàng 
    /// </summary>
    /// <param name="paragraph">Đoạn văn</param>
    public static void p_alert_write(string paragraph)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{"",alignLeft}{paragraph}");
    }
    /// <summary>
    /// Canh lề giữa đường gạch ngang
    /// </summary> 
    public static void hr()
    {
        Console.WriteLine($"{"",alignLeft}{"-----------------------------------------------------------"}{"",alignLeft}");
    }
    /// <summary>
    /// Canh lề giữa tiêu đề lớn
    /// </summary>
    /// <param name="title"></param>
    public static void h1_center_1(string title)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{"",alignLeft}{"***********************************************************"}");
        Console.Write($"{"",alignLeft}{"*"}");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{"",alignLeft}{title.ToUpper()}{"",alignLeft}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{"*"}");
        Console.WriteLine($"{"",alignLeft}{"***********************************************************"}");
    }
    /// <summary>
    /// Canh lề trái đoạn văn thông báo thoát xuống hàng
    /// </summary>
    /// <param name="paragraph">Đoạn văn</param>
    public static void p_exit()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"{"",alignLeft}{pressKeyToExit}");
        Console.ReadKey();
    }
    public static void WriteLine(string p)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{"",0}{p}");
        Console.ReadKey();
    }
}

