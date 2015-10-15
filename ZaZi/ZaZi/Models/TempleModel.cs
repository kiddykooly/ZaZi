using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ZaZi.Models
{
    public class TempleModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal BidPrice { get; set; }
        public int Id { get; set; }
        public string remember { get; set; }
    }

    public class admin
    {
        public string Name { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public DateTime Time { get; set; }
        public int WinnerId { get; set; }
        public string Winner { get; set; }
        public string Mail { get; set; }
        public string Picture { get; set; }
    }
}