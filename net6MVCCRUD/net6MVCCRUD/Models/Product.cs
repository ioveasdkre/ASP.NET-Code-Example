using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreConsoleAppla.Models
{
    public class Product
    {
        public int id { get; set; }
        [Required] // 不可空值
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; } = null!;
    }
}
