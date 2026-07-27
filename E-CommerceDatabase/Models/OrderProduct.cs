using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceDatabase.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        // Navigation 
        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}
