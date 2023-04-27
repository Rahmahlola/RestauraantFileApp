using System;
using System.Collections.Generic;
using System.IO;
using RestauraantFileApp.Managers.Interface;
using RestauraantFileApp.Models.Entities;

namespace RestauraantFileApp.Managers.Implementation
{
    public class ProductManager : IProductManager
    {
        IAdminManager adminManager = new AdminManager();
      public static    List<Product> productDb = new List<Product>();
        string file = @"C:\Users\Owner\Desktop\RestauraantFileApp\Files\product.txt";
        // public ProductManager()
        // {
        //      ReadProductFromFile();
        // }
        public void ReadProductFromFile()
        {
            var info = new FileInfo(file);
            if(File.Exists(file) && info.Length > 0)
            {
                var products = File.ReadAllLines(file);
                
                foreach (var item in products)
                {
                //    Console.WriteLine(item);
                //    Product product = new Product(id,adminId,productName,price,true,totalQuantity,perPlate,refNum,false);
                     productDb.Add(Product.ToProduct(item));
                
                }
            }
            else if(!File.Exists(file))
            {
                 string path = @"C:\Users\Owner\Desktop\RestauraantFileApp\Files";
                Directory.CreateDirectory(path);
                string fileName = "product.txt";
                string fullPath = Path.Combine(path,fileName);
                File.Create(fullPath);

            }
            else
            {
                Console.WriteLine("No record found");
            }
        }
         private void AddproductToFile(Product product)
        {
            using(StreamWriter str = new StreamWriter(file, true))
            {
                str.WriteLine(product.ToString());
            }

        }
        public  void RefreshFile()
        {
            File.WriteAllText(file,string.Empty);
            using(StreamWriter str = new StreamWriter(file))
            {
                foreach (var item in productDb)
                {
                   
                    str.WriteLine(item.ToString());
                }
            }
        
        }
        private Product CheckIfExist(int adminId)
        {
            foreach (var product in productDb)
            {
                if(product.AdminId == adminId && product.IsDeleted == false)
                {
                    return product;
                }
            }
            return null;
        }
        
        private Product CheckIfExist(string refNum)
        {
            foreach (var product in productDb)
            {
                if(product.RefNum == refNum && product.IsDeleted == false)
                {
                    return product;
                }
            }
            return null;
        }
        private string GenerateRefNumber()
        {
            Random random = new Random();
            return random.Next(2500,9999).ToString();
        }
        public Product Create(string productName,int adminId,double price,string perPlate, string refNum,double totalQuantity)
        {
            var productExist = CheckIfExist(refNum);
            var adminExist = adminManager.Get(adminId);
            if(productExist == null)
            {
                int id = productDb.Count + 1;
                Product product = new Product(id,adminId,productName,price,true,totalQuantity," per Plate",GenerateRefNumber(),false);
                productDb.Add(product);
                AddproductToFile(product);
                return product;
            }
            Console.WriteLine("Product already exist");
            return null;
        }

        public void Delete(string refNum)
        {
            throw new System.NotImplementedException();
        }

        public Product Get(int id)
        {
            foreach (var product in productDb)
            {
                if(product.Id == id && product.IsDeleted == false)
                {
                    return product;
                }
            }
            return null;
        }

        public Product Get(string refNum)
        {
            foreach (var product in productDb)
            {
                if(product.RefNum == refNum && product.IsDeleted == false)
                {
                    return product;
                }
            }
            return null;
        }

        public List<Product> GetAll()
        {
            return productDb;
        }

        public Product Update(string productName, string refNum, double price, string perPlate)
        {
            var product = CheckIfExist(refNum);
            if(product != null)
            {
                product.ProductName = productName;
                product.Price = price;
                product.PerPlate = perPlate;
                
                productDb.Add(product);
                return product;
            }
            return null;
        }
        public double Totalquantity(double quantity,double totalQuantity)
        {
            if(totalQuantity >= quantity)
            {
                totalQuantity -= quantity;
                return quantity;
            }
            return 0;
        }
        
    }
}