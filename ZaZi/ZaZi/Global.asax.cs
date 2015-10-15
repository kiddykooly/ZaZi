using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Xml;
using ZaZi.Models;

namespace ZaZi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static List<ProductModel> ProductList = new List<ProductModel>();
        public static List<CustomerModel> CustomerList = new List<CustomerModel>();
        public static List<BidModel> BidList = new List<BidModel>();
        // 
        public static int IdOfThePlaceTurn;

        //
        public static int productId = 0;
        public static int passproductId = 0;

        public static int passWinerId;
        public static int nowWinerId;

        public static string WinnerName;
        //public static string name;
        public static int currentUser = 0;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            PrepareProductList();
            PrepareCustomerList();
            PrepareBidList();
        }
        private void PrepareProductList()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/App_Data/products.xml"));
            XmlNodeList products = xml.SelectNodes("/products/product");

            foreach (XmlNode item in products)
            {
                ProductModel p = new ProductModel();

                p.Id = int.Parse(item.ChildNodes[0].InnerText);
                p.Name = item.ChildNodes[1].InnerText;
                p.StartPrice = decimal.Parse(item.ChildNodes[2].InnerText);
                p.CurrentPrice = decimal.Parse(item.ChildNodes[3].InnerText);
                p.StartDate = DateTime.Parse(item.ChildNodes[4].InnerText.ToString());
                p.EndDate = DateTime.Parse(item.ChildNodes[5].InnerText.ToString());
                p.CloseDate = DateTime.Parse(item.ChildNodes[6].InnerText.ToString());
                p.Rating = int.Parse(item.ChildNodes[7].InnerText);
                p.Bids = int.Parse(item.ChildNodes[8].InnerText);
                
                var property = item.ChildNodes[9];
                p.property = new ProductModel.Property()
                {
                    ScreenSize = property.ChildNodes[0].InnerText,
                    Resolution = property.ChildNodes[1].InnerText,
                    ScreenQuality = property.ChildNodes[2].InnerText,
                    RAM = property.ChildNodes[3].InnerText,
                    CPU = property.ChildNodes[4].InnerText,
                    GPU = property.ChildNodes[5].InnerText,
                    Memory = property.ChildNodes[6].InnerText,
                    ExternalMemory = property.ChildNodes[7].InnerText,
                    Brand = property.ChildNodes[8].InnerText,
                    FrontCamera = property.ChildNodes[9].InnerText,    
                    BehindCamera = property.ChildNodes[10].InnerText,        
                    OS = property.ChildNodes[11].InnerText,
                    Battery = property.ChildNodes[12].InnerText,
                    SimType = property.ChildNodes[13].InnerText,
                    WarrantyTime = property.ChildNodes[14].InnerText
                };
                
                var picture = item.ChildNodes[10];
                p.pictures = new List<string>();
                p.pictures.Add(picture.ChildNodes[0].InnerText);
                p.pictures.Add(picture.ChildNodes[1].InnerText);
                p.pictures.Add(picture.ChildNodes[2].InnerText);
                p.clear = int.Parse(item["clear"].InnerText);
                ProductList.Add(p);
            }
        }

        private void PrepareCustomerList()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/App_Data/customers.xml"));
            XmlNodeList customers = xml.SelectNodes("/customers/customer");
            foreach (XmlNode item in customers)
            {
                CustomerModel c = new CustomerModel();
                c.Id = int.Parse(item.Attributes["id"].Value);
                c.Email = item.Attributes["email"].Value;
                c.FullName = item.Attributes["name"].Value;

                CustomerList.Add(c);
            }
        }

        private void PrepareBidList()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/App_Data/bids.xml"));
            XmlNodeList bids = xml.SelectNodes("/bids/bid");
            foreach (XmlNode item in bids)
            {
                BidModel b = new BidModel();
                b.Id = int.Parse(item.Attributes["id"].Value);
                b.CustomerId = int.Parse(item.Attributes["customerid"].Value);
                b.ProductId = int.Parse(item.Attributes["productid"].Value);
                b.BidPrice = decimal.Parse(item.Attributes["bidprice"].Value);
                b.Time = DateTime.Parse(item.Attributes["time"].Value);

                BidList.Add(b);
            }
        }
    }

}