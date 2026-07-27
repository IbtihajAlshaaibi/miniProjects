using E_CommerceDatabase.Data;
using E_CommerceDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
namespace E_CommerceDatabase
{
    internal class Program
    {
        // Shared DbContext - created ONCE, here, so every function below reuses
        // the exact same instance instead of each function opening its own.
        static AppDbContext context = new AppDbContext();
        // Shared login state - 0 means "nobody is logged in".
        // Set by Login(), read by any function that requires a logged-in user,
        // reset back to 0 by Logout().
        static int loggedInUserId = 0;
        static void Main(string[] args)
        {
            bool exitApp = false;
            while (!exitApp)
            {
                Console.WriteLine("\n===== E-Commerce Console App =====");
                Console.WriteLine(" 1. Register New User");
                Console.WriteLine(" 2. Login");
                Console.WriteLine(" 3. Add New Category");
                Console.WriteLine(" 4. Add New Product");
                Console.WriteLine(" 5. View All Products");
                Console.WriteLine(" 6. Place an Order");
                Console.WriteLine(" 7. View My Orders");
                Console.WriteLine(" 8. View Order Details");
                Console.WriteLine(" 9. Add a Review for an Order");
                Console.WriteLine("10. View All Reviews for a Product");
                Console.WriteLine("11. Logout");
                Console.WriteLine(" 0. Exit");
                Console.Write("Enter your choice: ");
                int choice;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }
                switch (choice)
                {
                    case 1: RegisterUser(); break;
                    case 2: Login(); break;
                    case 3: AddCategory(); break;
                    case 4: AddProduct(); break;
                    case 5: ViewAllProducts(); break;
                    case 6: PlaceOrder(); break;
                    case 7: ViewMyOrders(); break;
                    case 8: ViewOrderDetails(); break;
                    case 9: AddReview(); break;
                    case 10: ViewReviewsForProduct(); break;
                    case 11: Logout(); break;
                    case 0:
                        exitApp = true;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        // ===================== FUNCTIONS =====================
        // Every function below talks to the console itself AND uses the
        // shared "context" field declared above - never create a new
        // AppDbContext() inside any of these functions.
        static void RegisterUser()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();


            Console.Write("Enter email: ");
            string email = Console.ReadLine();


            Console.Write("Enter password: ");
            string password = Console.ReadLine();



            User user = new User
            {
                Name = name,
                Email = email,
                Password = password
            };


            context.Users.Add(user);
            context.SaveChanges();


            Console.WriteLine("User registered successfully!");
        }
        static void Login()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();


            Console.Write("Password: ");
            string password = Console.ReadLine();



            var user = context.Users
                .FirstOrDefault(u =>
                u.Email == email &&
                u.Password == password);



            if (user != null)
            {
                loggedInUserId = user.UserId;

                Console.WriteLine("Login successful!");
            }
            else
            {
                Console.WriteLine("Wrong email or password");
            }
        }
        static void AddCategory()
        {
            Console.Write("Category name: ");
            string name = Console.ReadLine();


            



            Category category = new Category
            {
                CategoryName = name,
                
            };


            context.Categories.Add(category);

            context.SaveChanges();


            Console.WriteLine("Category added!");
        }
        static void AddProduct()
        {
            Console.Write("Product name: ");
            string name = Console.ReadLine();


            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());


            Console.Write("Stock: ");
            int stock = int.Parse(Console.ReadLine());



            var categories = context.Categories.ToList();


            Console.WriteLine("\nCategories:");

            foreach (var c in categories)
            {
                Console.WriteLine($"{c.CategoryId} - {c.CategoryName}");
            }



            Console.Write("Choose Category Id: ");

            int categoryId = int.Parse(Console.ReadLine());



            Product product = new Product
            {
                ProductName = name,
                Price = price,
                Stock = stock,
                CategoryId = categoryId
            };



            context.Products.Add(product);

            context.SaveChanges();


