using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using net.Models;

namespace net.SERVE
{
    public class SHOPPINGCARTBusiness
    {
        private netHWEntities db = new netHWEntities();

        public int GetCartId(int? id)       //由用户id获取到购物车id
        {
            List<int> CART_ID = (from item in db.shoppingCart
                                 where item.account == id
                                 select item.cart_id).ToList();
            if (CART_ID.Count == 0) 
            {
                shoppingCart newCart = new shoppingCart()
                {
                    account = id,
                    cart_id = (int)id,
                    total_price = 0,
                    total_quantity = 0
                };
                db.shoppingCart.Add(newCart);
                db.SaveChanges();
                return (int)id;
            }
            return Convert.ToInt32(CART_ID.First());
        }

        public void ClearShopping(int? id,int? seller_id)          //清空购物车;
        {
            var items = (from item in db.book_cart
                         where item.cart_id == id && item.book.account == seller_id
                         select item).ToList();
            foreach (var item in items)
            {
                db.book_cart.Remove(item);
            }
            //可由触发器实现功能;
            db.SaveChanges();

        }

        public void DeleteSHOPPINGCARTItem(int? BOOK_ID, int? CART_ID)
        {
            var items = (from c in db.book_cart
                        where c.cart_id == CART_ID && c.book_id == BOOK_ID
                        select c).ToList();
            foreach (var item in items)
            {
                db.book_cart.Remove(item);
            }
            //以下由触发器实现;

            db.SaveChanges();
        }

        public void ChangeSHOPPINGCARTItem(int? CART_ID, int? BOOK_ID, int NewCOUNT)   //修改购物车中各个商品的数量,传进来参数为CART_ID,BOOK_ID与修改后的数量
        {
            var items = (from item in db.book_cart
                        where item.book_id == BOOK_ID && item.cart_id == CART_ID
                        select item).ToList();
            foreach (var item in items)
            {
                if (NewCOUNT == 0)                      //如果修改后数量为零，则从数据库的BOOK_CART表删除这一个记录
                {
                    db.book_cart.Remove(item);
                }
                else
                {
                    item.count = NewCOUNT;
                }
            }
            db.SaveChanges();
            //BOOK_order中修改,SHOPPINGCART中跟着修改，可由触发器实现;

            decimal COUNT = 0;
            decimal price = 0;
            var iterms = (from item in db.book_cart
                          where item.cart_id == CART_ID
                          select item).ToList();

            foreach (var item in iterms)
            {
                COUNT += (decimal)item.count;
                price += (decimal)(item.book.price * item.count);
            }
            shoppingCart shopping_Cart = db.shoppingCart.Find(CART_ID);
            shopping_Cart.total_price = price;
            shopping_Cart.total_quantity = (int)COUNT;
            db.SaveChanges();
        }

        public void CreateOrder(int? buyer_id,int? seller_id)        //创建用户订单，修改BOOK_order与BOOKORDER两个表;
        {
            int i;
            if (db.bookOrder.Count() != 0)
            {
                List<int> orderIds = db.bookOrder.Select(c => c.order_id).ToList();
                i = Convert.ToInt32( orderIds.Max()) + 1;
            }
            else
            {
                i = 1000;
            }
            
            bookOrder newItem = new bookOrder();//不存在要插入的新项;
            newItem.order_id = i;
            newItem.buyer = buyer_id;
            newItem.state = "未生成";
            newItem.seller = seller_id;
            newItem.price = 0;
            if (db.users.Find(buyer_id) != null)
            {
                newItem.rece_id = db.users.Find(buyer_id).rece_id;
            }
            db.bookOrder.Add(newItem);
            db.SaveChanges();

            var items = (from c in db.book_cart
                         where c.book.account == seller_id && c.shoppingCart.account == buyer_id
                         select c).ToList();           //把一个用户要从同一个商家购买的所有记录提取出来
            foreach (var item in items)
            {
               int? COUNT = Convert.ToInt32( db.book.Find(item.book_id).count);
                if (item.count <= COUNT)
                {
                    db.SaveChanges();
                    newItem.price += item.count * item.book.price;
                    book_order newOrder = new book_order();
                    newOrder.order_id = newItem.order_id;
                    newOrder.state = "未生成";        //未生成订单;
                    newOrder.count = item.count;
                    newOrder.book_id = item.book_id;
                    db.book_order.Add(newOrder);
                }
            }
            db.SaveChanges();
            this.ChangeBOOKCOUNT(Convert.ToInt32( newItem.order_id));
        }

        public void DeleteOrder(int? id)        //删除订单;
        {
            var items = (from item in db.book_order
                        where item.order_id == id
                        select item).ToList();
            foreach (var item in items)
            {
                db.book_order.Remove(item);
            }
            //触发器实现;
            var orderItems = (from item in db.bookOrder
                             where item.order_id == id
                             select item).ToList();
            foreach (var orderItem in orderItems)
            {
                db.bookOrder.Remove(orderItem);
            }
            db.SaveChanges();
        }

        public void ChangeOrderState(int? order_id, string state)          //      根据用户的不同行为修改订单的状态;
        {
            var orderItems = (from c in db.bookOrder
                        where c.order_id == order_id
                        select c).ToList();
            foreach (var item in orderItems)
            {
                item.state = state;
            }

            var BOOK_ORDERItems = (from c in db.book_order
                                  where c.order_id == order_id
                                  select c).ToList();
            foreach (var bo_item in BOOK_ORDERItems)
            {
                bo_item.state = state;
            }
            db.SaveChanges();
        }

        public void SetOrderAddress(int? RECE_ID, int? order_id)            //设置订单记录中的收货人信息;
        {
            var items = (from c in db.bookOrder
                        where c.order_id == order_id
                        select c).ToList();
            foreach (var item in items)
            {
                item.rece_id = RECE_ID;
            }
            db.SaveChanges();

        }

        public void ChangeBOOKCOUNT(int? order_id)      //修改某个订单中所有涉及到的书本;
        {
            var items = (from c in db.book_order
                        where c.order_id == order_id
                        select c).ToList();
            foreach (var item in items)
            {
                var BOOK = db.book.Find(item.book_id);
                BOOK.count -= item.count;
            }
            db.SaveChanges();
        }

        public void AddChangeBOOKCOUNT(int? order_id)      //返还修改某个订单中所有涉及到的书本;
        {
            var items = (from c in db.book_order
                        where c.order_id == order_id
                        select c).ToList();
            foreach (var item in items)
            {
                var BOOK = db.book.Find(item.book_id);
                BOOK.count += item.count;
            }
            db.SaveChanges();
        }

        public void DeleteOverdueBook(int? buyer_id)
        {
            var items = from c in db.bookOrder
                        where c.buyer == buyer_id && c.state == "未生成"
                        select c.order_id;
            if (items.Count() > 0)
            {
                this.AddChangeBOOKCOUNT(Convert.ToInt32(items.First()));
                this.DeleteOrder(Convert.ToInt32(items.First()));
            }
           
        }
    }
}