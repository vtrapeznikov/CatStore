using System;

namespace CatStore.BLL.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public double Sum { get; set; }
        public DateTime Date { get; set; }
        public int CatId { get; set; }
        public string ClientProfileId { get; set; }
    }
}
