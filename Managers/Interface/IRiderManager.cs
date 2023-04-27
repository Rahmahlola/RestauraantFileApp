using System.Collections.Generic;
using RestauraantFileApp.Models.Entities;

namespace RestauraantFileApp.Managers.Interface
{
    public interface IRiderManager
    {
         public Rider Update(string name,string pin, string email, string phoneNumber,string address);
       public Rider Get(int userId);
        public Rider GetEmail(string email);
         public List<Rider> GetAll();
         public Rider Register(string name, string email,string address,string pin,double wallet, string phoneNumber);
         public void Delete(string email);
    }
}