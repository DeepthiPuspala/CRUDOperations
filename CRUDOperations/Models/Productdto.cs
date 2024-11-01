using System.ComponentModel.DataAnnotations;

namespace CRUDOperations.Models
{
    public class Productdto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = "";
        [Required, MaxLength(100)]
        public string Brand { get; set; } = "";
        [Required, MaxLength(100)]
        public string Category { get; set; } = "";
        [Required]
        public decimal Price { get; set; }
        [ MaxLength(100)]
        public string Description { get; set; } = "";
    }
}
