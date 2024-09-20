using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindTest.Context;
using NorthwindTest.DbSet;

namespace NorthwindTest.Services
{
    public class CustomerCRUDService : ICRUDService<Customers>
    {
        private readonly ILogger<CustomerCRUDService> _logger;
        private readonly NorthwindContext _context;
        public CustomerCRUDService(ILogger<CustomerCRUDService> logger, NorthwindContext northwindContext)
        {
            _logger = logger;
            _context = northwindContext;
        }

        public async Task<bool> Create(Customers entity)
        {
            try
            {
                var customerId = entity.CustomerID.Length > 5 ? entity.CustomerID.Substring(0, 5) : entity.CustomerID;
                _context.Customers.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteById(string id)
        {
            try
            {
                var customer = await GetById(id);
                if(customer is null) return false;
                 _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<IEnumerable<Customers>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customers?> GetById(string id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<bool> Update(Customers entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
    }
}
