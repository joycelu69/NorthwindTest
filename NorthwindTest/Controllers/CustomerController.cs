using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindTest.Context;
using NorthwindTest.DbSet;
using NorthwindTest.Services;

namespace NorthwindTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICRUDService<Customers> _crudService;
        public CustomerController(ILogger<CustomerController> logger,
            ICRUDService<Customers> crudService)
        {
            _logger = logger;
            _crudService = crudService;
        }

        [HttpGet(Name = "GetAll")]
        public async Task<IEnumerable<Customers>> Get()
        {
            return await _crudService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customers>> GetCustomerById(string id)
        {
            var customer = await _crudService.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customers>> PostCustomer(Customers customer)
        {
            if (customer.CustomerID is null) return BadRequest();
            var createSuccess = await _crudService.Create(customer);
            if(!createSuccess) return BadRequest();
            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut]
        public async Task<IActionResult> PutCustomer(string id, Customers customer)
        {
            if (id != customer.CustomerID)
            {
                return BadRequest();
            }
            var updateSuccess = await _crudService.Update(customer);
            if(!updateSuccess) return BadRequest();
            return Ok();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            if (await GetCustomerById(id) is null)
            {
                return NotFound();
            }
            var deleteSuccess = await _crudService.DeleteById(id);
            if(!deleteSuccess) return BadRequest();
            return Ok();
        }
    }
}
