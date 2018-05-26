using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace OAuthNotesApp.Utils
{
    public class CryptoUtil
    {
        public static string HashPassword(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPassword;
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var isVerified = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return isVerified;
        }
    }
}