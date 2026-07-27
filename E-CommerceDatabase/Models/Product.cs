using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceDatabase.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }

        // Navigation 
        public Category Category { get; set; }

        public List<OrderProduct> OrderProducts { get; set; } 
    }
}
