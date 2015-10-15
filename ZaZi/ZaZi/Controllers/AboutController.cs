using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Web;
using System.Web.Mvc;
using ZaZi.Models;

namespace ZaZi.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult About()
        {
            return View();
        }
    }
}