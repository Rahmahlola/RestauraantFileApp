using System;
using System.Collections.Generic;
using System.Text;

namespace RestauraantFileApp.Models.Entities
{
    public class Order : BaseEntity
    {
        public int ProductId;
        public string CustomerEmail;
        public double TotalAmount;
        public DateTime Time;
        // public  Dictionary<string, Dictionary<double, int>>Carts;

        public Order(int id, int productId,string customerEmail,double totalAmount,DateTime time ,bool isDeleted) : base( id,isDeleted)
        {
            Id = id;
            ProductId = productId;
             CustomerEmail = customerEmail;
             TotalAmount = totalAmount;
              Time = time;
            // Carts = carts;
            IsDeleted = isDeleted;
        }
        
        public override string ToString()
        {
            // StringBuilder sb = new StringBuilder();
            // foreach (var item in Carts)
            // {
            //     sb.Append($"{item.Key}>");
            //     foreach (var itm in item.Value)
            //     {
            //         sb.Append($"{itm.Key}:{itm.Value},");
            //     }
            //     sb.Append('|');
            // }
            return $"{Id}\t{ProductId}\t{CustomerEmail}\t{TotalAmount}\t{Time}\t{IsDeleted}";
            
        }
        public static Order ToOrder(string model)
         {
             var ord = model.Split("\t");
            int id = int.Parse(ord[0]);
            int productId = int.Parse(ord[1]);
            string customerEmail = ord[2];
            double totalAmount = double.Parse(ord[3]);
             DateTime time = DateTime.Parse(ord[4]);
            
            bool isDeleted = bool.Parse(ord[5]);

        //    Dictionary<string, Dictionary<double, int>> carts = new Dictionary<string, Dictionary<double, int>>();
        //     var xyz = ord[5].Split('|');
        //     for(int i = 0; i < xyz.Length-1; i++)
        //     {
        //         Dictionary<double, int> b = new Dictionary<double, int>();
        //         var c = xyz[i].Split('>');
        //         var d = c[1].Split(',');
        //         for(int j = 0; j < d.Length-1; j++)
        //         {
        //             var e = d[j].Split(':');
        //             b.Add(double.Parse(e[0]), int.Parse(e[1]));
        //         }

        //         carts.Add(c[0], b);
        //     }
            
            return new Order(id,productId,customerEmail,totalAmount,time,isDeleted);
         }
    }
}