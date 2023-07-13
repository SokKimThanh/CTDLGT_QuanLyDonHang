using BT_TONGHOP_SokKimThanh;
using System;
using System.IO;
internal static class Connection
{

    //Methods
    public static bool getConnection(TaiKhoan taikhoan, string filePath)
    {
        try
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    string[] t = line.Split('#');
                    if (taikhoan.UserName == t[0])
                    {
                        if (taikhoan.Password == t[1])
                        {
                            return true;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }
}

