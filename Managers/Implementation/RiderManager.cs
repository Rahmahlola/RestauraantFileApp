using System;
using System.Collections.Generic;
using System.IO;
using RestauraantFileApp.Managers.Interface;
using RestauraantFileApp.Models.Entities;

namespace RestauraantFileApp.Managers.Implementation
{
    public class RiderManager : IRiderManager
    {
        IUserManager userManager = new UserManager();
        public static List<Rider> riderDb = new List<Rider>();
         string file = @"C:\Users\Owner\Desktop\RestauraantFileApp\Files\rider.txt";
        //  public RiderManager()
        // {
        //      ReadRiderFromFile();
        // }

        public void ReadRiderFromFile()
        {
            if(File.Exists(file))
            {
                var riders = File.ReadAllLines(file);
                foreach (var item in riders)
                {
                    riderDb.Add(Rider.ToRider(item));
                
                }
            }
            else
            {
                string path = @"C:\Users\Owner\Desktop\RestauraantFileApp\Files";
                Directory.CreateDirectory(path);
                string fileName = "rider.txt";
                string fullPath = Path.Combine(path,fileName);
                File.Create(fullPath);
            }
        }
         private void AddRiderToFile(Rider rider)
        {
            using(StreamWriter str = new StreamWriter(file, true))
            {
                str.WriteLine(rider.ToString());
            }

        }
        private void RefreshFile()
        {
            using(StreamWriter str = new StreamWriter(file))
            {
                foreach (var item in riderDb)
                {
                    str.WriteLine(item.ToString());
                }
            }
        
        }
        private Rider CheckIfExist(int userId)
        {
            foreach (var rider in riderDb)
            {
                if(rider.UserId == userId && rider.IsDeleted == false)
                {
                    return rider;
                }
            }
            return null;
        }
         private Rider CheckIfExist(string email)
        {
            var user = userManager.GetEmail(email);
            foreach (var rider in riderDb)
            {
                if(user.Email == email && rider.IsDeleted == false)
                {
                    return rider;
                }
            }
            return null;
        }
        public void Delete(string email)
        {
            var rider = CheckIfExist(email);
            if(rider == null)
            {
                Console.WriteLine("rider does not exist");
            }
            rider.IsDeleted = true;
            Console.WriteLine("rider deleted successfully");
        }

        public Rider Get(int userId)
        {
            
            foreach (var rider in riderDb)
            {
                if(rider.UserId == userId && rider.IsDeleted == false)
                {
                    return rider;
                }
            }
            return null;
        }

        public List<Rider> GetAll()
        {
            return riderDb;
        }

        public Rider GetEmail(string email)
        {
            var user = userManager.GetEmail(email);
           foreach (var rider  in riderDb)
           {
            if(user.Email == email)
            {
                return rider;
            }
           }
           return null;
        }

        public Rider Register(string name, string email, string address, string pin, double wallet, string phoneNumber)
        {
            var userExist = userManager.GetEmail(email);
            if(userExist == null)
            {
                int id = riderDb.Count + 1;
                string role = "Rider";
                
                
                var user = userManager.Register(name,email,address,pin,0,phoneNumber,role,false);
                 if(user != null)
                {
                    Rider rider = new Rider(id,user.Id,address,false);
                    riderDb.Add(rider);
                    AddRiderToFile(rider);
                    return rider;
                 }
            }
            return null;
        }

        public Rider Update(string name, string pin, string email, string phoneNumber, string address)
        {
            var user = userManager.GetEmail(email);
            if(user != null)
            {
                user.Name = name;
                user.Pin = pin;
                user.PhoneNumber = phoneNumber;
                user.Address = address;
                var rider = Get(user.Id);
                riderDb.Add(rider);
                return rider;

            }
            return null;
        }
    }
}