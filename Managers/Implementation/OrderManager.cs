using System;
using System.Collections.Generic;
using System.IO;
using RestauraantFileApp.Managers.Interface;
using RestauraantFileApp.Models.Entities;

namespace RestauraantFileApp.Managers.Implementation
{
    public class OrderManager : IOrderManager
    {
        ICustomerManager customerManager = new CustomerManager();
        IUserManager userManager = new UserManager();
        IProductManager productManager = new ProductManager();
        public static  List<Order> orderDb = new List<Order>();
        string file = @"C:\Users\Owner\Desktop\RestauraantFileApp\Files\order.txt";
        // public OrderManager()
        // {
        //      ReadOrderFromFile();
        // }
         public void ReadOrderFromFile()
        {
            if(File.Exists(file))
            {
                var orders = File.ReadAllLines(file);
                foreach (var item in orders)
                {
                    orderDb.Add(Order.ToOrder(item));
                
                }
            }
            else
            {
                string path = @"C:\Users\Owner\Desktop\RestauraantFileApp\Files";
                Directory.CreateDirectory(path);
                string fileName = "order.txt";
                string fullPath = Path.Combine(path,fileName);
                File.Create(fullPath);
            }
        }
         private void AddorderToFile(Order order)
        {
            using(StreamWriter str = new StreamWriter(file, true))
            {
                str.WriteLine(order.ToString());
            }

        }
        private void RefreshFile()
        {
            using(StreamWriter str = new StreamWriter(file))
            {
                foreach (var item in orderDb)
                {
                    str.WriteLine(item.ToString());
                }
             }
        
        }
        private Order CheckIfExist(string customerEmail)
        {
            var customer = customerManager.Get(customerEmail);
            foreach (var order in orderDb)
            {
                if(order.CustomerEmail ==customerEmail && order.IsDeleted == false)
                {
                    return order;
                }
            }
            return null;
        }
        private Order CheckEmail(string customerEmail)
        {
            var user = userManager.GetEmail(customerEmail);
            foreach (var order in orderDb)
            {
                if(user.Email == customerEmail && order.IsDeleted == false)
                {
                    return order;
                }
            }
            return null;
        }
        public Order Create(int id, int productId, string customerEmail, double totalAmount)
        {
            
            
                Order order = new Order(orderDb.Count + 1,productId,customerEmail,totalAmount,DateTime.Now,false);
                orderDb.Add(order);
                AddorderToFile(order);
                return order;
           
        }

        public Order Get(int id)
        {
            foreach (var order in orderDb)
            {
                if(order.Id == id && order.IsDeleted == false)
                {
                    return order;
                }
            }
            return null;
        }

        public Order Get(DateTime time)
        {
            foreach (var order in orderDb)
            {
                if(order.Time == time && order.IsDeleted == false)
                {
                    return order;
                }
            }
            return null;
        }

        public List<Order> GetAll()
        {
            return orderDb;
        }

        public Order Update(string customerEmail)
        {
            var order = CheckIfExist(customerEmail);
            if(order != null)
            {
                
                orderDb.Add(order);
                return order;
            }
            return null;
        }
       

    }
}