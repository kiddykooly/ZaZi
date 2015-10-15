using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZaZi.Models
{
    public class BidModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal BidPrice { get; set; }
        public DateTime Time { get; set; }
    }
}