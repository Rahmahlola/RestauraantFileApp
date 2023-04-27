using System.Collections.Generic;
using RestauraantFileApp.Models.Entities;

namespace RestauraantFileApp.Managers.Interface
{
    public interface IAdminManager
    {
        public Admin Update(string name,string pin, string email, string phoneNumber,string address);
       public Admin Get(int userId);
        public Admin GetEmail(string email);
        public Admin Login(string email, string pin);
         public List<Admin> GetAll();
         public Admin Register(string name, string email,string address,string pin,double wallet, string phoneNumber);
         public void Delete(string email);
         public Admin RecievedPayment(double amount);
         public void RefreshFile();
         
    }
}