using CustomersManager.Models;
using CustomersManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using CustomersManager.DTOs.Customer;

namespace CustomersManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(CustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerRequestDto createCustomerRequestDto)
        {
            try
            {
                var newCustomer = new Customer()
                {
                    FirstName = createCustomerRequestDto.FirstName,
                    LastName = createCustomerRequestDto.LastName,
                    EmailAddress = createCustomerRequestDto.EmailAddress,
                };

                var result = await _customerService.CreateCustomerAsync(newCustomer);

                return result ? Ok(new CreateCustomerResponseDto() { Message = "Customer created!" }) : BadRequest(new CreateCustomerResponseDto() { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerService.GetCustomersAsync();

            _logger.LogInformation($"Requesting customers info: {JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true })}");

            return result != null ? Ok(new { message = "Customers found", customers = result }) : BadRequest(new {message="Something went wrong"});
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _customerService.GetCustomerByIdAsync(id);

            return result != null ? Ok(new { message = "Customer found", customer = result }) : BadRequest(new { message = "Something went wrong" });
        }

        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _customerService.DeleteCustomerAsync(id);

        //    return result ? Ok(new { message = "Customer deleted" }) : BadRequest(new { message = "Something went wrong" });
        //}

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery]int id ,[FromBody]Customer customer)
        {
            var result = await _customerService.UpdateCustomerAsync(id, customer);

            return result ? Ok(new { message = "Customer updated" }) : BadRequest(new { message = "Something went wrong" });
        }
    }
}
