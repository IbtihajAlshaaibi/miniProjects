using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
namespace E_CommerceDatabase.Models
{
    public class OrderProduct
    {
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        // Navigation 
        public Order Order { get; set; }



        [ForeignKey("Product")]
        public int ProductId { get; set; }
        // Navigation
        public Product Product { get; set; }
        
        
        
        public int Quantity { get; set; }
    }
}
