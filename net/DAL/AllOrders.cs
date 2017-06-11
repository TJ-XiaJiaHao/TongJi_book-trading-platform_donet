using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using net.Models;
namespace net.DAL
{
    public class AllOrders
    {
        public List<book_order> buyer_order;
        public List<book_order> seller_order;
    }
}