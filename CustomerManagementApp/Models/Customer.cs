using System.ComponentModel.DataAnnotations;

namespace CustomerManagementApp.Models
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }

        [Range(0, 110)]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*[0-9]).+$",
            ErrorMessage = "Must contain letters and numbers")]
        public string PostCode { get; set; }

        [Range(0, 2.5, ErrorMessage = "Height must be between 0 and 2.5 meters")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$",
       ErrorMessage = "Max 2 decimal places")]
        public double Height { get; set; }
    }
}
