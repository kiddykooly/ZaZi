using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ZaZi.Models;

namespace Admin.Controllers
{


    public class AdminController : Controller
    {
        //
        // GET: 
        public ActionResult Index()
        {
            
            List<admin> lst = new List<admin>();
            for (int i = 0; i < ZaZi.MvcApplication.ProductList.Count; i++)
            {
                DateTime tst = DateTime.Now;
                DateTime t = ZaZi.MvcApplication.ProductList[i].EndDate;
                int result = DateTime.Compare(t, tst);
                if (ZaZi.MvcApplication.ProductList[i].clear != 0)
                {
                    continue;
                }
                if (result < 0)
                {
                    admin model = new admin();
                    model.Name = ZaZi.MvcApplication.ProductList[i].Name;
                    model.ProductId = ZaZi.MvcApplication.ProductList[i].Id;
                    model.Picture = ZaZi.MvcApplication.ProductList[i].pictures[0];
                    model.Time = t;
                    model.Price = ZaZi.MvcApplication.ProductList[i].CurrentPrice;
                    //model.WinnerId = ZaZi.MvcApplication.CustomerList.Find(x => x.Id == 
                    int custom = ZaZi.MvcApplication.BidList.Find(x => x.Id ==  ZaZi.MvcApplication.ProductList[i].Bids).CustomerId;
                    model.WinnerId = custom;
                    model.Winner = ZaZi.MvcApplication.CustomerList.Find(x => x.Id == custom).FullName;
                    model.Mail = ZaZi.MvcApplication.CustomerList.Find(x => x.Id == custom).Email;
                    lst.Add(model);
                }
                //if(ZaZi.MvcApplication.ProductList[i].EndDate - 
            }
            return View("AdminPage", lst);
        }

    }
}
