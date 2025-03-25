using System.ComponentModel.DataAnnotations;

namespace CustomersManager.DTOs.Customer
{
    public class CreateCustomerRequestDto
    {
        [Required]
        [StringLength(25)]
        public required string FirstName { get; set; }
        [Required]
        [StringLength(25)]
        public required string LastName { get; set; }
        [Required]
        [EmailAddress]
        public required string EmailAddress { get; set; }
    }
}
