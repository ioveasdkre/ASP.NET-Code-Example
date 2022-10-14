using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreConsoleAppla.Models
{
    public class ProductOrder
    {
        public int id { get; set; }
        public int Queantity { get; set; }
        public int Productld { get; set; }
        public int Orderld { get; set; }
        public Order Order { get; set; } = null!;
        public Product product { get; set; } = null!;
        public int MyProperty { get; set; }
    }
}
