using System;
using RestauraantFileApp.Managers.Implementation;
using RestauraantFileApp.Managers.Interface;
using RestauraantFileApp.Menu.Interface;
using RestauraantFileApp.Menu.Implementation;

namespace RestauraantFileApp.Menu
{
    public class CustomerMenu : ICustomerMenu
    {
        IProductManager productManager = new ProductManager();
        IAdminManager adminManager = new AdminManager();
        IRiderManager riderManager = new RiderManager();
        ICustomerManager customerManager = new CustomerManager();
        IOrderManager orderManager = new  OrderManager();
        IUserManager userManager = new UserManager();
        public void CustomerMain()
        {
            Console.WriteLine("Enter 1 to View Food Menu\nEnter 2 to View Promo\nEnter 3 to View Special Promo\nEnter 4 to Fund Wallet\nEnter 5 to View My Wallet\nEnter 6 to Log out");
            int input = int.Parse(Console.ReadLine());
            if (input == 1)
            {
                FoodMenu();
            }
            else if (input == 2)
            {
                ViewPromo();
            }
            else if (input == 3)
            {
                ViewSpecialPromo();
            }
            else if (input == 4)
            {
                FundWalletMenu();
            }
            else if(input == 5)
            {
                ViewMyWallet();
            }
            else if (input == 6)
            {
                LogOut();
            }
        }

        public void FoodMenu()
        {
            
            var menus = productManager.GetAll();
            foreach (var menu in menus)
            {
                Console.WriteLine($"{menu.Id}\t {menu.ProductName}\t {menu.Price}\t{menu.PerPlate}\t");
            }
            Console.WriteLine("Enter Food id:");
            int id = int.Parse(Console.ReadLine());
            var foodmenu = productManager.Get(id);
            if(foodmenu != null)
            {
               Console.WriteLine($"{foodmenu.ProductName}\t{foodmenu.Price}\t{foodmenu.PerPlate}\t "); 
            Console.WriteLine("How many plate(s) do you want");
            int quantity =int.Parse(Console.ReadLine());
              if(foodmenu.TotalQuantity > 0)
                {
                 Console.WriteLine("Enter your Email for Confirmation of order:");
                string email = Console.ReadLine();
                var order = orderManager.Create(id,foodmenu.Id,email,foodmenu.Price);
                // var update = orderManager.Update(email);
                var customer = customerManager.Get(email);
                if(customer == null)
                {
                    Console.WriteLine($"invalid customer");
                    FoodMenu();
                }
                else
                {
                    var amount =  customerManager.MakePayment(foodmenu.Price,email,quantity);
                    
                    if(amount == true)
                    {
                        foodmenu.TotalQuantity -= quantity;
                       var productz = productManager.Get(id);
                       var admin = adminManager.RecievedPayment(foodmenu.Price);
                       userManager.RefreshFile();
                       productManager.RefreshFile();
                        Console.WriteLine("Thanks for Patronizing our Kitchen                       .......we dey for una");
                             CustomerMain();
                    }
                
                     
                }
                }
                else
                {
                    Console.WriteLine("Menu is not available");
                }
                
            }
            else
            {
                Console.WriteLine("wrong input");
            }
        }

         public void FundWalletMenu()
        {
             Console.WriteLine("Enter mail :");
            string mail = Console.ReadLine();
            Console.WriteLine("Enter amount :");
            double amount = double.Parse(Console.ReadLine());
             customerManager.FundWallet(mail,amount);
             userManager.RefreshFile();
            CustomerMain();
        }

        

          public void LogOut()
        {
         MainMenu m = new MainMenu();
          m.Menu();
        }

        public void ViewMyWallet()
        {
            var customers = customerManager.GetAll();
            foreach (var customer  in customers)
            {
                var user = userManager.Get(customer.UserId);
               Console.WriteLine($"Customer wallet:{user.Wallet}") ;
               CustomerMain();
            }
        }

        public void ViewPromo()
        {
            Console.WriteLine("1. Order 3 Plates of Food Menu for Free Set of Spoon\n2.Order 4 Plates of Food Menu for Free Set of Plate\n3.Order 5 Plates of Food Menu For Free Set of ServingTray ");
            MainMenu m = new MainMenu();
             m.Menu();
        }

    public void ViewSpecialPromo()
        {
              Console.WriteLine("1.Order 6 Plates of Food Menu for Free Set of Jugs\n2.Order 7 Plates of Food Menu for Free Set of Mug\n3.Order 8 Plates of Food Menu For Free Set of FryingPan\n4.Order All For FamilyFreeVoucher");
              MainMenu m = new MainMenu();
             m.Menu();
        }
        
    }
}