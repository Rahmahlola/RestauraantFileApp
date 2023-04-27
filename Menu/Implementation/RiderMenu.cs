using System;
using RestauraantFileApp.Managers.Implementation;
using RestauraantFileApp.Managers.Interface;
using RestauraantFileApp.Menu.Interface;

namespace RestauraantFileApp.Menu.Implementation
{
    public class RiderMenu : IRiderMenu
    {
       IProductManager productManager = new ProductManager();
        IAdminManager adminManager = new AdminManager();
        IRiderManager riderManager = new RiderManager();
        ICustomerManager customerManager = new CustomerManager();
        IOrderManager orderManager = new OrderManager();
        IUserManager userManager = new UserManager();
        

        public void  RiderMain()
        {
        Console.WriteLine("Enter 1 to View Order\nEnter 2 to LogOut");
            int input = int.Parse(Console.ReadLine());
            if (input == 1)
            {
                ViewAllOrder();
            }
            else if(input == 2)
            {
                LogOut();
            }
        }
        public void ViewAllOrder()
         {
             var orders = orderManager.GetAll();
            foreach (var order in orders)
            {
        
              Console.WriteLine($"{order.Id}\t{order.ProductId}\t{order.CustomerEmail}\t{order.Time}\t{order.TotalAmount}");
              
            } 
        
        }

        public void LogOut()
        {
            MainMenu m = new MainMenu();
             m.Menu();
        }
    }

        
}