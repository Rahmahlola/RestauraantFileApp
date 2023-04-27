using System;
using System.Collections.Generic;
using System.IO;
using RestauraantFileApp.Managers.Interface;
using RestauraantFileApp.Models.Entities;

namespace RestauraantFileApp.Managers.Implementation
{
    public class CustomerManager : ICustomerManager
    {
        IUserManager userManager = new UserManager();
       public static List<Customer> customerDb = new List<Customer>();
        
        string file = @"C:\Users\Owner\Desktop\RestauraantFileApp\Files\Customer.txt";
        // public CustomerManager()
        // {
        //      ReadCustomerFromFile();
        // }

       public void ReadCustomerFromFile()
        {
            if(File.Exists(file))
            {
                var customers = File.ReadAllLines(file);
                foreach (var customer in customers)
                {
                    customerDb.Add(Customer.ToCustomer(customer));
                
                }
            }
            else
            {
                string path = @"C:\Users\Owner\Desktop\RestauraantFileApp\Files";
                Directory.CreateDirectory(path);
                string fileName = "customer.txt";
                string fullPath = Path.Combine(path,fileName);
                File.Create(fullPath);
            }
        }
         private void AddCustomerToFile(Customer customer)
        {
            using(StreamWriter str = new StreamWriter(file, true))
            {
                str.WriteLine(customer.ToString());
            }

        }
        private void RefreshFile()
        {
            File.WriteAllText(file,string.Empty);

            using(StreamWriter str = new StreamWriter(file))
            {
                foreach (var customer in customerDb)
                {
                    str.WriteLine(customer.ToString());
                }
            }
        
        }
        private Customer CheckIfExist(int userId)
        {
            foreach (var customer in customerDb)
            {
                if(customer.UserId == userId && customer.IsDeleted == false)
                {
                    return customer;
                }
            }
            return null;
        }
         private Customer CheckIfExist(string email)
        {
            var user = userManager.GetEmail(email);
            foreach (var customer in customerDb)
            {
                if(user.Email == email && customer.IsDeleted == false)
                {
                    return customer;
                }
            }
            return null;
        }

        public Customer Register(string name, string email, string address, string pin, double wallet, string phoneNumber)
        {
            var userExist = userManager.GetEmail(email);
            if(userExist == null)
            {
                int id = customerDb.Count + 1;
                string role ="Customer";


                var user = userManager.Register(name, email, address, pin, 0, phoneNumber, role, false);
                
                
                 if(user != null)
                {
                    Customer customer = new Customer(id,user.Id,address,false);
                    customerDb.Add(customer);
                    AddCustomerToFile(customer);
                    return customer;
                 }
            }
            return null;
            
        }

        public void Delete(string email)
        {
            var customer = CheckIfExist(email);
            if(customer == null)
            {
                Console.WriteLine("Customer does not exist");
            }
            customer.IsDeleted = true;
            Console.WriteLine("Customer deleted successfully");
        }

        public void FundWallet(string email, double amount)
        {
            var user = userManager.GetEmail(email);
            if(user != null && user.IsDeleted == false)
            {
                if(amount > 0)
                {
                    user.Wallet += amount;
                    System.Console.WriteLine(user.Wallet);
                    // RefreshFile();

                    Console.WriteLine($"Mr/Mrs {user.Name}, you have successfully funded your account with {amount} and your new balance is {user.Wallet}");
                }
                else
                {
                Console.WriteLine("amount not valid");
                }
            
            }
            
            
        }

        public Customer Get(int userId)
        {
            
            foreach (var customer in customerDb)
            {
                if(customer.UserId == userId && customer.IsDeleted == false)
                {
                    return customer;
                }
            }
            return null;
        }

        public Customer Get(string email)
        {
           var user = userManager.GetEmail(email);
           foreach (var customer  in customerDb)
           {
            if(user.Email == email)
            {
                return customer;
            }
           }
           return null;
        }

        public List<Customer> GetAll()
        {
            return customerDb;
        }

        public Customer GetByPin(string pin)
        {
            throw new System.NotImplementedException();
        }

        public Customer Update(int userId, string name, string pin, string email, string phoneNumber, string address)
        {
            throw new System.NotImplementedException();
        }

        public Customer Update(string name, string pin, string email, string phoneNumber, string address)
        {
            var user = userManager.GetEmail(email);
            if(user != null)
            {
                user.Name = name;
                user.Pin = pin;
                user.PhoneNumber = phoneNumber;
                user.Address = address;
                var customer = Get(user.Id);
                customerDb.Add(customer);
                return customer;

            }
            return null;
        }
        public bool MakePayment(double amount,string email,int quantity)
        {
            var user = userManager.GetEmail(email);
            foreach (var customer in customerDb)
            {
                if(user.Email == email)
                {
                    if(user.Wallet >= amount)
                    {
                        user.Wallet-= amount * quantity;
                        
                        RefreshFile();
                     
                     return true;
                    }
                    else
                    {
                        return false;
                    }
                      
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        

    }
}