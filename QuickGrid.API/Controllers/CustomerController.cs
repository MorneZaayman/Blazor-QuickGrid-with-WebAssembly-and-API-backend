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
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _appDbContext.Customers
                .Select(c => new Customer
                {
                    Name = c.FirstName + " " + c.LastName,
                    EmailAddress = c.Email,
                    UserName = c.UserName,
                    Avatar = c.Avatar
                })
                .ToListAsync();
        }
    }
}