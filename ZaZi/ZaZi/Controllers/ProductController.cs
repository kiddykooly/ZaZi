using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZaZi.Models;
using System.Xml;
using System.Globalization;

namespace ZaZi.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index(int id)
        {
            var product = ZaZi.MvcApplication.ProductList.Find(x => x.Id == id);
            if (product != null)
            {
                return View(product);
            }
            else
            {
                int max = ZaZi.MvcApplication.ProductList.Count;
                product = ZaZi.MvcApplication.ProductList[max - 1];
                return View(product);
            }
            
        }
    }
}
