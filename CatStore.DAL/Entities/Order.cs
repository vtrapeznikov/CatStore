using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatStore.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public double Sum { get; set; }
        public DateTime Date { get; set; }
        public int CatId { get; set; }
        public virtual Cat Cat { get; set; }
        [ForeignKey("ClientProfile")]
        public string ClientProfileId { get; set; }
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
