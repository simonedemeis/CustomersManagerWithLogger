using System.ComponentModel.DataAnnotations;

namespace CustomersManager.DTOs.Customer
{
    public class CreateCustomerResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
