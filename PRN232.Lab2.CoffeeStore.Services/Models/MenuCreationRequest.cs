using System.ComponentModel.DataAnnotations;

namespace PRN232.Lab2.CoffeeStore.Services.Models
{
    public class MenuCreationRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime? FromDate { get; set; }
        [Required]
        public DateTime? ToDate { get; set; }

        public ProductInMenuRequest[] ProductInMenuList { get; set; }

        public class ProductInMenuRequest
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
