using System.Collections.Generic;
using RestauraantFileApp.Models.Entities;

namespace RestauraantFileApp.Managers.Interface
{
    public interface ICustomerManager
    {
      
        public Customer Register(string name, string email,string address,string pin,double wallet, string phoneNumber);
        public Customer Get(int UserId);
        public Customer Get(string email);
        public Customer GetByPin(string  pin);
        public List<Customer> GetAll();
        public Customer Update(string name, string pin, string email, string phoneNumber,string address);
        public void Delete(string email);
        public void FundWallet(string email,double amount);
        public bool MakePayment(double amount,string email,int quantity);
        
        
    }
}