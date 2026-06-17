
//steps for any program / system ( Ex: Ecommerce system )
//1- determine system entities ( ex: Customer, products, order, reviews )
//2- determie the entities properties ( ex: customer:CustomerId, CustomerName,Age, Email, Phone, Address )  
//                                          product: ProductId, ProductName, Description, Price, Stock )
//                                          order:   OrderId,  CustomerId, ProductId,  Quantity, totalPrice, DateTime)
//                                          review:  ReviewId, CustomerId, ProductId, comment, rating, Datetime
//3- System storage : create empty collection of each entity 
//4- system functionalities ( ex: add customer, add product, place order, add review, view products, view orders, view reviews )
//   system functionalities divides to two types:  - Crud operations ( Create, Read, Update, Delete ) 
//                                                    => ex: add customer, edit custmoer, delete cutomer, print customer details, add product, place order, add review, view products, view orders, view reviews
//                                                 - Inner functionalities ( ex: calculate total price, check stock availability, etc. )

/////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Backend_session2.Models;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
namespace Backend_session2
{

    public class Program
    {
        public static void RegisterUser(List<Customer> customersList)
        {
            //input user information
            Console.WriteLine("Enter User Name:");
            string userName = Console.ReadLine();

            Console.WriteLine("Enter User Age:");
            int userAge = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter User Email:");
            string userEmail = Console.ReadLine();

            Console.WriteLine("Enter User Phone");
            string userPhone = Console.ReadLine();

            int userId = (customersList.Count) + 1; //calculated

            //////////////////////////////////////////////
            //add user 

            customersList.Add( 
                 new Customer
                 {
                     customerId = userId,
                     customerName = userName,
                     customerEmail = userEmail,
                     customerPhone = userPhone,
                     customeAge = userAge
                 }
                
                );

            Console.WriteLine("Customer Added Successfully with ID " + userId);
        }

        public static void AddProduct(EcommerceContext context)
        {
            Console.WriteLine("Enter product name:");
            string productName = Console.ReadLine();

            Console.WriteLine("Enter product price:");
            decimal productPrice = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter Stock:");
            int productStock = int.Parse(Console.ReadLine());

            int ProductId = (context.Products.Count) + 1;

            context.Products.Add(

                new Product
                {
                    productId = ProductId,
                    productName = productName,
                    productPrice = productPrice,
                    productStock = productStock
                }
                );

            Console.WriteLine("Product Added Successfully");
        }

        public static void PlaceOrder(EcommerceContext context)
        {
            int orderId = (context.Orders.Count) + 1;

            Console.WriteLine("Enter your id : ");
            int UserId = int.Parse(Console.ReadLine());


            Console.WriteLine("Choose product number :");
            foreach (var product in context.Products)
            {
                Console.WriteLine("ID = " + product.productId + " , Name = " + product.productName + " , price = " + product.productPrice);
            }
            int productID = int.Parse(Console.ReadLine());



            Console.WriteLine("enter quantity:");
            int quantity = int.Parse(Console.ReadLine());


            var selectedProduct = context.Products.FirstOrDefault(item => item.productId == productID);
            decimal totalPrice = quantity * selectedProduct.productPrice;

            context.Orders.Add(
                new Order
                {
                    orderId = orderId,
                    customerId = UserId,
                    productId = productID,
                    quantity = quantity,
                    totalPrice = totalPrice,

                }
                
                );


            Console.WriteLine("Order placed successfully");
            selectedProduct.productStock -= quantity;




        }

        static void Main(string[] args)
        {
            //data storage for the system ( in memory )
            EcommerceContext mainContext = new EcommerceContext();
            mainContext.Customers = new List<Customer>();
            mainContext.Products = new List<Product>();
            mainContext.Orders = new List<Order>();
            mainContext.Reviews = new List<Review>();

            bool exit = false;
            while (exit == false)
            {
                //let the system begin 
                Console.WriteLine("Welcome to the E-commerce System!");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("10- Register Customer");
                Console.WriteLine("22- Add Product");
                Console.WriteLine("33- Place Order");
                Console.WriteLine("44- Add Review");
                Console.WriteLine("55- View Products");
                Console.WriteLine("66- View Orders");
                Console.WriteLine("77- View Reviews");
                Console.WriteLine("0- Exit");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 10:
                        // code for register customer
                        RegisterUser(mainContext.Customers);
                        break;
                    case 22:
                        // code for add product
                        AddProduct(mainContext);
                        break;
                    case 33:
                        // code for place order
                        break;
                    case 44:
                        // code for add review
                        break;
                    case 55:
                        // code for view products
                        break;
                    case 66:
                        // code for view orders
                        break;
                    case 77:
                        // code for view reviews
                        break;
                    case 0:
                        exit = true;
                       break;  
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;

                }

              Console.WriteLine("Press any key to continue...");
              Console.ReadKey();// to wait for user input before clearing the console
              Console.Clear();
            }
        }
    }
}




