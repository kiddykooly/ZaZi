using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Web;
using System.Web.Mvc;
using ZaZi.Models;

namespace ZaZi.Controllers
{
    public class HomeController : Controller
    {
        #region Utilities

        [NonAction]
        private CustomerModel getCustomerByEmail(string email)
        {
            CustomerModel customer = ZaZi.MvcApplication.CustomerList.Find(e => String.Equals(e.Email, email));
            return customer;
        }

        [NonAction]
        private BidModel getBidModel(int customerId, int productId)
        {
            BidModel bid = ZaZi.MvcApplication.BidList.Find(x => String.Equals(x.CustomerId, customerId) && String.Equals(x.ProductId, productId));
            return bid;
        }

        [NonAction]
        private void InsertOrUpdateCustomer(CustomerModel customer, bool insert)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/App_Data/customers.xml"));
            if (insert)
            {
                ZaZi.MvcApplication.CustomerList.Add(customer);

                XmlNode customers = xml.SelectSingleNode("customers");
                XmlNode c = xml.CreateNode(XmlNodeType.Element, "customer", null);

                XmlAttribute id = xml.CreateAttribute("id");
                id.Value = customer.Id.ToString();

                XmlAttribute email = xml.CreateAttribute("email");
                email.Value = customer.Email;

                XmlAttribute name = xml.CreateAttribute("name");
                name.Value = customer.FullName;

                c.Attributes.Append(id);
                c.Attributes.Append(email);
                c.Attributes.Append(name);

                customers.AppendChild(c);
            }
            else
            {
                ZaZi.MvcApplication.CustomerList.Find(x => x.Id == customer.Id).FullName = customer.FullName;

                XmlNodeList customers = xml.SelectNodes("/customers/customer");
                foreach (XmlNode item in customers)
                {
                    if (int.Parse(item.Attributes["id"].Value) == customer.Id)
                    {
                        item.Attributes["name"].Value = customer.FullName;
                        break;
                    }
                }
            }

            xml.Save(Server.MapPath("~/App_Data/customers.xml"));
        }

        [NonAction]
        private void InsertOrUpdateBid(BidModel model, bool insert)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/App_Data/bids.xml"));

