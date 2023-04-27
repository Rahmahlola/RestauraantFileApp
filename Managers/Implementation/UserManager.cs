using System;
using System.Collections.Generic;
using System.IO;
using RestauraantFileApp.Managers.Interface;
using RestauraantFileApp.Models.Entities;

namespace RestauraantFileApp.Managers.Implementation
{
    public class UserManager : IUserManager
    {
       public static List<User> userDb = new List<User>();
        string file = @"C:\Users\Owner\Desktop\RestauraantFileApp\Files\user.txt";
        // public UserManager()
        // {
        //      ReadUserFromFile();
        // }

        public void ReadUserFromFile()
        {
            var info = new FileInfo(file);
            if(File.Exists(file) && info.Length > 0)
            {
                var users = File.ReadAllLines(file);
               foreach (var item in users)
                {
                    userDb.Add(User.ToUser(item));
                
                }
            }
            else if(!File.Exists(file))
            {
                 string path = @"C:\Users\Owner\Desktop\RestauraantFileApp\Files";
                Directory.CreateDirectory(path);
                string fileName = "user.txt";
                string fullPath = Path.Combine(path,fileName);
                File.Create(fullPath);

            }
            else
            {
                Console.WriteLine("No record found");
            }
        }
          private void AddUserToFile(User user)
        {
            using(StreamWriter str = new StreamWriter(file,true))
            {
                
        
                    str.WriteLine(user.ToString());
            
            }
        }

        public User RegisterUser(string name, string email, string address,string pin,string wallet,string phoneNumber, string role)
        {
            User user = new User(UserManager.userDb.Count + 1,name,email,address,pin,0,phoneNumber,role,false);
            userDb.Add(user);
            userDb.Clear();
            AddUserToFile(user);
            return user;
        }
        public void RefreshFile()
        {
            File.WriteAllText(file,string.Empty);
            using (StreamWriter str = new StreamWriter(file))
            {
                foreach (var item in userDb)
                {
                    str.WriteLine(item.ToString());
                }
            }
        }
        public User Register(string name, string email, string address, string pin, double wallet, string phoneNumber, string role, bool isDeleted)
        {
            int id = userDb.Count + 1;
            User user = new User(id,name,email,address,pin,0,phoneNumber,role,false);
            userDb.Add(user);
            AddUserToFile(user);
            return user;
        }

        public User Get(int id)
        {
            foreach (var user in userDb)
            {
                if (user.Id == id) 
                {
                    return user;
                }
            }
           return null;;
        }

        public List<User> GetAll()
        {
            return userDb;
        }

        public User GetEmail(string email)
        {
            foreach (var user in userDb)
            {
                if (user.Email == email) 
                {
                    return user;
                }
            }
           return null;
        }

        public User Login(string email, string pin)
        {
            foreach(var user in userDb)
            {
                if(user.Email == email && user.Pin == pin)
                {
                    
                    return user;
                }
                 
            }
            return null;
        }

       

        public User Update(int id, string name, string email, string pin, string phoneNumber)
        {
           var user = CheckIfExisted(id);
            if (user != null )
            {
                user.Name = name;
                user.Pin = pin;
                user.Email = email;
                user.PhoneNumber = phoneNumber;
                userDb.Add(user);
                return user;
            }
            return null;
        }
         private User CheckIfExisted(int id)
        {
            foreach(var user in userDb)
            {
                if( user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }

        public void Delete(int id)
        {
           var user = CheckIfExisted(id);
            if(user == null)
            {
                Console.WriteLine("user does not exist");
            }
            user.IsDeleted = true;
            Console.WriteLine("user deleted successfully");
        }
    }
}