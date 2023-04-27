using System.Collections.Generic;
using System.Text;

namespace RestauraantFileApp.Models.Entities
{
    public class Product : BaseEntity
    
    {
     public int  AdminId;
     public string ProductName;
     public double Price;
     public bool IsAvailable;
     public string RefNum;
     public double TotalQuantity;
     public string PerPlate;
     

        public Product(int id,int adminId,string productName,double price,bool isAvailable,double totalQuantity,string perPlate,string refNum, bool isDeleted) : base(id, isDeleted)
        {
            Id = id;
            AdminId = adminId;
            ProductName = productName;
            Price = price;
            IsAvailable = isAvailable;
            RefNum = refNum;
            TotalQuantity = totalQuantity;
            PerPlate = perPlate;
            IsDeleted = isDeleted;
        }
         public override string ToString()
        {
            return $"{Id}\t{AdminId}\t{ProductName}\t{Price}\t{IsAvailable}\t{RefNum}\t{TotalQuantity}\t{PerPlate}\t{IsDeleted}";
        }
        public static Product ToProduct(string models)
        {
            var sep = models.Split("\t");
            int id = int.Parse(sep[0]);
            int adminId = int.Parse(sep[1]);
            string productName = sep[2];
            double price = double.Parse(sep[3]);
            bool isAvailable = bool.Parse(sep[4]);
            string refNum = sep[5];
            double totalQuantity = double.Parse(sep[6]);
            string perPlate = sep[7];
           bool isDeleted = bool.Parse(sep[8]);

            return new Product(id,adminId,productName,price,isAvailable,totalQuantity,refNum,perPlate,isDeleted);
        }
    }
}