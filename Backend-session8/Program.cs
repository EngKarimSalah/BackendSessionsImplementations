using ECommerce_Solution.Models;

namespace ECommerce_Solution
{
    public class Program
    {
        // Static in-memory context — accessible by all functions without passing parameters
        public static ECommerceContext context = new ECommerceContext()
        {
            Users      = new List<User>(),
            Categories = new List<Category>(),
            Products   = new List<Product>(),
            Orders     = new List<Order>(),
            OrderItems = new List<OrderItem>(),
            Reviews    = new List<Review>()
        };

        // ─────────────────────────────────────────────────────────────────────
        // User functions
        // ─────────────────────────────────────────────────────────────────────
        public static void RegisterUser()
        {
            Console.WriteLine("\n=== Register New User ===");

            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            // In a real system this would be hashed — stored as plain text here for demo
            string passwordHash = password;

            Console.Write("Enter full name: ");
            string fullName = Console.ReadLine();

            Console.Write("Enter phone number (optional, press Enter to skip): ");
            string phone = Console.ReadLine();

            Console.Write("Enter address (optional, press Enter to skip): ");
            string address = Console.ReadLine();

            int userId = context.Users.Count + 1;

            context.Users.Add(new User
            {
                userId           = userId,
                username         = username,
                email            = email,
                passwordHash     = passwordHash,
                fullName         = fullName,
                phoneNumber      = string.IsNullOrWhiteSpace(phone) ? null : phone,
                address          = string.IsNullOrWhiteSpace(address) ? null : address,
                registrationDate = DateTime.Now,
                isActive         = true
            });

            Console.WriteLine($"User registered successfully. Assigned ID: {userId}");
        }

        public static void ViewAllUsers()
        {
            Console.WriteLine("\n=== All Users ===");

            foreach (User u in context.Users)
            {
                Console.WriteLine($"ID: {u.userId}  |  Username: {u.username}  |  Name: {u.fullName}" +
                                  $"  |  Email: {u.email}  |  Active: {u.isActive}");
            }
        }

 
        public static void AddCategory()
        {
            Console.WriteLine("\n=== Add New Category ===");

            Console.Write("Enter category name: ");
            string name = Console.ReadLine();

            Console.Write("Enter description (optional): ");
            string desc = Console.ReadLine();

            int categoryId = context.Categories.Count + 1;

            context.Categories.Add(new Category
            {
                categoryId   = categoryId,
                categoryName = name,
                description  = string.IsNullOrWhiteSpace(desc) ? null : desc
            });

            Console.WriteLine($"Category added. ID: {categoryId}");
        }

        public static void ViewAllCategories()
        {
            Console.WriteLine("\n=== All Categories ===");

            foreach (Category c in context.Categories)
                Console.WriteLine($"ID: {c.categoryId}  |  Name: {c.categoryName}  |  Desc: {c.description}");
        }

