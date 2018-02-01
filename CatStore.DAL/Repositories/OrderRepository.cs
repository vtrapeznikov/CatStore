using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CatStore.DAL.EF;
using CatStore.DAL.Entities;
using CatStore.DAL.Interfaces;

namespace CatStore.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private CatContext Db;

        public OrderRepository(CatContext db)
        {
            Db = db;
        }

        public IEnumerable<Order> GetAll()
        {
            return Db.Orders.Include(o => o.Cat);
        }

        public Order Get(int id)
        {
            return Db.Orders.Find(id);
        }

        public void Create(Order order)
        {
            Db.Orders.Add(order);
        }

        public void Update(Order order)
        {
            Db.Entry(order).State = EntityState.Modified;
        }
        public IEnumerable<Order> Find(Func<Order, Boolean> predicate)
        {
            return Db.Orders.Include(o => o.Cat).Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Order order = Db.Orders.Find(id);
            if (order != null)
                Db.Orders.Remove(order);
        }
    }
}
