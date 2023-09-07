using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickGrid.API.Data;
using QuickGrid.Shared.Dtos;

namespace QuickGrid.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(
            AppDbContext appDbContext,
            ILogger<CustomerController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<CustomersWrapper> Get([FromQuery]int skip, [FromQuery] int limit, [FromQuery] string? nameFilter, [FromQuery]string? emailAddressFilter, [FromQuery]string? userNameFilter)
        {

            var totalCustomerCount = await _appDbContext.Customers.CountAsync();
            var customersQuery = _appDbContext.Customers.AsQueryable();
                
            if (!string.IsNullOrEmpty(nameFilter))
            {
                customersQuery = customersQuery.Where(c => c.FirstName.Contains(nameFilter) 
                    || c.LastName.Contains(nameFilter));
            }

            if (!string.IsNullOrEmpty(emailAddressFilter))
            {
                customersQuery = customersQuery.Where(c => c.Email.Contains(emailAddressFilter));
            }

            if (!string.IsNullOrEmpty(userNameFilter))
            {
                customersQuery = customersQuery.Where(c => c.UserName.Contains(userNameFilter));
            }

            var customers = await customersQuery.Skip(skip)
                .Take(limit)
                .Select(c => new Customer
                {
                    Name = c.FirstName + " " + c.LastName,
                    EmailAddress = c.Email,
                    UserName = c.UserName,
                    Avatar = c.Avatar
                }).ToListAsync();

            return new CustomersWrapper
            {
                TotalCustomerCount = totalCustomerCount,
                Customers = customers
            };
        }
    }
}