        public static void AddProduct()
        {
            Console.WriteLine("\n=== Add New Product ===");

            Console.WriteLine("Available categories:");
            foreach (Category c in context.Categories)
                Console.WriteLine($"  ID: {c.categoryId}  |  {c.categoryName}");

            Console.Write("Enter category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            Category category = context.Categories.FirstOrDefault(c => c.categoryId == categoryId);

            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter description (optional): ");
            string desc = Console.ReadLine();

            Console.Write("Enter price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Enter stock quantity: ");
            int stock = int.Parse(Console.ReadLine());

            int productId = context.Products.Count + 1;

            Product product = new Product
            {
                productId     = productId,
                productName   = name,
                description   = string.IsNullOrWhiteSpace(desc) ? null : desc,
                price         = price,
                stockQuantity = stock,
                categoryId    = categoryId,
                Category      = category,       // set navigation property directly
                createdAt     = DateTime.Now,
                isAvailable   = true
            };

            context.Products.Add(product);
            category.Products.Add(product);     // maintain reverse navigation

            Console.WriteLine($"Product added. ID: {productId}");
        }

        public static void ViewAllProducts()
        {
            Console.WriteLine("\n=== All Products ===");

            foreach (Product p in context.Products)
            {
                // navigation property — no search needed
                Console.WriteLine($"ID: {p.productId}  |  {p.productName}  |  Price: {p.price:C}" +
                                  $"  |  Stock: {p.stockQuantity}  |  Category: {p.Category.categoryName}" +
                                  $"  |  Available: {p.isAvailable}");
            }
        }

        //replacing the previous ViewProductsByCategory method with a new one that uses reverse navigation
        public static void ViewProductsByCategory() 
        {
            Console.WriteLine("\n=== View Products by Category ===");

            Console.WriteLine("Categories:");
            foreach (Category c in context.Categories)
                Console.WriteLine($"  ID: {c.categoryId}  |  {c.categoryName}");

            Console.Write("Enter category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            Category category = context.Categories.FirstOrDefault(c => c.categoryId == categoryId);

            Console.WriteLine($"\nProducts in '{category.categoryName}':");

            // reverse navigation — no list search needed
            foreach (Product p in category.Products)
                Console.WriteLine($"  ID: {p.productId}  |  {p.productName}  |  {p.price:C}  |  Stock: {p.stockQuantity}");
        }


        public static void PlaceOrder()
        {
            Console.WriteLine("\n=== Place New Order ===");

            Console.WriteLine("Users:");
            foreach (User u in context.Users)
                Console.WriteLine($"  ID: {u.userId}  |  {u.username}");

            Console.Write("Enter user ID: ");
            int userId = int.Parse(Console.ReadLine());
            User user = context.Users.FirstOrDefault(u => u.userId == userId);

            Console.Write("Enter shipping address: ");
            string shippingAddress = Console.ReadLine();

            Console.WriteLine("Payment methods: 1-CreditCard  2-DebitCard  3-PayPal  4-Cash");
            Console.Write("Choose payment method: ");
            int payChoice = int.Parse(Console.ReadLine());
            string[] payMethods = { "CreditCard", "DebitCard", "PayPal", "Cash" };
            string paymentMethod = payMethods[payChoice - 1];

            int orderId = context.Orders.Count + 1;
            Order order = new Order
            {
                orderId         = orderId,
                userId          = userId,
                User            = user,         // set navigation property
                orderDate       = DateTime.Now,
                totalAmount     = 0,
                status          = "Pending",
                shippingAddress = shippingAddress,
                paymentMethod   = paymentMethod
            };
            context.Orders.Add(order);
            user.Orders.Add(order);             // maintain reverse navigation

            // Add products to the order
            bool addingItems = true;
            while (addingItems)
            {
                Console.WriteLine("\nAvailable products:");
                foreach (Product p in context.Products.Where(p => p.isAvailable && p.stockQuantity > 0).ToList())
                    Console.WriteLine($"  ID: {p.productId}  |  {p.productName}  |  {p.price:C}  |  Stock: {p.stockQuantity}");

                Console.Write("Enter product ID to add (0 to finish): ");
                int productId = int.Parse(Console.ReadLine());
                if (productId == 0) break;

                Product product = context.Products.FirstOrDefault(p => p.productId == productId);

                Console.Write("Enter quantity: ");
                int qty = int.Parse(Console.ReadLine());

                int orderItemId = context.OrderItems.Count + 1;
                OrderItem item = new OrderItem
                {
                    orderItemId = orderItemId,
                    orderId     = orderId,
                    Order       = order,        // navigation property
                    productId   = productId,
                    Product     = product,       // navigation property
                    quantity    = qty,
                    unitPrice   = product.price  // snapshot price at time of ordering
                };

                context.OrderItems.Add(item);
                order.OrderItems.Add(item);     // reverse navigation on Order
                product.OrderItems.Add(item);   // reverse navigation on Product

                // update stock and running total
                product.stockQuantity -= qty;
                order.totalAmount     += item.unitPrice * qty;
            }

            Console.WriteLine($"\nOrder placed! Order ID: {orderId}  |  Total: {order.totalAmount:C}");
        }

        public static void ViewOrderHistory()
        {
            Console.WriteLine("\n=== Order History ===");

            Console.WriteLine("Users:");
            foreach (User u in context.Users)
                Console.WriteLine($"  ID: {u.userId}  |  {u.username}");

            Console.Write("Enter user ID: ");
            int userId = int.Parse(Console.ReadLine());
            User user = context.Users.FirstOrDefault(u => u.userId == userId);

            Console.WriteLine($"\nOrders for {user.fullName}:");
            // reverse navigation — directly through user.Orders
            foreach (Order o in user.Orders)
            {
                Console.WriteLine($"\n  Order ID: {o.orderId}  |  Date: {o.orderDate:d}" +
                                  $"  |  Status: {o.status}  |  Total: {o.totalAmount:C}");
                foreach (OrderItem item in o.OrderItems)
                {
                    // navigation property — no search
                    Console.WriteLine($"    - {item.Product.productName}  x{item.quantity}  @ {item.unitPrice:C}");
                }
            }

            decimal grandTotal = user.Orders.Sum(o => o.totalAmount);
            Console.WriteLine($"\n  TOTAL SPENT: {grandTotal:C}");
        }


        public static void WriteReview()
        {
            Console.WriteLine("\n=== Write a Review ===");

            Console.WriteLine("Users:");
            foreach (User u in context.Users)
                Console.WriteLine($"  ID: {u.userId}  |  {u.username}");
            Console.Write("Enter user ID: ");
            int userId = int.Parse(Console.ReadLine());
            User user = context.Users.FirstOrDefault(u => u.userId == userId);

            Console.WriteLine("\nProducts:");
            foreach (Product p in context.Products)
                Console.WriteLine($"  ID: {p.productId}  |  {p.productName}");
            Console.Write("Enter product ID to review: ");
            int productId = int.Parse(Console.ReadLine());
            Product product = context.Products.FirstOrDefault(p => p.productId == productId);

            Console.Write("Enter rating (1-5): ");
            int rating = int.Parse(Console.ReadLine());

            Console.Write("Enter comment (optional): ");
            string comment = Console.ReadLine();

            int reviewId = context.Reviews.Count + 1;
            Review review = new Review
            {
                reviewId   = reviewId,
                userId     = userId,
                User       = user,
                productId  = productId,
                Product    = product,
                rating     = rating,
                comment    = string.IsNullOrWhiteSpace(comment) ? null : comment,
                reviewDate = DateTime.Now
            };

            context.Reviews.Add(review);
            user.Reviews.Add(review);
            product.Reviews.Add(review);

            Console.WriteLine($"Review submitted! Review ID: {reviewId}");
        }

        public static void ViewProductReviews()
        {
            Console.WriteLine("\n=== Product Reviews ===");

            Console.WriteLine("Products:");
            foreach (Product p in context.Products)
                Console.WriteLine($"  ID: {p.productId}  |  {p.productName}");

            Console.Write("Enter product ID: ");
            int productId = int.Parse(Console.ReadLine());
            Product product = context.Products.FirstOrDefault(p => p.productId == productId);

            Console.WriteLine($"\nReviews for '{product.productName}':");
            foreach (Review r in product.Reviews)
            {
                // navigation — no search needed
                Console.WriteLine($"  [{r.rating}/5] by {r.User.username} on {r.reviewDate:d}");
                if (!string.IsNullOrWhiteSpace(r.comment))
                    Console.WriteLine($"    \"{r.comment}\"");
            }

            if (product.Reviews.Count > 0)
            {
                double avg = product.Reviews.Average(r => r.rating);
                Console.WriteLine($"\n  Average Rating: {avg:F1} / 5.0  ({product.Reviews.Count} reviews)");
            }
            else
            {
                Console.WriteLine("  No reviews yet.");
            }
        }


        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("        E-Commerce System");
                Console.WriteLine("========================================");
                Console.WriteLine(" 1  - Register User");
                Console.WriteLine(" 2  - View All Users");
                Console.WriteLine(" 3  - Add Category");
                Console.WriteLine(" 4  - View All Categories");
                Console.WriteLine(" 5  - Add Product");
                Console.WriteLine(" 6  - View All Products");
                Console.WriteLine(" 7  - View Products by Category");
                Console.WriteLine(" 8  - Place Order");
                Console.WriteLine(" 9  - View Order History");
                Console.WriteLine(" 10 - Write a Review");
                Console.WriteLine(" 11 - View Product Reviews");
                Console.WriteLine(" 0  - Exit");
                Console.WriteLine("========================================");
                Console.Write("Select option: ");

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:  RegisterUser();          break;
                    case 2:  ViewAllUsers();          break;
                    case 3:  AddCategory();           break;
                    case 4:  ViewAllCategories();     break;
                    case 5:  AddProduct();            break;
                    case 6:  ViewAllProducts();       break;
                    case 7:  ViewProductsByCategory();break;
                    case 8:  PlaceOrder();            break;
                    case 9:  ViewOrderHistory();      break;
                    case 10: WriteReview();           break;
                    case 11: ViewProductReviews();    break;
                    case 0:  exit = true;             break;
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
