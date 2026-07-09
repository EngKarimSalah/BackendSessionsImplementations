using Backend_session_10_SoC.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_session_10_SoC
{
    public class Program
    {
        public static EcommerceContext context = new EcommerceContext();

        // ─────────────────────────────────────────────────────────────────────
        // SERVICE 01 — Register a New User                          [ADD]
        // ─────────────────────────────────────────────────────────────────────
        public static void RegisterUser()
        {
            Console.WriteLine("\n=== Register New User ===");

            Console.Write("Enter username: ");
            string name = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            Console.Write("Enter full name: ");
            string fullName = Console.ReadLine();

            Console.Write("Enter phone number (optional — press Enter to skip): ");
            string phone = Console.ReadLine();

            Console.Write("Enter address (optional — press Enter to skip): ");
            string address = Console.ReadLine();

            context.Users.Add(new User
            {
                Name = name,
                email = email,
                passwordHash = password,        // production: hash this
                fullName = fullName,
                phoneNumber = string.IsNullOrWhiteSpace(phone) ? null : phone,
                address = string.IsNullOrWhiteSpace(address) ? null : address,
                registrationDate = DateTime.Now,
                isActive = true
            });

            context.SaveChanges();

            User saved = context.Users.OrderBy(u => u.userId).Last();
            Console.WriteLine($"User registered successfully. Assigned ID: {saved.userId}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // SERVICE 02 — Add a New Product to a Category             [ADD]
        // ─────────────────────────────────────────────────────────────────────
        public static void AddProduct()
        {
            Console.WriteLine("\n=== Add New Product ===");

            List<Category> categories = context.Categories.ToList();
            Console.WriteLine("Available categories:");
            foreach (Category cat in categories)
                Console.WriteLine($"  ID: {cat.categoryId}  |  {cat.categoryName}");

            Console.Write("Enter category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter description (optional): ");
            string desc = Console.ReadLine();

            Console.Write("Enter price: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Enter stock quantity: ");
            int stock = int.Parse(Console.ReadLine());

            context.Products.Add(new Product
            {
                productName = name,
                description = string.IsNullOrWhiteSpace(desc) ? null : desc,
                price = price,
                stockQuantity = stock,
                categoryId = categoryId,
                createdAt = DateTime.Now,
                isAvailable = true
            });

            context.SaveChanges();

            Product saved = context.Products.OrderBy(p => p.productId).Last();
            Console.WriteLine($"Product added. Assigned ID: {saved.productId}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // SERVICE 03 — Place an Order                              [ADD]
        // ─────────────────────────────────────────────────────────────────────
        public static void PlaceOrder()
        {
            Console.WriteLine("\n=== Place New Order ===");

            List<User> users = context.Users.ToList();
            Console.WriteLine("Users:");
            foreach (User u in users)
                Console.WriteLine($"  ID: {u.userId}  |  {u.Name}");

            Console.Write("Enter user ID: ");
            int userId = int.Parse(Console.ReadLine());

            Console.Write("Enter shipping address: ");
            string shippingAddress = Console.ReadLine();

            Console.WriteLine("Payment methods: 1-CreditCard  2-DebitCard  3-PayPal  4-Cash");
            Console.Write("Choose: ");
            int payChoice = int.Parse(Console.ReadLine());
            string[] methods = { "CreditCard", "DebitCard", "PayPal", "Cash" };
            string paymentMethod = methods[payChoice - 1];

            // INSERT Order first to get the orderId from DB
            Order order = new Order
            {
                userId = userId,
                orderDate = DateTime.Now,
                totalAmount = 0,
                status = "Pending",
                shippingAddress = shippingAddress,
                paymentMethod = paymentMethod
            };
            context.Orders.Add(order);
            context.SaveChanges();   // orderId is now assigned

            while (true)
            {
                List<Product> available = context.Products
                    .Where(p => p.isAvailable && p.stockQuantity > 0)
                    .ToList();

                Console.WriteLine("\nAvailable products:");
                foreach (Product p in available)
                    Console.WriteLine($"  ID: {p.productId}  |  {p.productName}  |  {p.price:C}  |  Stock: {p.stockQuantity}");

                Console.Write("Enter product ID to add (0 to finish): ");
                int productId = int.Parse(Console.ReadLine());
                if (productId == 0) break;

                Product product = context.Products.FirstOrDefault(p => p.productId == productId);

                Console.Write("Enter quantity: ");
                int qty = int.Parse(Console.ReadLine());

                // INSERT OrderItem — bridge entity
                context.OrderItems.Add(new OrderItem
                {
                    orderId = order.orderId,
                    productId = productId,
                    quantity = qty,
                    unitPrice = (decimal)product.price  // price snapshot
                });

                // UPDATE product stock and order total (EF Core change tracker handles these)
                product.stockQuantity -= qty;
                order.totalAmount += (decimal)product.price * qty;

                context.SaveChanges();  // INSERT OrderItem + UPDATE Product + UPDATE Order
            }

            Console.WriteLine($"\nOrder placed! Order ID: {order.orderId}  |  Total: {order.totalAmount:C}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // SERVICE 04 — Write a Product Review                      [ADD]
        // ─────────────────────────────────────────────────────────────────────
        public static void WriteReview()
        {
            Console.WriteLine("\n=== Write a Review ===");

            List<User> users = context.Users.ToList();
            Console.WriteLine("Users:");
            foreach (User u in users)
                Console.WriteLine($"  ID: {u.userId}  |  {u.Name}");
            Console.Write("Enter user ID: ");
            int userId = int.Parse(Console.ReadLine());

            List<Product> products = context.Products.ToList();
            Console.WriteLine("\nProducts:");
            foreach (Product p in products)
                Console.WriteLine($"  ID: {p.productId}  |  {p.productName}");
            Console.Write("Enter product ID to review: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Enter rating (1-5): ");
            int rating = int.Parse(Console.ReadLine());

            Console.Write("Enter comment (optional): ");
            string comment = Console.ReadLine();

            context.reviews.Add(new Review
            {
                userId = userId,
                productId = productId,
                rating = rating,
                comment = string.IsNullOrWhiteSpace(comment) ? null : comment,
                reviewDate = DateTime.Now
            });

            context.SaveChanges();

            Review saved = context.reviews.OrderBy(r => r.reviewId).Last();
            Console.WriteLine($"Review submitted! Review ID: {saved.reviewId}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // SERVICE 05 — Update Product Price and Availability       [UPDATE]
        // ─────────────────────────────────────────────────────────────────────
        public static void UpdateProduct()
        {
            Console.WriteLine("\n=== Update Product Price & Availability ===");

            List<Product> products = context.Products.ToList();
            foreach (Product p in products)
                Console.WriteLine($"  ID: {p.productId}  |  {p.productName}  |  {p.price:C}  |  Available: {p.isAvailable}");

            Console.Write("Enter product ID to update: ");
            int productId = int.Parse(Console.ReadLine());

            Product product = context.Products.FirstOrDefault(p => p.productId == productId);

            Console.Write($"Enter new price (current: {product.price:C}): ");
            product.price = double.Parse(Console.ReadLine());

            Console.Write($"Is available? (y/n, current: {product.isAvailable}): ");
            product.isAvailable = Console.ReadLine().Trim().ToLower() == "y";

            context.SaveChanges();  // EF Core change tracker sends UPDATE SQL

            Console.WriteLine("Product updated successfully.");
        }

        // ─────────────────────────────────────────────────────────────────────
        // SERVICE 06 — Cancel an Order                             [UPDATE]
        // ─────────────────────────────────────────────────────────────────────
        public static void CancelOrder()
        {
            Console.WriteLine("\n=== Cancel an Order ===");

            List<Order> orders = context.Orders
                .Where(o => o.status != "Cancelled")
                .ToList();

            foreach (Order o in orders)
                Console.WriteLine($"  ID: {o.orderId}  |  Date: {o.orderDate:d}  |  Status: {o.status}  |  Total: {o.totalAmount:C}");

            Console.Write("Enter order ID to cancel: ");
            int orderId = int.Parse(Console.ReadLine());

            Order order = context.Orders.FirstOrDefault(o => o.orderId == orderId);

            // Restore stock for each item in the order
            List<OrderItem> items = context.OrderItems
                .Where(i => i.orderId == orderId)
                .ToList();

            foreach (OrderItem item in items)
            {
                Product product = context.Products.FirstOrDefault(p => p.productId == item.productId);
                product.stockQuantity += item.quantity;
            }

            order.status = "Cancelled";
            context.SaveChanges();  // UPDATE Order status + UPDATE all product stocks

            Console.WriteLine($"Order {orderId} cancelled. {items.Count} product stock(s) restored.");
        }

        // ─────────────────────────────────────────────────────────────────────
        // SERVICE 07 — Delete a Review                             [DELETE]
        // ─────────────────────────────────────────────────────────────────────
        public static void DeleteReview()
        {
            Console.WriteLine("\n=== Delete a Review ===");

            List<Review> reviews = context.reviews.ToList();
            foreach (Review r in reviews)
                Console.WriteLine($"  ID: {r.reviewId}  |  Rating: {r.rating}  |  Comment: {r.comment}");

            Console.Write("Enter review ID to delete: ");
            int reviewId = int.Parse(Console.ReadLine());

            Review review = context.reviews.FirstOrDefault(r => r.reviewId == reviewId);

            context.reviews.Remove(review);
            context.SaveChanges();  // DELETE FROM reviews WHERE reviewId = ...

            Console.WriteLine($"Review {reviewId} deleted.");
        }

        // ─────────────────────────────────────────────────────────────────────
        // SERVICE 08 — View All Products                           [GET-ALL]
        // ─────────────────────────────────────────────────────────────────────
        public static void ViewAllProducts()
        {
            Console.WriteLine("\n=== All Products ===");

            // SELECT * FROM Products — no Include, raw data only
            List<Product> products = context.Products.ToList();

            foreach (Product p in products)
                Console.WriteLine($"ID: {p.productId}  |  {p.productName}  |  {p.price:C}" +
                                  $"  |  Stock: {p.stockQuantity}  |  Available: {p.isAvailable}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // SERVICE 09 — Filter Products by Category and Price Range  [FILTER]
        // ─────────────────────────────────────────────────────────────────────
        public static void FilterProducts()
        {
            Console.WriteLine("\n=== Filter Products by Category & Price ===");

            List<Category> categories = context.Categories.ToList();
            Console.WriteLine("Categories:");
            foreach (Category c in categories)
                Console.WriteLine($"  ID: {c.categoryId}  |  {c.categoryName}");

            Console.Write("Enter category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            Console.Write("Enter minimum price: ");
            double minPrice = double.Parse(Console.ReadLine());

            Console.Write("Enter maximum price: ");
            double maxPrice = double.Parse(Console.ReadLine());

            // SELECT with WHERE on multiple conditions + ORDER BY
            List<Product> results = context.Products
                .Where(p => p.categoryId == categoryId && p.price >= minPrice && p.price <= maxPrice)
                .OrderBy(p => p.price)
                .ToList();

            Console.WriteLine($"\nFound {results.Count} product(s):");
            foreach (Product p in results)
                Console.WriteLine($"  ID: {p.productId}  |  {p.productName}  |  {p.price:C}  |  Stock: {p.stockQuantity}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // SERVICE 10 — Get Category with All Its Products          [INCLUDE]
        // ─────────────────────────────────────────────────────────────────────
        public static void GetCategoryWithProducts()
        {
            Console.WriteLine("\n=== Category with Its Products ===");

            List<Category> categories = context.Categories.ToList();
            foreach (Category c in categories)
                Console.WriteLine($"  ID: {c.categoryId}  |  {c.categoryName}");

            Console.Write("Enter category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            // Include loads Products in the same SQL query — one JOIN, no second query
            Category category = context.Categories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.categoryId == categoryId);

            Console.WriteLine($"\nCategory: {category.categoryName}");
            Console.WriteLine($"Description: {category.description}");
            Console.WriteLine($"\nProducts ({category.Products.Count}):");

            foreach (Product p in category.Products)
                Console.WriteLine($"  ID: {p.productId}  |  {p.productName}  |  {p.price:C}  |  Stock: {p.stockQuantity}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // SERVICE 11 — View Order History with Full Details        [INCLUDE + ThenInclude]
        // ─────────────────────────────────────────────────────────────────────
        public static void ViewOrderHistory()
        {
            Console.WriteLine("\n=== Order History with Full Details ===");

            List<User> users = context.Users.ToList();
            Console.WriteLine("Users:");
            foreach (User u in users)
                Console.WriteLine($"  ID: {u.userId}  |  {u.Name}");

            Console.Write("Enter user ID: ");
            int userId = int.Parse(Console.ReadLine());

            // 3-level ThenInclude chain: User → Orders → OrderItems → Product
            // Everything loaded in a single chained SQL query — no extra queries fire
            User user = context.Users
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(i => i.Product)
                .FirstOrDefault(u => u.userId == userId);

            Console.WriteLine($"\nOrder history for {user.fullName}:");

            foreach (Order o in user.Orders)
            {
                Console.WriteLine($"\n  Order ID: {o.orderId}  |  Date: {o.orderDate:d}" +
                                  $"  |  Status: {o.status}  |  Total: {o.totalAmount:C}");

                foreach (OrderItem item in o.OrderItems)
                {
                    // item.Product is already loaded — navigation property, no extra query
                    Console.WriteLine($"    - {item.Product.productName}  x{item.quantity}  @ {item.unitPrice:C}");
                }
            }

            decimal grandTotal = user.Orders.Sum(o => o.totalAmount);
            Console.WriteLine($"\n  TOTAL SPENT: {grandTotal:C}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // SERVICE 12 — Product Summary Report                      [PROJECT + LAZY]
        // ─────────────────────────────────────────────────────────────────────
        public static void ProductSummaryReport()
        {
            Console.WriteLine("\n=== Product Summary Report ===");

            // Part A — Projection: single efficient query, no Include needed
            // EF Core translates Select() directly into SQL with computed columns
            var summary = context.Products
                .Select(p => new
                {
                    p.productId,
                    p.productName,
                    p.price,
                    p.stockQuantity,
                    CategoryName = p.c.categoryName,            // JOIN with Categories
                    ReviewCount = p.Reviews.Count(),            // COUNT from Reviews
                    AvgRating = p.Reviews.Count() == 0
                                    ? 0.0
                                    : p.Reviews.Average(r => (double)r.rating) // AVG rating
                })
                .OrderBy(p => p.productName)
                .ToList();

            Console.WriteLine($"\n  {"Product",-25} {"Category",-18} {"Price",-10} {"Stock",-7} {"Reviews",-9} {"Avg"}");
            Console.WriteLine("  " + new string('-', 78));

            foreach (var item in summary)
            {
                Console.WriteLine($"  {item.productName,-25} {item.CategoryName,-18} {item.price,-10:C}" +
                                  $" {item.stockQuantity,-7} {item.ReviewCount,-9} {item.AvgRating:F1}");
            }

            // Part B — Lazy Loading demo
            // Product.Reviews and Product.c are virtual — EF Core fires a separate query on first access
            Console.WriteLine("\n--- Lazy Loading Demo ---");
            Console.Write("Enter a product ID to inspect lazily: ");
            int productId = int.Parse(Console.ReadLine());

            Product product = context.Products
                .FirstOrDefault(p => p.productId == productId);
            // ↑ Only Products table queried here

            Console.WriteLine($"Product loaded: {product.productName}");

            string categoryName = product.c.categoryName;
            // ↑ *** SECOND QUERY fires HERE — SELECT * FROM Categories WHERE categoryId = ... ***

            int reviewCount = product.Reviews.Count;
            // ↑ *** THIRD QUERY fires HERE — SELECT * FROM reviews WHERE productId = ... ***

            Console.WriteLine($"Category (lazy): {categoryName}");
            Console.WriteLine($"Reviews  (lazy): {reviewCount}");
            Console.WriteLine("(Each navigation access above fired a separate database query)");
        }

        // ─────────────────────────────────────────────────────────────────────
        // MAIN — Menu Loop
        // ─────────────────────────────────────────────────────────────────────
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n============================================");
                Console.WriteLine("          E-Commerce System");
                Console.WriteLine("============================================");
                Console.WriteLine(" 1  - Register User                   [ADD]");
                Console.WriteLine(" 2  - Add Product                     [ADD]");
                Console.WriteLine(" 3  - Place Order                     [ADD]");
                Console.WriteLine(" 4  - Write Review                    [ADD]");
                Console.WriteLine(" 5  - Update Product Price            [UPDATE]");
                Console.WriteLine(" 6  - Cancel Order                    [UPDATE]");
                Console.WriteLine(" 7  - Delete Review                   [DELETE]");
                Console.WriteLine(" 8  - View All Products               [GET-ALL]");
                Console.WriteLine(" 9  - Filter Products by Category     [FILTER]");
                Console.WriteLine(" 10 - Get Category with Products      [INCLUDE]");
                Console.WriteLine(" 11 - View Order History              [INCLUDE+THEN]");
                Console.WriteLine(" 12 - Product Summary Report          [PROJECT+LAZY]");
                Console.WriteLine(" 0  - Exit");
                Console.WriteLine("============================================");
                Console.Write("Select option: ");

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1: RegisterUser(); break;
                    case 2: AddProduct(); break;
                    case 3: PlaceOrder(); break;
                    case 4: WriteReview(); break;
                    case 5: UpdateProduct(); break;
                    case 6: CancelOrder(); break;
                    case 7: DeleteReview(); break;
                    case 8: ViewAllProducts(); break;
                    case 9: FilterProducts(); break;
                    case 10: GetCategoryWithProducts(); break;
                    case 11: ViewOrderHistory(); break;
                    case 12: ProductSummaryReport(); break;
                    case 0: exit = true; break;
                    default: Console.WriteLine("Invalid option. Please try again."); break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine("Goodbye!");
        }
    }
}