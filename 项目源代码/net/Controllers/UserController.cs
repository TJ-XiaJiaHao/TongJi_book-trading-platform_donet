using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using net.Controllers;
using net.Models;
using net.Controllers.ViewModel;
using System.IO;
using net.BusinessLayer;
using DLL.EmailService;
using DLL.UploadFile;
//using DLL.Verify; //原有的程序集
using System.Runtime.InteropServices;
using System.Text;
using CLRDLL;
using MSGBUSLib;

namespace net.Controllers
{
    public class UserController : Controller
    {
        //[DllImport("CppDLL.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //[return: MarshalAs(UnmanagedType.I1)]
        //public extern static bool IsEmail(byte[] email);

        private netHWEntities db = new netHWEntities();
        private Verify verify = new Verify();
        private MSGBUS msgBus = new MSGBUS();
        private bool isEamil(string email)
        {
            unsafe
            {
                byte[] bb = Encoding.Default.GetBytes(email);
                sbyte[] sbb = new sbyte[bb.Length];
                Buffer.BlockCopy(bb, 0, sbb, 0, bb.Length);
                fixed (sbyte* sb = sbb)
                {
                    return verify.IsEmail(sb);
                }
            }
        }
        private bool isHandset(string phone)
        {
            unsafe
            {
                byte[] bb = Encoding.Default.GetBytes(phone);
                sbyte[] sbb = new sbyte[bb.Length];
                Buffer.BlockCopy(bb, 0, sbb, 0, bb.Length);
                fixed (sbyte* sb = sbb)
                {
                    return verify.IsHandset(sb);
                }
            }

        }
        // GET: User
        [loginFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginByEmail(FormCollection fc)
        {
            string email = fc["login[username]"] as string;
            if (email == null) return Content("email is null");
            string a = System.Environment.CurrentDirectory;
            //bool isemail = IsEmail(Encoding.ASCII.GetBytes("15068206281"));
            //if (!verify.IsEmail(email))
            if(!isEamil(email))
            {
                ViewData["error"] =  msgBus.getMsg(10001);//"邮箱格式不正确！";
                return View("Login");
            }
            string password = fc["login[password]"] as string;
            if (password == null) return Content("password is null");

            if (AccountService.emailLogin(email, password, db))
            {
                string role = (string)Session["userRole"];
                if (role == "Admin") return Redirect("../Admin/Index");
                else return Redirect("../Product/Home");
            }

            ViewData["error"] = msgBus.getMsg(10002); //"invalidate name or password";
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            int account = Convert.ToInt32(fc["Account"]);
            string password = fc["Password"];

            if (AccountService.accountLogin(account, password, db))
            {
                if (Session["userRole"] != null && (string)Session["userRole"] == "Admin") return Redirect("../Admin/adminIndex");
                else return Redirect("../Product/Home");
            }
            else return View();
        }

        public ActionResult reSend()
        {
            if (AccountService.varifyEmail()) return View("Verify");
            else return Redirect("Regist");
        }

        public ActionResult Regist()
        {
            return View();
        }

        public ActionResult onRegist(RegisterForm form)
        {
            Console.WriteLine(ModelState.IsValid);
            if (ModelState.IsValid)
            {
                users user = new users();
                user.nickname = form.nickName;
                user.email = form.email;
                user.tele_phone = form.phone;
                string password = form.password;
                if (!isHandset(form.phone))
                {
                    ViewData["error"] = msgBus.getMsg(10003);// "phone number is not right";
                    return View("Regist");
                }
                //if (!verify.IsEmail(form.email))
                if(!isEamil(form.email))
                {
                    ViewData["error"] = msgBus.getMsg(10004); //"email is not right";
                    return View("Regist");
                }

                if (AccountService.regist_verify(user, password, db)) return View("Verify");
                else
                {
                    ViewData["error"] = msgBus.getMsg(10005); //"name or email repeated";
                    return View("Regist");
                }
            }
            else
            {
                ViewData["error"] = msgBus.getMsg(10006); //"input invalidate";
                return View("Regist");
            }
        }

        public ActionResult Verify(string code)
        {
            AccountService.regist_toDb(code, db);
            return Redirect("../Product/Home");
        }

        [loginFilter]
        public ActionResult Logoff()
        {
            AccountService.logoff();
            return Redirect("Login");
        }
        public ActionResult LoginNew()
        {
            return View();
        }
        [loginFilter]
        public ActionResult dashboard()
        {
            int account = Convert.ToInt32(Session["account"]);
            var users = from u in db.users
                        where u.account == account
                        select u;
            users user = users.First();
            if (user != null)
            {
                ViewData["nickName"] = user.nickname;
                ViewData["email"] = user.email;
                ViewData["picture"] = user.picture_url;
                if (user.picture_url == null) ViewData["picture"] = "/images/UserPhoto/0.jpeg";

                if (user.rece_id != null)
                {
                    var reces = from r in db.receiving
                                where r.rece_id == user.rece_id
                                select r;
                    receiving rece = reces.First();
                    ViewData["receName"] = rece.name;
                    ViewData["receAdd"] = rece.address;
                    ViewData["recePhone"] = rece.tele_number;
                }
            }
            return View();
        }
        [loginFilter]
        public ActionResult change_Info()
        {
            int account = Convert.ToInt32(Session["account"]);
            var users = from u in db.users
                        where u.account == account
                        select u;
            users user = users.First();
            if (user != null)
            {
                ViewData["nickName"] = user.nickname;
                ViewData["email"] = user.email;
                ViewData["phone"] = user.tele_phone;
                ViewData["sex"] = user.sex;
                if (user.sex == null) ViewData["sex"] = 0;
            }
            return View();
        }

        public ActionResult comment()
        {
            string name = Request.Params["name"];
            string email = Request.Params["email"];
            string phone = Request.Params["telephone"];
            string comment = Request.Params["comment"];
            string filename = Server.MapPath("~/BusinessLayer/CommentEmail.txt");
            string messBody = System.IO.File.ReadAllText(filename);

            messBody = messBody.Replace("@name@", name);
            messBody = messBody.Replace("@email@", email);
            messBody = messBody.Replace("@phone@", phone);
            messBody = messBody.Replace("@comment@", comment);

            EmailService.sendEmail(messBody, "comment", "mailforcode@163.com", "mailforcode@163.com");
            return Redirect("~/product/home");
        }

        [loginFilter]
        public ActionResult on_change_Info(UserForm uf)
        {
            if (!ModelState.IsValid)
            {
                return View("change_Info");
            }

            int account = Convert.ToInt32(Session["account"]);
            var users = from u in db.users
                        where u.account == account
                        select u;
            users user = users.First();

            user.nickname = uf.name;
            user.email = uf.email;
            user.tele_phone = uf.phone;
            switch (Convert.ToInt32(uf.sex))
            {
                //male
                case 1:
                    user.sex = "1";
                    break;
                //female
                case 2:
                    user.sex = "2";
                    break;
                //else
                default:
                    user.sex = "0";
                    break;
            }

            byte[] picDate = null;
            // && fc["picture"] != null
            if (Request.Files.Count > 0)
            {
                string filename = "/images/UserPhoto/" + account + ".jpeg";
                user.picture_url = filename;
                UploadFile upload = new UploadFile();
                upload.upload(Request.Files["picture"], filename);
                //HttpPostedFileBase picture = Request.Files["picture"];
                //using (var binary = new BinaryReader(picture.InputStream))
                //{
                //    picDate = binary.ReadBytes(picture.ContentLength);
                //}
            }
            //if (picDate != null && picDate.Length != 0)
            //{
            //    string filename = "/images/UserPhoto/" + account + ".jpeg";
            //    user.picture_url = filename;
            //    AccountService.uploadPhoto(picDate, filename);
            //}
            db.SaveChanges();

            return Redirect("dashboard");
        }

        [loginFilter]
        public ActionResult receiving()
        {
            int account = Convert.ToInt32(Session["account"]);
            var users = from u in db.users
                        where u.account == account
                        select u;
            users user = users.First();
            if (user.rece_id != null)
            {
                var reces = from r in db.receiving
                            where r.rece_id == user.rece_id
                            select r;
                receiving rece = reces.First();
                if (rece == null) return View();

                ViewData["name"] = rece.name;
                ViewData["phone"] = rece.tele_number;
                ViewData["province"] = rece.province;
                ViewData["city"] = rece.city;
                ViewData["street"] = rece.street;
                ViewData["address"] = rece.address;
            }

            return View();
        }

        [loginFilter]
        public ActionResult change_receiving(ReceivingForm rf)
        {
            if (!ModelState.IsValid)
            {
                return View("receiving");
            }
            int account = Convert.ToInt32(Session["account"]);
            var users = from u in db.users
                        where u.account == account
                        select u;
            users user = users.First();
            bool IsExist = false;
            receiving rece = null;
            if (user.rece_id != null)
            {
                var reces = from r in db.receiving
                            where r.rece_id == user.rece_id
                            select r;
                rece = reces.First();
                IsExist = true;
            }
            else
            {
                rece = new receiving();
                IsExist = false;
            }

            rece.name = rf.name;
            rece.province = rf.province;
            rece.city = rf.city;
            rece.street = rf.street;
            rece.tele_number = int.Parse(rf.phone);
            rece.address = rf.address;

            if (!IsExist)
            {
                db.receiving.Add(rece);
            }
            db.SaveChanges();

            //rece.rece_id插入数据库后自动修改
            user.rece_id = rece.rece_id;
            db.SaveChanges();

            return Redirect("dashboard");
        }

        public ActionResult show(int id)
        {
            var users = from u in db.users
                        where u.account == id
                        select u;
            users user = users.First();
            if (user != null)
            {
                ViewData["nickName"] = user.nickname;
                ViewData["email"] = user.email;
                ViewData["picture"] = user.picture_url;
                if (user.picture_url == null) ViewData["picture"] = "UserPhoto/0.jpeg";
                ViewData["phone"] = user.tele_phone;
            }
            return View();
        }

        public ActionResult aboutUs()
        {
            return View();
        }
    }
}