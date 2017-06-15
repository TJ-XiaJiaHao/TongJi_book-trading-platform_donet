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
using net.BusinessLayer;
namespace BookTrade.Controllers
{
    public class shoppingCartsController : Controller
    {
        private netHWEntities db = new netHWEntities();                                  //总体数据库的访问;
        public  SHOPPINGCARTBusiness business= new SHOPPINGCARTBusiness();      //逻辑业务层的访问
        [loginFilter]
        public ActionResult Index( )      //购物车首页;
        {
            int? userId = Convert.ToInt32( Session["account"]);
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.myShoppingCartId = userId;
            userId = business.GetCartId(userId);                                             //用户账号到购物车编号的转换;
            var BOOK_CART = from c in db.book_cart
                            where c.cart_id == userId 
                            select c ;
            ViewBag.seller_id = (from c in db.book_cart
                                 where c.cart_id == userId
                                 select c.book.account).Distinct();
            var list = BOOK_CART.ToList();
            foreach(var item in list)
            {
                item.book = item.book;
            }
            return View(list);
        }   

        public ActionResult Clear(int? cart_id,int?book_id)
        {
            if (cart_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            business.DeleteSHOPPINGCARTItem(book_id, cart_id);
            int? userId = Convert.ToInt32(db.shoppingCart.Find(cart_id).account);
            return Redirect("/shoppingCarts/Index");
        }

        [HttpPost]
        public ActionResult Edit(FormCollection fc)     //修改购物车单种书的数量;出现问题;
        {
            int count = int.Parse (fc["shoppingCartItemChange"]);
            int? cart_id = int.Parse(fc["cart_id"]);
            int book_id = int.Parse(fc["book_id"]);
            int? userId = Convert.ToInt32(db.shoppingCart.Find(cart_id).account);
            if (count >= 0)
            {
                business.ChangeSHOPPINGCARTItem(cart_id, book_id, count);
            }
            return Redirect("/shoppingCarts/Index");
        }

        [loginFilter]
        public ActionResult Order(int? buyer_id,int?seller_id)
        {
            business.DeleteOverdueBook(buyer_id);
            int cartId = business.GetCartId(buyer_id);
            business.CreateOrder(buyer_id,seller_id);           //创建用户订单;
            business.ClearShopping((int?)cartId,seller_id);                 //创建成功后清空购物车;
            var orders = from c in db.book_order
                         where c.bookOrder.buyer == buyer_id && c.book.account==seller_id &&c.state=="未生成"
                         select c;
            ViewBag.address = db.receiving.Find(db.users.Find(buyer_id).rece_id);
            return View(orders.ToList());
        }

        public ActionResult Cancel(int? orderId)        //取消订单;
        {
            int? userId = Convert.ToInt32( db.bookOrder.Find(orderId).buyer);
            business.AddChangeBOOKCOUNT(orderId);
            business.DeleteOrder(orderId);
            return Redirect("/shoppingCarts/Index");
        }


        public ActionResult address(FormCollection fc)      //设置用户的收货地址;
        {
            int? order_id =  int.Parse(fc["order_id"]);
            receiving newAddress = new receiving();
            newAddress.name = fc["name"];
            newAddress.tele_number = int.Parse(fc["telephone"]);
            newAddress.province = fc["province"];
            newAddress.city = fc["city"];
            newAddress.street = fc["street"];
            newAddress.address = fc["address"];
            db.receiving.Add(newAddress);
            db.SaveChanges();
            List<int> RECE_IDS = db.receiving.Select(c => c.rece_id).ToList();
            int RECE_ID = Convert.ToInt32(RECE_IDS.Max());
            business.SetOrderAddress( RECE_ID, order_id);
            business.ChangeOrderState(order_id, "未确认支付");
            int? userId = Convert.ToInt32(db.bookOrder.Find(order_id).buyer);
            return Redirect("/shoppingCarts/Index");
           
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
