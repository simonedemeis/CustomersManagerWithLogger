using System.ComponentModel.DataAnnotations;

namespace CustomersManager.DTOs.Customer
{
    public class CustomerDto
    {
        [Required]
        public int Id { get; set; }
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
