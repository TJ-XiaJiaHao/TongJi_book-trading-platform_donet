using net.BusinessLayer;
using net.Models;
using net.SERVE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace net.Controllers
{
    public class ProductController : Controller
    {
        //创建商品实例
        Product product = new Product();

        //首页浏览界面
        public ActionResult Home()
        {
            var bookList = product.getBookInformation(10);
            return View(bookList);
        }

        //分类搜索查询书本界面
        [loginFilter]
        public ActionResult List(string type, string keyWord)
        {
            if (type == null) type = "";
            if (keyWord == null) keyWord = "";
            if (keyWord != "" && type != "")
            {
                var bookList = product.getBookInformation(5, type, keyWord);
                return View(bookList);
            }
            else if (keyWord != "")
            {
                var bookList = product.getBookInformationByKeyWord(5, keyWord);
                return View(bookList);
            }
            else if (type != "")
            {
                var bookList = product.getBookInformationByType(5, type);
                return View(bookList);
            }
            else
            {
                var bookList = product.getBookInformation(10);
                return View(bookList);
            }
        }
        [HttpPost]
        [loginFilter]
        public ActionResult List(FormCollection fc)
        {
            var keyWord = fc["search"];
            return Redirect("/Product/List?type=&keyWord=" + keyWord);
        }

        //书本详情界面
        [loginFilter]
        public ActionResult BookDetails(string book_id, string count = "")
        {
            int account = Convert.ToInt32(Session["account"]);
            //返回书本详细信息
            int book = Convert.ToInt32(book_id);
            ViewBag.book_id = book_id;
            var bookDetails = product.getBookDetails(book);
            bookDetails.similarBook = product.getBookInformationByKeyWord(4, bookDetails.book.book_name);
            foreach (var item in bookDetails.similarBook)
            {
                if (item.book.book_id == book)
                {
                    bookDetails.similarBook.Remove(item);
                    break;
                }
            }
            if (bookDetails.similarBook.Count() < 4)
            {
                var add = product.getSimilarBookInformation(4 - bookDetails.similarBook.Count(), bookDetails.book.book_name);
                foreach (var item in add)
                {
                    bookDetails.similarBook.Add(item);
                }
            }
            //加入购物车
            if (count != "")
            {
                int cnt = Convert.ToInt32(count);
                product.addBookToCart(account, book, cnt);
            }
            return View(bookDetails);
        }
        [HttpPost]
        [loginFilter]
        public ActionResult BookDetails(string book_id, FormCollection fc)
        {

            int account = Convert.ToInt32(Session["account"]);
            DateTime now = DateTime.Now;
            product.addBookComment(account, Convert.ToInt32(book_id), fc["detail"], now);
            return Redirect("/Product/BookDetails?book_id=" + book_id + "&count=");
        }


        //发布书本界面
        [loginFilter]
        public ActionResult AddNewBook()
        {
            return View();
        }
        [HttpPost]
        [loginFilter]
        public ActionResult AddNewBook(FormCollection fc)
        {
            string bookName = fc["billing[productname]"];
            int account = Convert.ToInt32(Session["account"]);
            book newBook = new book()
            {
                account = account,
                book_name = fc["billing[productname]"],
                author = fc["billing[authornamename]"],
                book_edition = fc["billing[publishinghouse]"],
                price = decimal.Parse(fc["billing[newprice]"]),
                count = Convert.ToInt32(fc["billing[count]"]),
                province = fc["billing[province]"],
                state = "出售中",
                introduction = fc["detailed introduction"]
            };

            //系统给新的书本编号和类型编号
            netHWEntities db = new netHWEntities();
            int id = 1;
            foreach (var tmp in db.book)
            {
                if (tmp.book_id >= id)
                {
                    id = tmp.book_id + 1;
                }
            }
            newBook.book_id = id;

            byte[] picDate = null;
            // && fc["picture"] != null
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase picture = Request.Files["picture"];
                using (var binary = new BinaryReader(picture.InputStream))
                {
                    picDate = binary.ReadBytes(picture.ContentLength);
                }
            }
            if (picDate != null && picDate.Length != 0)
            {
                string filename = "/images/BookPicture/" + newBook.book_id + ".jpeg";
                newBook.picture_url = filename;
                AccountService.uploadPhoto(picDate, filename);
            }
            else
            {
                newBook.picture_url = "~/images/BookPicture/product1.jpg";
            }

            bookDetail newBookDetail = new bookDetail()
            {
                book_name = fc["billing[productname]"],
                author = fc["billing[authornamename]"],
                book_edition = fc["billing[publishinghouse]"],
                price = decimal.Parse(fc["billing[oldprice]"]),
                pulisher = fc["billing[publisher]"],
                publish_date = DateTime.Now
            };
            bookType newBookType = new bookType()
            {
                type_name = fc["billing[type]"].ToString()
            };
            product.addNewBook(newBook, newBookDetail, newBookType);
            return View();
        }
    }
}