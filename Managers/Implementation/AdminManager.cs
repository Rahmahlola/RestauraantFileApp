using System;
using System.Collections.Generic;
using System.IO;
using RestauraantFileApp.Managers.Interface;
using RestauraantFileApp.Models.Entities;

namespace RestauraantFileApp.Managers.Implementation
{
    public class AdminManager : IAdminManager
    {
        IUserManager userManager = new UserManager();
      private  static  List<Admin> adminDb = new List<Admin>();
        string file = @"C:\Users\Owner\Desktop\RestauraantFileApp\Files\Admin.txt";
        // public AdminManager()
        // {
        //      ReadAdminFromFile();
        // }
        public void ReadAdminFromFile()
        {
            var info = new FileInfo(file);
            if(File.Exists(file) && info.Length > 0)
            {
                
                   var admins = File.ReadAllLines(file);
                foreach (var admin in admins)
                {
                    adminDb.Add(Admin.ToAdmin(admin));
                
                }
            }
            else if(!File.Exists(file))
            {
                string path = @"C:\Users\Owner\Desktop\RestauraantFileApp\Files";
                Directory.CreateDirectory(path);
                string fileName = "admin.txt";
                string fullPath = Path.Combine(path,fileName);
                File.Create(fullPath);
            }
            else
            {
                Console.WriteLine("No record found");
            }
        }
         private void AddadminToFile(Admin admin)
        {
            using(StreamWriter str = new StreamWriter(file, true))
            {
                str.WriteLine(admin.ToString());
            }

        }
        public void RefreshFile()
        {
            File.WriteAllText(file,string.Empty);
            using(StreamWriter str = new StreamWriter(file))
            {
                foreach (var item in adminDb)
                {
                    str.WriteLine(item.ToString());
                }
            }
        
        }
        private Admin CheckIfExist(int userId)
        {
            foreach (var admin in adminDb)
            {
                if(admin.UserId == userId && admin.IsDeleted == false)
                {
                    return admin;
                }
            }
            return null;
        }
         private Admin CheckIfExist(string email)
        {
            var user = userManager.GetEmail(email);
            foreach (var admin in adminDb)
            {
                if(user.Email == email && admin.IsDeleted == false)
                {
                    return admin;
                }
            }
            return null;
        }

        public Admin Get(int userId)
        {
            foreach (var admin in adminDb)
            {
                if(admin.UserId == userId && admin.IsDeleted == false)
                {
                    return admin;
                }
            }
            return null;
        }

        public List<Admin> GetAll()
        {
            return adminDb;
        }

        public Admin GetEmail(string email)
        {
            var user = userManager.GetEmail(email);
           foreach (var admin  in adminDb)
           {
            if(user.Email == email)
            {
                return admin;
            }
           }
           return null;
        }

        public Admin Update(string name, string pin, string email, string phoneNumber,string address)
        {
            var user = userManager.GetEmail(email);
            if(user != null)
            {
                user.Name = name;
                user.Pin = pin;
                user.PhoneNumber = phoneNumber;
                user.Address = address;
                var admin = Get(user.Id);
                adminDb.Add(admin);
                return admin;

            }
            return null;
        }

        public Admin Register(string name, string email, string address, string pin, double wallet, string phoneNumber)
        {
            var userExist = userManager.GetEmail(email);
            if(userExist == null)
            {
                int id = adminDb.Count + 1;
                string role = "Admin";
                
                var user = userManager.Register(name, email, address, pin, 0, phoneNumber, role, false);
                
                 if(user != null)
                {
                    Admin admin = new Admin(id,user.Id,address,false);
                    adminDb.Add(admin);
                    AddadminToFile(admin);
                   
                    return admin;
                  
                 }
                 
            }
            
            return null;
        }

        public void Delete(string email)
        {
             var admin = CheckIfExist(email);
            if(admin == null)
            {
                Console.WriteLine("admin does not exist");
            }
            admin.IsDeleted = true;
            Console.WriteLine("admin deleted successfully");
        }

        public Admin Login(string email, string pin)
        {
            throw new NotImplementedException();
        }
        public Admin RecievedPayment(double amount)
        {
            var user = userManager.Get(adminDb[0].UserId);
            foreach (var admin in adminDb)
            {
                if(user.Role == "Admin")
                {
                    user.Wallet += amount;
                    RefreshFile();
                    return admin;
                }
            }
            return null;
        }
    }
}