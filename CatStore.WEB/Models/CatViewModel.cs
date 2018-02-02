using System.ComponentModel.DataAnnotations;

namespace CatStore.WEB.Models
{
    public class CatViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Cost { get; set; }
        [Required]
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
    }
}