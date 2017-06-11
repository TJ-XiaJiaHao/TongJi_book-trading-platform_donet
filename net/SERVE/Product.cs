using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using net.Models;
using net.DAL;
namespace net.SERVE
{
    public class Product
    {
        private netHWEntities db = new netHWEntities();
        
        //获取N个随机商品信息 ok
        public List<BookInformation> getBookInformation(int n)
        {
            var bookInfoList = new List<BookInformation>();
            var bookList = db.book.Where(a => a.state != "已删除").Take(n).ToList();
            Console.WriteLine(bookList);
            foreach (var item in bookList)
            {
                //    db.Dispose();
                //    db = new netHWEntities();
                var bookInfo = new BookInformation()
                {
                    book = item,
                    bookDetails = db.bookDetail.FirstOrDefault(a => a.author == item.author & a.book_name == item.book_name & a.book_edition == item.book_edition)
                    //(from a in db.bookDetail
                    //where a.author == item.author && a.book_name == item.book_name && a.book_edition == item.book_edition
                    //select a).First(),
            };
                bookInfoList.Add(bookInfo);
            }
            return bookInfoList;
        }

        //根据关键字和类型获取指定数量信息 ok
        public List<BookInformation> getBookInformation(int n, string type, string keyWord)
        {
            var bookInfoList = new List<BookInformation>();
            var bl = db.bookType.Where(a => a.type_name == type);
            if (bl.Count() == 0) return null;
            var typeID = bl.First().type_id;
            var bookList = db.book.Where(a => a.type_id == typeID & a.book_name.Contains(keyWord)).Take(n).ToList();
            foreach (var item in bookList)
            {
                var bookInfo = new BookInformation()
                {
                    book = item,
                    bookDetails = db.bookDetail.Where(a => a.author == item.author & a.book_name == item.book_name & a.book_edition == item.book_edition).First()
                };
                bookInfoList.Add(bookInfo);
            }
            return bookInfoList;
        }
        public List<BookInformation> getBookInformationByType(int n, string type)
        {
            var bookInfoList = new List<BookInformation>();
            var bl = db.bookType.Where(a => a.type_name == type);
            if (bl.Count() == 0) return bookInfoList;
            var typeID = bl.First().type_id;
            var bookList = db.book.Where(a => a.type_id == typeID).Take(n).ToList();
            foreach (var item in bookList)
            {
                var bookInfo = new BookInformation()
                {
                    book = item,
                    bookDetails = db.bookDetail.Where(a => a.author == item.author & a.book_name == item.book_name & a.book_edition == item.book_edition).First()
                };
                bookInfoList.Add(bookInfo);
            }
            return bookInfoList;
        }
        public List<BookInformation> getBookInformationByKeyWord(int n, string keyWord)
        {
            var bookInfoList = new List<BookInformation>();
            var bookList = db.book.Where(a => a.book_name.Contains(keyWord)).Take(n).ToList();
            foreach (var item in bookList)
            {
                var bookInfo = new BookInformation()
                {
                    book = item,
                    bookDetails = db.bookDetail.Where(a => a.author == item.author & a.book_name == item.book_name & a.book_edition == item.book_edition).First()
                };
                bookInfoList.Add(bookInfo);
            }
            return bookInfoList;
        }
        public List<BookInformation> getSimilarBookInformation(int n, string bookName)
        {
            var bookInfoList = new List<BookInformation>();
            var bookList = db.book.Where(a => a.book_name != bookName).Take(n).ToList();
            foreach (var item in bookList)
            {
                var bookInfo = new BookInformation()
                {
                    book = item,
                    bookDetails = db.bookDetail.Where(a => a.author == item.author & a.book_name == item.book_name & a.book_edition == item.book_edition).First()
                };
                bookInfoList.Add(bookInfo);
            }
            return bookInfoList;
        }

        //获取某个商品的拓展信息 ok
        public BookDetails getBookDetails(int book_id)
        {
            var bookList = db.book.Where(a => a.book_id == book_id).First();
            var bookInfo = new BookDetails()
            {
                book = bookList,
                bookDetails = getBookDetails(bookList.book_name, bookList.author, bookList.book_edition)
            };
            var commentList = new List<Comment>();
            var comment = db.bookComment.Where(a => a.book_id == book_id).ToList();
            foreach (var item in comment)
            {
                var oneComment = new Comment()
                {
                    buyer = db.users.Where(a => a.account == item.account).First(),
                    comment = item
                };
                commentList.Add(oneComment);
            }
            bookInfo.CommentList = commentList;

            var sler = db.users.Where(a => a.account == bookList.account).First();
            bookInfo.seller = sler;
            return bookInfo;
        }
        public bookDetail getBookDetails(string bookName, string author, string bookEdition)
        {
            var bookDetail = db.bookDetail.Where(a => a.book_name == bookName & a.author == author & a.book_edition == bookEdition).First();
            return bookDetail;
        }