            Console.WriteLine("Product added!");
        }
        static void ViewAllProducts()
        {
            var products = context.Products
               .Include(p => p.Category)
               .ToList();


            Console.WriteLine("\n===== Products =====");


            foreach (var product in products)
            {
                Console.WriteLine(
                    $"ID: {product.ProductId} | " +
                    $"Name: {product.ProductName} | " +
                    $"Price: {product.Price} | " +
                    $"Category: {product.Category.CategoryName}"
                );
            }
        }
        static void PlaceOrder()
        {
            // Login check
            if (loggedInUserId == 0)
            {
                Console.WriteLine("Please login first!");
                return;
            }



            Console.WriteLine("\nAvailable Products:");

            var products = context.Products.ToList();


            foreach (var product in products)
            {
                Console.WriteLine(
                    $"{product.ProductId} - {product.ProductName} - {product.Price}"
                );
            }



            Order order = new Order
            {
                UserId = loggedInUserId,
                OrderDate = DateTime.Now
            };



            context.Orders.Add(order);

            context.SaveChanges();



            bool addingProducts = true;


            while (addingProducts)
            {
                Console.Write("\nEnter Product Id: ");
                int productId = int.Parse(Console.ReadLine());


                Console.Write("Enter Quantity: ");
                int quantity = int.Parse(Console.ReadLine());



                OrderProduct orderProduct = new OrderProduct
                {
                    OrderId = order.OrderId,
                    ProductId = productId,
                    Quantity = quantity
                };



                context.OrderProducts.Add(orderProduct);



                Console.Write("Add another product? (y/n): ");

                string answer = Console.ReadLine();


                if (answer.ToLower() != "y")
                {
                    addingProducts = false;
                }
            }



            context.SaveChanges();


            Console.WriteLine("Order placed successfully!");
        }
        static void ViewMyOrders()
        {
            if (loggedInUserId == 0)
            {
                Console.WriteLine("Please login first!");
                return;
            }



            var orders = context.Orders
                .Where(o => o.UserId == loggedInUserId)
                .ToList();



            Console.WriteLine("\n===== My Orders =====");


            foreach (var order in orders)
            {
                Console.WriteLine(
                    $"Order ID: {order.OrderId} | Date: {order.OrderDate}"
                );
            }
        }
        static void ViewOrderDetails()
        {
            Console.Write("Enter Order ID: ");

            int orderId = int.Parse(Console.ReadLine());



            var order = context.Orders
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .Include(o => o.Review)
                .FirstOrDefault(o => o.OrderId == orderId);



            if (order == null)
            {
                Console.WriteLine("Order not found");
                return;
            }



            Console.WriteLine("\n===== Order Details =====");

            double total = 0;



            foreach (var item in order.OrderProducts)
            {
                decimal price = item.Product.Price * item.Quantity;

                total += price;


                Console.WriteLine(
                    $"Product: {item.Product.ProductName} | " +
                    $"Quantity: {item.Quantity} | " +
                    $"Price: {price}"
                );
            }



            Console.WriteLine($"Total: {total}");



            if (order.Review != null)
            {
                Console.WriteLine("\nReview:");

                Console.WriteLine(
                    $"Rating: {order.Review.Rating}"
                );

                Console.WriteLine(
                    $"Comment: {order.Review.Comment}"
                );
            }
            else
            {
                Console.WriteLine("No review yet.");
            }
        }
        static void AddReview()
        {
            if (loggedInUserId == 0)
            {
                Console.WriteLine("Please login first!");
                return;
            }



            Console.Write("Enter Order ID: ");

            int orderId = int.Parse(Console.ReadLine());



            var order = context.Orders
                .Include(o => o.Review)
                .FirstOrDefault(o =>
                    o.OrderId == orderId &&
                    o.UserId == loggedInUserId);



            if (order == null)
            {
                Console.WriteLine(
                    "Order not found or does not belong to you."
                );

                return;
            }



            // Check one-to-one constraint
            if (order.Review != null)
            {
                Console.WriteLine(
                    "This order already has a review."
                );

                return;
            }



            Console.Write("Rating (1-5): ");

            int rating = int.Parse(Console.ReadLine());



            Console.Write("Comment: ");

            string comment = Console.ReadLine();



            Review review = new Review
            {
                OrderId = orderId,
                Rating = rating,
                Comment = comment
            };



            context.Reviews.Add(review);

            context.SaveChanges();



            Console.WriteLine(
                "Review added successfully!"
            );
        }
        static void ViewReviewsForProduct()

        {
            Console.Write("Enter Product ID: ");

            int productId = int.Parse(Console.ReadLine());



            var reviews = context.OrderProducts
                .Where(op => op.ProductId == productId)
                .Include(op => op.Order)
                .ThenInclude(o => o.Review)
                .ToList();



            Console.WriteLine(
                "\n===== Product Reviews ====="
            );



            bool found = false;



            foreach (var item in reviews)
            {
                if (item.Order.Review != null)
                {
                    found = true;


                    Console.WriteLine(
                        $"Rating: {item.Order.Review.Rating}"
                    );


                    Console.WriteLine(
                        $"Comment: {item.Order.Review.Comment}"
                    );


                    Console.WriteLine("------------------");
                }
            }



            if (!found)
            {
                Console.WriteLine(
                    "No reviews found for this product."
                );
            }
        }

        
        
        static void Logout()
        {
            loggedInUserId = 0;


            Console.WriteLine(
                "Logged out successfully!"
            );
        }
    }
}


