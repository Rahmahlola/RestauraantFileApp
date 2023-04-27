using System.Collections.Generic;
using RestauraantFileApp.Models.Entities;

namespace RestauraantFileApp.Managers.Interface
{
    public interface IProductManager
    {
       public Product Create(string productName,int adminId,double price,string perPlate,string refNum,double totalQuantity); 
       public Product Get(int id);
       public Product Get(string refNum);
       public List<Product> GetAll(); 
       public Product Update(string productName,string refNum,double price,string perPlate);
       public void Delete(string refNum);
        public void RefreshFile();

       public double Totalquantity(double quantity,double totalQuantity);
    }
}