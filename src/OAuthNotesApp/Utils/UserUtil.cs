using OAuthNotesApp.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace OAuthNotesApp.Utils
{
    public class UserUtil
    {
        public static User Login(string username, string password)
        {
            using (var db = new NotesModel())
            {
                var user = db.User.SingleOrDefault(x => x.Username == username);
                if (user != null)
                {
                    var isVerified = CryptoUtil.VerifyPassword(password, user.Password);
                    if (isVerified)
                    {
                        return user;
                    }
                }
            }
            return null;
        }
        public static bool Register(string username, string password)
        {
            using (var db = new NotesModel())
            {
                var isExist = db.User.Any(x => x.Username == username);
                if (!isExist)
                {
                    var user = new User();
                    user.Username = username;
                    var hashedPassword = CryptoUtil.HashPassword(password);
                    user.Password = hashedPassword;
                    user.Role = "User";
                    db.User.Add(user);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}