using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceDatabase.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        // Foreign Key
        public int UserId { get; set; }

        // Navigation 
        public User User { get; set; }

        public List<OrderProduct> OrderProducts { get; set; } 

        public Review Review { get; set; }
    }
}