            if (insert)
            {
                ZaZi.MvcApplication.BidList.Add(model);            

                XmlNode customers = xml.SelectSingleNode("bids");
                XmlNode c = xml.CreateNode(XmlNodeType.Element, "bid", null);

                XmlAttribute id = xml.CreateAttribute("id");
                id.Value = model.Id.ToString();

                XmlAttribute customerid = xml.CreateAttribute("customerid");
                customerid.Value = model.CustomerId.ToString();

                XmlAttribute productid = xml.CreateAttribute("productid");
                productid.Value = model.ProductId.ToString();

                XmlAttribute bidprice = xml.CreateAttribute("bidprice");
                bidprice.Value = model.BidPrice.ToString();

                XmlAttribute time = xml.CreateAttribute("time");
                time.Value = String.Format("{0:dd/MM/yyyy hh:mm:ss tt}", model.Time);

                c.Attributes.Append(id);
                c.Attributes.Append(customerid);
                c.Attributes.Append(productid);
                c.Attributes.Append(bidprice);
                c.Attributes.Append(time);

                customers.AppendChild(c);

            }
            else
            {
                ZaZi.MvcApplication.BidList.Find(x => x.Id == model.Id).BidPrice = model.BidPrice;
                ZaZi.MvcApplication.BidList.Find(x => x.Id == model.Id).Time = model.Time;

                XmlNodeList bids = xml.SelectNodes("/bids/bid");
                foreach (XmlNode item in bids)
                {
                    if (int.Parse(item.Attributes["id"].Value) == model.Id)
                    {
                        item.Attributes["bidprice"].Value = model.BidPrice.ToString();
                        item.Attributes["time"].Value = String.Format("{0:dd/MM/yyyy hh:mm:ss tt}", model.Time);
                        break;
                    }
                }
            }
            xml.Save(Server.MapPath("~/App_Data/bids.xml"));
        }

        [NonAction]
        private void UpdateProductPrice(BidModel model)
        {
            ZaZi.MvcApplication.ProductList.Find(x => String.Equals(x.Id, model.ProductId)).CurrentPrice = model.BidPrice;
            ZaZi.MvcApplication.ProductList.Find(y => String.Equals(y.Id, model.ProductId)).Bids = model.Id;
            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/App_Data/products.xml"));
            XmlNodeList products = xml.SelectNodes("/products/product");

            foreach (XmlNode item in products)
            {
                if (int.Parse(item.ChildNodes[0].InnerText) == model.ProductId)
                {
                    // Lưu lại bid của người đấu giá thắng
                    item["bids"].InnerText = model.Id.ToString();
                    item.ChildNodes[3].InnerText = model.BidPrice.ToString();
                    break;
                }
            }
            xml.Save(Server.MapPath("~/App_Data/products.xml"));
        }

        #endregion

        #region Methods
     
        public ActionResult Index()
        {
            //ProductModel product = ZaZi.MvcApplication.ProductList.FirstOrDefault();
            int k = getProduct();
            ProductModel product = ZaZi.MvcApplication.ProductList[k];
            //getRetard();
            ZaZi.MvcApplication.currentUser = getNumUser();
            return View(product);
        }
        // Get the Retard is loser
        public void getRetard()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/App_Data/bids.xml"));
            
            // Lấy bid chứa mã người thắng
            int bid = ZaZi.MvcApplication.ProductList[ZaZi.MvcApplication.productId].Bids;
            //ZaZi.MvcApplication.WinnerName = ZaZi.MvcApplication.CustomerList[customId].FullName;
            string nod = "/bids/bid[@id='" + bid + "']";
            XmlNode bids = xml.SelectSingleNode(nod);
            int customId = int.Parse(bids["customerid"].InnerText);
            ZaZi.MvcApplication.WinnerName = "i";
        }
        // get product lên
        // TODO
        public int getProduct()
        {
            for (int i = 0; i < ZaZi.MvcApplication.ProductList.Count; i++)
            {
                DateTime tst = DateTime.Now;
                DateTime t = ZaZi.MvcApplication.ProductList[i].EndDate;
                int result = DateTime.Compare(t, tst);
                if (result > 0)
                {
                    ZaZi.MvcApplication.productId = i;
                    return i;
                }
                //if(ZaZi.MvcApplication.ProductList[i].EndDate - 
            }
            return -1;
            //ProductModel p = new ProductModel();
        }
        // Đăng kí nhậ goiá ácb
        [ValidateAntiForgeryToken()]
        [HttpPost]
        public ActionResult InsertBid(TempleModel model)
        {
            
            //int k = getNumUser();

            CustomerModel customer = getCustomerByEmail(model.Email);
            BidModel bid;
            // Nếu giá thấp hơn giá hiện tại thì next
            if (ZaZi.MvcApplication.ProductList[ZaZi.MvcApplication.productId].CurrentPrice >= model.BidPrice)
            {
                return Json(new { Status = 1 });
            }
            if (customer != null)
            {
                customer.FullName = model.Name;
                InsertOrUpdateCustomer(customer, false);
                if (model.remember != null)
                {
                    Session["id"] = customer.Id;
                    Session["name"] = customer.FullName;
                    Session["mail"] = customer.Email;
                }
                bid = getBidModel(customer.Id, model.Id);
                if (bid == null)
                {
                    bid = new BidModel();
                    bid.Id = ZaZi.MvcApplication.BidList.Count + 1;
                    bid.CustomerId = customer.Id;
                    bid.ProductId = model.Id;
                    bid.BidPrice = model.BidPrice;
                    bid.Time = DateTime.Now;
                    ZaZi.MvcApplication.WinnerName = customer.FullName;
                    InsertOrUpdateBid(bid, true);
                }
                else
                {
                    bid.BidPrice = model.BidPrice;
                    bid.Time = DateTime.Now;
                    ZaZi.MvcApplication.WinnerName = customer.FullName;
                    InsertOrUpdateBid(bid, false);
                }
                
            }
            else
            {
                //ZaZi.MvcApplication.currentUser += 1;
                customer = new CustomerModel();
                customer.Id = ZaZi.MvcApplication.CustomerList.Count + 1;
                customer.Email = model.Email;
                customer.FullName = model.Name;
                InsertOrUpdateCustomer(customer, true);
                // Dùng cho việc ghi nhớ thôgn tin đăng nhập
                if (model.remember != null)
                {
                    Session["id"] = customer.Id;
                    Session["name"] = customer.FullName;
                    Session["mail"] = customer.Email;
                }
                bid = new BidModel();
                bid.Id = ZaZi.MvcApplication.BidList.Count + 1;
                bid.CustomerId = customer.Id;
                bid.ProductId = model.Id;
                bid.BidPrice = model.BidPrice;
                bid.Time = DateTime.Now;
                ZaZi.MvcApplication.WinnerName = customer.FullName;
                InsertOrUpdateBid(bid, true);
            }

            UpdateProductPrice(bid);
            return Json(new { Status = 1});
        }

        #endregion

        #region[ "Artiel" ]
        public int getNumUser()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/App_Data/bids.xml"));
            string nod = "/bids/bid[@productid='" + ZaZi.MvcApplication.productId + "']";
            XmlNodeList bids = xml.SelectNodes(nod);
            int count = bids.Count;
            // lấy tên người thắng -_- 
            int bid = ZaZi.MvcApplication.ProductList[ZaZi.MvcApplication.productId].Bids;
            //ZaZi.MvcApplication.WinnerName = ZaZi.MvcApplication.CustomerList[customId].FullName;
            if (bid == 0)
            {
                return count;
            }
            int custom = ZaZi.MvcApplication.BidList.Find(x => x.Id == bid).CustomerId;
            ZaZi.MvcApplication.WinnerName = ZaZi.MvcApplication.CustomerList.Find(x => x.Id == custom).FullName;


            //string temp = "/bids/bid[@id='" + bid + "']";
            //XmlNode nd = xml.SelectSingleNode(temp);
            //int customId = int.Parse(nd.Attributes["customerid"].Value);

            //xml.Load(Server.MapPath("~/App_Data/customers.xml"));
            //string getUser = "/customers/customer[@id='" + customId + "']";
            //XmlNode user = xml.SelectSingleNode(getUser);
            //ZaZi.MvcApplication.WinnerName = user.Attributes["name"].Value;

            return count;
        }
        #endregion
    }
}
