using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreConsoleAppla.Models
{
    public class Order
    {
        public int id { get; set; }
        public DateTime OrderPlaced { get; set; }
        public DateTime? OrderFulfilled { get; set; }
        public int Customerld { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
