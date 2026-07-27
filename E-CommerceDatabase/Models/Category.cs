using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceDatabase.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        // Navigation 
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
