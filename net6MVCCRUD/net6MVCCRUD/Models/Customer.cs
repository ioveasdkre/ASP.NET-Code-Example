using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreConsoleAppla.Models
{
    public class Customer
    {
        public int id { get; set; }
#nullable enable
        [Required]
        [Comment("資料庫備註")]
        public string FirstName { get; set; }
        [Required]
        [Comment("我是name")]
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
#nullable disable
        public ICollection<Order> Orsers { get; set; }
    }
}
