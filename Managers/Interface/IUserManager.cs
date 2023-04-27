using System.Collections.Generic;
using RestauraantFileApp.Models.Entities;

namespace RestauraantFileApp.Managers.Interface
{
    public interface IUserManager
    {
        public User GetEmail(string email);
       public User Get(int id);
       public List<User> GetAll();
       public User Update(int id,string name, string email,string pin, string phoneNumber);
     public User Login(string email, string pin);
     
     public User RegisterUser(string name, string email,string address,string pin,string wallet,string phoneNumber,string role);
     public User Register(string name, string email,string address,string pin,double wallet, string phoneNumber,string role,bool isDeleted);
     
         public void Delete(int id);
         public void RefreshFile();
    }
}