using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CatStore.DAL.Interfaces;
using CatStore.DAL.EF;
using CatStore.DAL.Entities;

namespace CatStore.DAL.Repositories
{
    public class CatRepository : IRepository<Cat>
    {
        private CatContext Db;

        public CatRepository(CatContext db)
        {
            Db = db;
        }

        public IEnumerable<Cat> GetAll()
        {
            return Db.Cats;
        }

        public Cat Get(int id)
        {
            return Db.Cats.Find(id);
        }

        public void Create(Cat item)
        {
            Db.Cats.Add(item);
        }

        public void Update(Cat item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }
        public IEnumerable<Cat> Find(Func<Cat, Boolean> predicate)
        {
            return Db.Cats.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Cat cat = Db.Cats.Find(id);
            if (cat != null)
                Db.Cats.Remove(cat);
        }
    }
}
