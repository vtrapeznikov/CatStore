using System;

namespace CatStore.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public float Sum { get; set; }
        public int MyProperty { get; set; }
        public DateTime Date { get; set; }
        public int CatId { get; set; }
        public Cat Cat { get; set; }
    }
}
