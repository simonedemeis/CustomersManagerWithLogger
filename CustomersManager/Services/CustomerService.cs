using CustomersManager.Data;
using CustomersManager.DTOs.Customer;
using CustomersManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomersManager.Services
{
    public class CustomerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ApplicationDbContext context, ILogger<CustomerService> logger) { 
            _context = context;
            _logger = logger;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<List<Customer>?> GetCustomersAsync()
        {
            try
            {
                return await _context.Customers.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            try
            {
                var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

                var customerDto = new CustomerDto()
                {
                    Id = existingCustomer.Id,
                    FirstName = existingCustomer.FirstName,
                    LastName = existingCustomer.LastName,
                    EmailAddress = existingCustomer.EmailAddress
                };

                return customerDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        //public async Task<bool> DeleteCustomerAsync(int id)
        //{
        //    try
        //    {
        //        var existingCustomer = await GetCustomerByIdAsync(id);


        //        if (existingCustomer == null)
        //        {
        //            return false;
        //        }

        //        _context.Customers.Remove(existingCustomer);

        //        return await SaveAsync();
        //    }
        //    catch(Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);
        //        return false;
        //    }
        //}

        public async Task<bool> UpdateCustomerAsync(int id, Customer customer)
        {
            try
            {
                var existingCustomer = await GetCustomerByIdAsync(id);

                if(existingCustomer == null)
                {
                    return false;
                }

                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.EmailAddress = customer.EmailAddress;

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
