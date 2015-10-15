using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZaZi.Models;

namespace ZaZi.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal StartPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int Rating { get; set; }
        public int Bids { get; set; }
        public Property property { get; set; }
        public List<string> pictures { get; set; }
        public partial class Property
        {
            public string ScreenSize { get; set; }
            public string Resolution { get; set; }
            public string ScreenQuality { get; set; }
            public string RAM { get; set; }
            public string CPU { get; set; }
            public string GPU { get; set; }
            public string Memory { get; set; }
            public string ExternalMemory { get; set; }
            public string Brand { get; set; }
            public string FrontCamera { get; set; }
            public string BehindCamera { get; set; }
            public string OS { get; set; }
            public string Battery { get; set; }
            public string SimType { get; set; }
            public string WarrantyTime { get; set; }
        }
        public int clear { get; set; }
    }
}