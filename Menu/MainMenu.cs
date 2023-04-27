using System;
using RestauraantFileApp.Managers.Implementation;
using RestauraantFileApp.Managers.Interface;
using RestauraantFileApp.Menu.Implementation;
using RestauraantFileApp.Menu.Interface;


namespace RestauraantFileApp.Menu
{
    public class MainMenu
    {
        ICustomerManager customerManager = new CustomerManager();
        IUserManager userManager = new UserManager();
        IAdminManager adminManager = new AdminManager();
        IProductManager productManager = new ProductManager();
        IRiderManager riderManager = new RiderManager();
        ICustomerMenu customerMenu = new CustomerMenu();
        IAdminMenu adminMenu = new AdminMenu();
        IRiderMenu riderMenu = new RiderMenu();

        public void Menu()
        {
            Console.WriteLine("Welcome To Madam Special Kitchen");
            Console.WriteLine("Enter 1 to Sign up\nEnter 2 to Log in ");
            int input = int.Parse(Console.ReadLine());
            if(input == 1)
            {
                Console.WriteLine("Enter 1 to Sign up as a Customer\nEnter 2 to Sign up as a Rider");
                int option = int.Parse(Console.ReadLine());
                if(option == 1)
                {
                    RegisterCustomerMenu();
                }
                else if( option == 2)
                {
                    RegisterRiderMenu();
                }
                else if (option == 9)
                {
                    RegisterAdminMenu();
                }
               
            }
            else if (input == 2)
            {
            
                 LoginMenu();
            }
            else
            {
                Console.WriteLine("Invalid option");
                Menu();
            }

        }
             public void RegisterCustomerMenu()
            {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Pin: ");
            string pin = Console.ReadLine();
            Console.Write("Enter PhoneNumber:");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter your Address: ");
            string address = Console.ReadLine();

            
            var customer = customerManager.Register(name,email,address,pin,0,phoneNumber);
           if(customer != null)
           {
            Console.WriteLine("You have sign up successfully");
           }
           
            else
             {
                 Console.WriteLine("user already exist");
            }
                Menu();
            }
            public void RegisterRiderMenu()
            {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Pin: ");
            string pin = Console.ReadLine();
            Console.Write("Enter PhoneNumber:");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter your Address: ");
            string address = Console.ReadLine();

            var rider = riderManager.Register(name,email,address,pin,0,phoneNumber);
             if(rider != null) 
            {  
             Console.WriteLine("You have sign up successfully");
            }
            else
             {
                 Console.WriteLine("user already exist");
            }
                Menu();
            }
             public void RegisterAdminMenu()
            {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Pin: ");
            string pin = Console.ReadLine();
            Console.Write("Enter PhoneNumber:");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter your Address: ");
            string address = Console.ReadLine();

            var admin = adminManager.Register(name,email,address,pin,0,phoneNumber);
             if(admin != null) 
            {  
             Console.WriteLine("You have sign up successfully");
            }
            else
             {
                 Console.WriteLine("user already exist");
            }
                Menu();
            } 

        
       
        public void LoginMenu()
        {
            Console.Write("Enter your Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter your Pin: ");
            string pin = Console.ReadLine();
            var signin = userManager.Login(email,pin);
            if (signin != null)
            {
             if (signin.Role == "Admin")
             {
                adminMenu.AdminMain();
             } 
             if(signin.Role == "Customer") 
             {
                customerMenu.CustomerMain();
             } 
             if(signin.Role == "Rider")
             {
                riderMenu.RiderMain();
             }
            }
           else
            {
                System.Console.WriteLine("Invalid Name or Password");
                
            }
        
        }   
    }
}