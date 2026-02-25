using System.ComponentModel.DataAnnotations;

namespace MidtermAPI_AryanGajjar.Models
{
    public class AGProduct
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 25 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; }
    }
    
}
