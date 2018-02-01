using System.Collections.Generic;

namespace CatStore.DAL.Entities
{
    public class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Cost { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
