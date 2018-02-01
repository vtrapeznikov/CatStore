using System;

namespace CatStore.WEB.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public float Sum { get; set; }
        public DateTime Date { get; set; }
        public int CatId { get; set; }
        public string ClientProfileId { get; set; }
    }
}