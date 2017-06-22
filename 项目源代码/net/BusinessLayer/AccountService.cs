using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using net.Models;
using System.Web.SessionState;
using System.IO;
using DLL.EmailService;
using DLL.RandomCode;
using DLL.EncryptAndDecrypt;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace net.BusinessLayer
{
    public class AccountService
    {

        private AccountService() { }
        public static bool regist_verify(users user, string password, netHWEntities db)
        {
            if (user.nickname == null || user.email == null) return false;

            var users = from u in db.users
                        where u.nickname == user.nickname || u.email == user.email
                        select u;

            if (users.Count() > 0) return false;

            EncryptAndDecrypt crypt = new EncryptAndDecrypt();
            //SHA1 sha = new SHA1CryptoServiceProvider();
            //byte[] pwd = System.Text.Encoding.Default.GetBytes(password);

            //pwd = sha.ComputeHash(pwd);
            //user.password = System.Text.Encoding.Default.GetString(pwd);
            user.password = crypt.Encrypt(password);
            HttpContext.Current.Session.Contents["userInfo"] = user;
            varifyEmail();
            //ThreadPool.QueueUserWorkItem(sendEmail,"");
            return true;
        }
        private static void sendEmail(Object state)
        {
            varifyEmail();
        }
        public static bool varifyEmail()
        {
            if (HttpContext.Current.Session.Contents["userInfo"] == null) return false;

            string filename = HttpContext.Current.Server.MapPath("~/BusinessLayer/VerifyEmail.txt");
            string messBody = File.ReadAllText(filename);
            Random random = new Random();
            string code = "";

            //for (int i = 0; i < 6; i++) code += random.Next(0, 9);  //六位校验码
            RandomCode rand = new DLL.RandomCode.RandomCode();
            code = rand.getCharNumber(6);
            messBody = messBody.Replace("@USERNAME@", ((users)HttpContext.Current.Session.Contents["userInfo"]).nickname);
            messBody = messBody.Replace("@VERIFYCODE@", code);
            HttpContext.Current.Session.Contents["verifyCode"] = code;

            EmailService.sendEmail(messBody, "verifyCode", "mailforcode@163.com", ((users)HttpContext.Current.Session.Contents["userInfo"]).email);
            return true;
        }

        public static int regist_toDb(string code, netHWEntities db)
        {
            if (HttpContext.Current.Session.Contents["userInfo"] == null) return 0;

            users user = HttpContext.Current.Session.Contents["userInfo"] as users;
            string verifyCode = HttpContext.Current.Session.Contents["verifyCode"] as string;
            if (string.Compare(code, verifyCode) == 0)
            {
                db.users.Add(user);
                db.SaveChanges();
                var users = from u in db.users
                            where u.nickname == user.nickname && u.email == user.email
                            select u;

                int user_id = Convert.ToInt32(users.First().account);
                if (user.userRole.Count == 0)
                {
                    var role_id = from role in db.userRole
                                  where role.role_name == "Normal"
                                  select role;
                    users.First().userRole.Add(role_id.First());
                    users.First().account = user_id;
                    db.SaveChanges();
                }

                HttpSessionState session = HttpContext.Current.Session;
                session.Contents.Clear();
                session.Contents["userName"] = users.First().nickname;
                session.Contents["account"] = users.First().account;
                session.Contents["userRole"] = users.First().userRole.First().role_name;
                return user_id;
            }
            else return -1;
        }
        public static bool accountLogin(int account, string password, netHWEntities _db)
        {
            //SHA1 sha = new SHA1CryptoServiceProvider();
            //byte[] pwd = System.Text.Encoding.Default.GetBytes(password);

            //pwd = sha.ComputeHash(pwd);
            //password = System.Text.Encoding.Default.GetString(pwd);
            EncryptAndDecrypt crypt = new EncryptAndDecrypt();
            password = crypt.Encrypt(password);

            var users = from u in _db.users
                        where u.account == account && u.password == password
                        select u;
            if (users.Count() > 0)
            {
                HttpSessionState session = HttpContext.Current.Session;
                session.Contents["userName"] = users.First().nickname;
                session.Contents["account"] = users.First().account;
                session.Contents["userRole"] = ((userRole)users.First().userRole.First()).role_name;
                return true;
            }
            return false;
        }
        public static bool emailLogin(string email, string password, netHWEntities _db)
        {
            //SHA1 sha = new SHA1CryptoServiceProvider();
            //byte[] pwd = System.Text.Encoding.Default.GetBytes(password);

            //pwd = sha.ComputeHash(pwd);
            //password = System.Text.Encoding.Default.GetString(pwd);
            EncryptAndDecrypt crypt = new EncryptAndDecrypt();
            password = crypt.Encrypt(password);

            var users = from u in _db.users
                        where u.email == email && u.password == password
                        select u;
            if (users.Count() > 0)
            {
                HttpSessionState session = HttpContext.Current.Session;
                session.Contents["userName"] = users.First().nickname;
                session.Contents["account"] = users.First().account;
                session.Contents["userRole"] = ((userRole)users.First().userRole.First()).role_name;
                return true;
            }
            return false;
        }
        public static void logoff()
        {
            HttpSessionState session = HttpContext.Current.Session;
            session.Contents.Clear();

        }
        public static void modify(users user)
        {

        }
        //public static void uploadPhoto(Byte[] imgData, string filename)
        //{
        //    if (imgData != null)
        //    {
        //        string path = HttpContext.Current.Server.MapPath("~/" + filename);
        //        System.IO.File.WriteAllBytes(path, imgData);
        //    }
        //}
    }
}