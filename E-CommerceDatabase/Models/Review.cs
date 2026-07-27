using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceDatabase.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        // Foreign Key
        public int OrderId { get; set; }

        // Navigation 
        public Order Order { get; set; }
    }
}
