using System;
using RestauraantFileApp.Managers.Implementation;
using RestauraantFileApp.Menu;

namespace RestauraantFileApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var adminManager = new AdminManager();
            adminManager.ReadAdminFromFile();
            var customerManager = new CustomerManager();
           customerManager.ReadCustomerFromFile();
           var riderManager = new RiderManager();
            riderManager.ReadRiderFromFile();
            var productManager = new ProductManager();
            productManager.ReadProductFromFile();
            var userManager = new UserManager();
            userManager.ReadUserFromFile();
            var orderManager = new OrderManager();
            orderManager.ReadOrderFromFile();
          MainMenu mainMenu = new MainMenu();
            mainMenu.Menu();
            
        
        
       

        }
    }
}
