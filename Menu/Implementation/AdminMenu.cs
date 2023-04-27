using System;
using RestauraantFileApp.Managers.Implementation;
using RestauraantFileApp.Managers.Interface;
using RestauraantFileApp.Menu.Interface;
using RestauraantFileApp.Menu.Implementation;
namespace RestauraantFileApp.Menu
{
    public class AdminMenu : IAdminMenu
    {
        IProductManager productManager = new ProductManager();
        IUserManager userManager = new UserManager();
        IAdminManager adminManager = new AdminManager();
        IRiderManager riderManager = new RiderManager();
        ICustomerManager customerManager = new CustomerManager();
        public void AdminMain()
        {
            Console.WriteLine("Enter 1 to Create Product Menu\nEnter 2 to View all Products\nEnter 3 to View all Customers\nEnter 4 to View all Riders\nEnter 5 to View Wallet\nEnter 6 to Log Out");
            int input = int.Parse(Console.ReadLine());
            if(input == 1)
            {
              CreateProductMenu(); 
              Console.WriteLine("Enter 0 to Go Back :");
                int a =int.Parse(Console.ReadLine());
                if(a == 0)
                {
                    AdminMain();
                } 
            }
            else if(input == 2)
            {
                ViewAllProducts();
                Console.WriteLine("Enter 0 to Go Back :");
                int a =int.Parse(Console.ReadLine());
                if(a == 0)
                {
                    AdminMain();
                }
            }
            else if(input == 3)
            {
                ViewAllCustomers();
                Console.WriteLine("Enter 0 to Go Back :");
                int a =int.Parse(Console.ReadLine());
                if(a == 0)
                {
                    AdminMain();
                }
            }
            else if(input == 4)
            {
                ViewAllRiders();
                Console.WriteLine("Enter 0 to Go Back :");
                int a =int.Parse(Console.ReadLine());
                if(a == 0)
                {
                    AdminMain();
                }
            }
            else if (input == 5)
            {
                ViewWallet();
                Console.WriteLine("Enter 0 to Go Back :");
                int a =int.Parse(Console.ReadLine());
                if(a == 0)
                {
                    AdminMain();
                }
                
            }
            else if (input == 6)
            {
                LogOut();
            }

        }

        public void CreateProductMenu()
        {
            Console.WriteLine("Enter your id");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Product Name :");
            string name = Console.ReadLine();
            Console.WriteLine($"Enter Price for {name}:");
            double price =  double.Parse(Console.ReadLine());
            Console.WriteLine("Enter Product Reference Number:");
            string refNum = Console.ReadLine();
            Console.WriteLine("Enter the Total Quantity:");
            double totalquantity = double.Parse(Console.ReadLine());
            var admin = adminManager.Get(id);
            int adminId = admin.Id;
            var y =productManager.Create(name,adminId,price,"Per Plate",refNum,totalquantity);
            System.Console.WriteLine("You have successfully added to the menu");
            
        }

        public void LogOut()
        {
            MainMenu m = new MainMenu();
              m.Menu();
        }

        public void ViewAllCustomers()
        {
            var customers = customerManager.GetAll();
            foreach (var customer in customers)
            {
                var user = userManager.Get(customer.UserId);
                Console.WriteLine($"{customer.Id}\t Customer name is {user.Name}\t Email:{user.Email}\tAddress:{user.Address}\tPhoneNumber:{user.PhoneNumber}");
            }
        }

        public void ViewAllProducts()
        {
             var products = productManager.GetAll();
            foreach (var product in products)
            {
              Console.WriteLine($"{product.Id}\t{product.ProductName}\t{product.Price}\t{product.IsAvailable}\t{product.PerPlate}\t{product.RefNum}\t{product.IsDeleted}");
              
            } 
        
        }

        public void ViewAllRiders()
        {
             var riders = riderManager.GetAll();
            foreach (var rider in riders)
            {
              var user = userManager.Get(rider.UserId);
                Console.WriteLine($"{rider.Id}\t Rider name is {user.Name}\t Email:{user.Email}\tAddress:{user.Address}\tPhoneNumber:{user.PhoneNumber}");
            }
            
        }

        public void ViewWallet()
        {
            var admins = adminManager.GetAll();
            foreach (var admin  in admins)
            {
                var user = userManager.Get(admin.UserId);
               Console.WriteLine($"Admin wallet:{user.Wallet}") ;
            }
            
        }
    }
}