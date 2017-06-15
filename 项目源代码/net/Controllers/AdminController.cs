using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using net.Models;
using net.Controllers.ViewModel;
using net.BusinessLayer;

namespace DataBaseCourseDesign.Controllers
{
    public class AdminController : Controller
    {
        private netHWEntities db = new netHWEntities();
        // GET: Admin
        [AdminFilter]
        public ActionResult Index()
        {
            return View();
        }

        [AdminFilter]
        public ActionResult adminIndex()
        {
            return View();
        }
        [AdminFilter]
        public JsonResult getUserList(string data)
        {
            var users = from u in db.users
                        select u;

            List<userInfo> ui = new List<userInfo>();
            foreach (users user in users)
            {
                userInfo tmp = new userInfo();
                tmp.pic = user.picture_url;
                if (tmp.pic == null) tmp.pic = "~/images/UserPhoto/0.jpeg";
                tmp.pic = "/" + tmp.pic;
                tmp.id = "" + user.account;
                tmp.name = user.nickname;

                ui.Add(tmp);
            }
            JsonResult json = new JsonResult()
            {
                Data = ui
            };

            return Json(json);
        }
        [AdminFilter]
        public ActionResult adminOrder()
        {
            return View();
        }
        [AdminFilter]
        public JsonResult getOrderList()
        {
            string match = Request.Params["match"];
            if (match == null) match = "";
            int pageSize = 10;
            int pageNum = Convert.ToInt32(Request.Params["Page"]);
            int begPos = 0;
            if (pageSize > 0 && pageNum > 0)
            {
                begPos = (pageNum - 1) * pageSize;
            }
            else return new JsonResult();

            var orders = db.bookOrder;
            List<OrderItem> items = new List<OrderItem>();
            foreach (var order in orders)
            {
                string tmp1 = order.order_id.ToString();
                string tmp2 = order.buyer.ToString();
                string tmp3 = order.seller.ToString();
                if (tmp1.Contains(match) || tmp2.Contains(match) || tmp3.Contains(match))
                {
                    OrderItem oi = new OrderItem();
                    oi.o_id = (int)order.order_id;
                    oi.c_id = (int)order.buyer;
                    oi.s_id = (int)order.seller;
                    oi.status = order.state;
                    items.Add(oi);
                }
            }
            int pageCnt = items.Count();
            pageCnt = pageCnt % pageSize == 0 ? (pageCnt / pageSize) : pageCnt / pageSize + 1;
            List<OrderItem> orderList = new List<OrderItem>();
            if (begPos < items.Count())
            {
                for (int i = 0; i < pageSize; i++)
                {
                    if ((begPos + i) < items.Count()) { orderList.Add(items[begPos + i]); }
                    else break;
                }
            }

            OrderJson json = new OrderJson()
            {
                Data = orderList,
                curPage = pageNum,
                pageCnt = pageCnt,
                key = match,
            };
            return Json(json);
        }

        [AdminFilter]
        public ActionResult adminBook()
        {
            return View();
        }
    }
}