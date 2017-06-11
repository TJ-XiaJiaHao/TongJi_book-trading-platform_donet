using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using net.Models;
using net.SERVE;
using net.DAL;
using net.BusinessLayer;

namespace DataBaseCourseDesign.Controllers
{
    public class orderController : Controller
    {
        private netHWEntities db = new netHWEntities();                                  //总体数据库的访问;
        private SHOPPINGCARTBusiness business = new SHOPPINGCARTBusiness();      //逻辑业务层的访问

        [loginFilter]
        public ActionResult myorder()
        {
            int? user_id = Convert.ToInt32(Session["account"]);
            AllOrders allOrder = new AllOrders();
            allOrder.buyer_order = (from c in db.bookOrder
                                    from a in db.book_order
                                    where c.buyer == user_id && a.order_id == c.order_id
                                    select a).ToList();
            allOrder.seller_order = (from c in db.bookOrder
                                 from a in db.book_order
                                 where c.seller == user_id && a.order_id == c.order_id
                                 select a).ToList();
            return View(allOrder);
        }
        [loginFilter]
        public ActionResult sold_goods( )
        {
            int? user_id = Convert.ToInt32(Session["account"]);
            var items = from c in db.book
                        where c.account == user_id
                        select c;
            return View(items.ToList());
        }

        public ActionResult ConfirmReceiving(int? order_id)
        {
            business.ChangeOrderState(order_id, "已完成");
            return RedirectToAction("myorder");
        }

        public ActionResult ApplyRefond(int? order_id)
        {
            business.ChangeOrderState(order_id, "退款中");
            return RedirectToAction("myorder");
        }

        public ActionResult ConfirmPay(int? order_id)
        {
            business.ChangeOrderState(order_id, "等待收货");
            return RedirectToAction("myorder");
        }

        public ActionResult AgreeRefond(int? order_id)
        {
            business.ChangeOrderState(order_id, "已退款");
            return RedirectToAction("myorder");
        }
        [HttpPost]
        public ActionResult ChangeBookLastCount(FormCollection fc)
        {
            int book_id =int.Parse(fc["book_id"]);
            int count = int.Parse(fc["count"]);
            decimal price = decimal.Parse(fc["price"]);
            var items = from c in db.book
                        where c.book_id== book_id
                        select c;
            foreach (var item in items)
            {
                item.count = count;
                item.price = price;
            }
            int user_id = Convert.ToInt32( db.book.Find(book_id).account);
            db.SaveChanges();
            return Redirect("/order/sold_goods");
        }
    }
}