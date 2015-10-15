using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZaZi.Models;

namespace ZaZi.Controllers
{
    public class CustomerController : Controller
    {

        // Lúc đầu để check đặt tên không trùng nhưng không hiệu quả nên không xài nữa
        [HttpPost]
        public ActionResult ValidateName(string Name, string Email)
        {
           CustomerModel email = ZaZi.MvcApplication.CustomerList.Find(x => String.Equals(x.Email, Email));
           if(email != null) {
               return Json(true);
           } else {
               CustomerModel name = ZaZi.MvcApplication.CustomerList.Find(x => String.Equals(x.FullName, Name));
               return Json(name == null);
           }
        }

    }
}
