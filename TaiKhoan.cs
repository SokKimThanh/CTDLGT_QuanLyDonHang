using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * Sok Kim Thanh
 * Date 19/05/2023 4:55 SA
 */
namespace BT_TONGHOP_SokKimThanh
{
    public class TaiKhoan
    {
        private string _UserName;
        private string _Password;
        private string _RePassword;

        public TaiKhoan()
        {
        }

        /// <summary>
        /// Đang nhập
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public TaiKhoan(string userName, string password)
        {
            _UserName = userName;
            _Password = password;
        }

        /// <summary>
        /// Đang ký
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="rePassword"></param>
        public TaiKhoan(string userName, string password, string rePassword)
        {
            _UserName = userName;
            _Password = password;
            _RePassword = rePassword;
        }

        public string UserName { get => _UserName; set => _UserName = value; }
        public string Password { get => _Password; set => _Password = value; }
        public string RePassword { get => _RePassword; set => _RePassword = value; }

        public bool KiemTraDangKy()
        {
            if (Password.ToLower().CompareTo(RePassword.ToLower()) == 0)
            {
                return true;
            }
            return false;
        }
        public string InRaFile()
        {
            return $"{UserName}#{Password}";
        }
        public string toString()
        {
            return $"username: {UserName} password: {Password}";
        }

    }
}
