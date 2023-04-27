using System;
using System.Collections.Generic;
using RestauraantFileApp.Models.Entities;

namespace RestauraantFileApp.Managers.Interface
{
    public interface IOrderManager
    {
         public Order Get(int id);
        public Order Get(DateTime time);
        
        public List<Order> GetAll();
        public Order Update(string customerEmail);
        public Order Create(int id, int productId, string customerEmail, double totalAmount);
    }
}