using System.ComponentModel.DataAnnotations;

namespace PRN232.Lab1.CoffeeStore.Service.Models
{
    public class ProductCreationRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public Guid? CategoryId { get; set; }
    }
}