        //添加书本至购物车
        public bool addBookToCart(int account, int book_id, int count)
        {
            var user = db.users.Where(a => a.account == account);
            if (user.Count() == 0) return false;

            var shoopingCart = db.shoppingCart;
            var cart = db.shoppingCart.Where(a => a.account == account);
            var book = db.book.Where(a => a.book_id == book_id);
            //检测购物车是否存在
            if (cart.Count() == 0)
            {
                shoppingCart newCart = new shoppingCart()
                {
                    account = account,
                    cart_id = account,
                    total_price = 0,
                    total_quantity = 0
                };
                shoopingCart.Add(newCart);
                db.SaveChanges();
                cart = db.shoppingCart.Where(a => a.account == account);
                var c = cart.Count();
            }
            //修改购物车属性 
            //cart.First().TOTAL_QUANTITY += count;
            //cart.First().TOTAL_PRICE += book.First().PRICE * count;
            //修改购物车和书本联系集属性
            var bookCart = db.book_cart.Where(a => a.book_id == book_id & a.cart_id == cart.FirstOrDefault().cart_id);
            var tmp1 = bookCart.Count();
            if (bookCart.Count() == 0)
            {
                db.book_cart.Add(new book_cart() { book_id = book_id, cart_id = cart.First().cart_id, count = count });
            }
            else
            {
                bookCart.First().count += count;
            }
            db.SaveChanges();
            return true;
        }

        //减少商品数量 ok
        public bool reduceBookCount(int book_id, int number)
        {
            var book = db.book.Where(a => a.book_id == book_id);
            if (book.Count() > 0 && book.First().count >= number)
            {
                book.First().count -= number; var bookDetaile = db.bookDetail.Where(a => a.book_name == book.First().book_name & a.author == book.First().author & a.book_edition == book.First().book_edition);
                if (bookDetaile.First().count > number)
                {
                    bookDetaile.First().count -= number;
                }
                else
                {
                    bookDetaile.First().count = 0;
                }
                db.SaveChanges();
                return true;
            }
            else return false;
        }

        //增加商品数量 ok
        public bool addBookCount(int book_id, int number)
        {
            var book = db.book.Where(a => a.book_id == book_id);
            if (book.Count() > 0)
            {
                book.First().count += number;
                var bookDetaile = db.bookDetail.Where(a => a.book_name == book.First().book_name & a.author == book.First().author & a.book_edition == book.First().book_edition);
                bookDetaile.First().count += number;
                db.SaveChanges();
                return true;
            }
            else return false;
        }

        //修改商品价格
        public bool changBookPrice(int book_id, int newPrice)
        {
            var book = db.book.Where(a => a.book_id == book_id);
            if (book.Count() > 0 && newPrice >= 0)
            {
                book.First().price = newPrice;
                db.SaveChanges();
                return true;
            }
            else return false;
        }

        //修改商品简介
        public bool changeBookIntroduction(int book_id, string newIntro)
        {
            var book = db.book.Where(a => a.book_id == book_id);
            if (book.Count() > 0)
            {
                book.First().introduction = newIntro;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //新增书本信息 ok
        public bool addNewBook(book book, bookDetail bookDetail, bookType bookType)
        {
            //检测卖书账户是否存在
            if (db.users.Where(a => a.account == book.account).Count() == 0) return false;
            book.type_id = db.bookType.Where(a => a.type_name == bookType.type_name).First().type_id;
            //把书本存入数据库中
            db.book.Add(book);
            //检测书本详细信息是否存在
            var oldBookDetail = db.bookDetail.Where(a => a.book_name == bookDetail.book_name & a.author == bookDetail.author & a.book_edition == bookDetail.book_edition);
            if (oldBookDetail.Count() == 0)
            {
                bookDetail.count = book.count;
                db.bookDetail.Add(bookDetail);
            }
            else
            {
                oldBookDetail.First().count += book.count;
            }
            db.SaveChanges();
            return true;
        }

        //删除商品 ok
        public bool removeBook(int book_id)
        {
            var book = db.book.Where(a => a.book_id == book_id);
            if (book.Count() > 0)
            {
                book.First().state = "已删除";
                var bookDetaile = db.bookDetail.Where(a => a.book_name == book.First().book_name & a.author == book.First().author & a.book_edition == book.First().book_edition);
                if (bookDetaile.First().count > book.First().count)
                {
                    bookDetaile.First().count -= book.First().count;
                }
                else
                {
                    bookDetaile.First().count = 0;
                }
                book.First().count = 0;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //添加书本评论
        public bool addBookComment(int account, int book_id, string comment, DateTime date)
        {
            //查询购物表看用户是否购买过该本书
            var orderList = from c in db.bookOrder
                            from a in db.book_order
                            where c.buyer == account && a.book_id == book_id && c.state == "已完成" && a.order_id == c.order_id
                            select a;
            if (orderList.Count() == 0) return false;
            //系统给评论编号
            int i = 1;
            bool find = false;
            while (!find)
            {
                find = true;
                foreach (var tmp in db.book)
                {
                    if (tmp.book_id == i)
                    {
                        i++;
                        find = false;
                        break;
                    }
                }

            }
            int comment_id = i;
            var bookComment = db.bookComment.Where(a => a.comment_id == comment_id);
            var accountList = db.users.Where(a => a.account == account);
            var bookList = db.book.Where(a => a.book_id == book_id);
            if (bookComment.Count() > 0 || accountList.Count() == 0 || bookList.Count() == 0)
            {
                return false;
            }
            else
            {
                int id = 1;
                foreach (var tmp in db.bookComment)
                {
                    if (tmp.comment_id != id) break;
                    else id++;
                }
                bookComment newComment = new bookComment()
                {
                    comment_id = id,
                    account = account,
                    book_id = book_id,
                    comment_date = date,
                    content = comment
                };
                db.bookComment.Add(newComment);
                db.SaveChanges();
                return true;
            }
        }
    }
}