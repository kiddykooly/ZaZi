using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZaZi.Models
{
    [Serializable]
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}