using FreightManagement.Domain.Model;
using FreightManagement.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FreightManagement.Service.Concrete
{
    public class CustomerService : BaseService<Customer>
    {
        public CustomerService(DbContext context) :base()
        {
            _context = context;
        }

        /// <summary>
        /// Add new entity to db.
        /// </summary>
        /// <param name="item">New entity.</param>
        public override void Create(Customer item)
        {
            if(item == null)
            {
                string message = "Try to add null item!";
                ArgumentNullException ex = new ArgumentNullException(message);
                Logger.Error(ex, $"{message} {ex.Message}");
                throw ex;
            }
            _context.Set<Customer>().Add(item);
            SaveChanges(_context);
        }

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="id">Id of entity to delete.</param>
        async public override void Delete(int id)
        {
            Customer customer = await _context.Set<Customer>().FindAsync(id);
            if(customer != null)
            {
                _context.Entry(customer).State = EntityState.Deleted;
            }

            SaveChanges(_context);
        }

        /// <summary>
        /// Get all entries of specified table.
        /// </summary>
        /// <returns>List of entries.</returns>
        public override Task<List<Customer>> GetAll()
        {
            return _context.Set<Customer>().ToListAsync();
        }

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id">Id of entity to retrive.</param>
        /// <returns>Entity.</returns>
        async public override Task<Customer> GetById(int id)
        {
            Customer customer = await _context.Set<Customer>().FindAsync(id);
            if(customer != null)
            {
                return customer;
            }

            return null;
        }

        /// <summary>
        /// Get last entity.
        /// </summary>
        /// <returns>Entity.</returns>
        async public override Task<Customer> GetLast()
        {
            int maxId = await _context.Set<Customer>().MaxAsync(c => c.Id);
            Customer customer = await GetById(maxId);
            if(customer != null)
            {
                return customer;
            }

            return null;
        }

        /// <summary>
        /// Update specific entity.
        /// </summary>
        /// <param name="item">Entity to update.</param>
        public override void Update(Customer item)
        {
            if(item == null)
            {
                string message = "Try to update nonexistent item!";
                ArgumentNullException ex = new ArgumentNullException(message);
                Logger.Error(ex, $"{message} {ex.Message}");
                throw ex;
            }

            _context.Entry(item).State = EntityState.Modified;

            SaveChanges(_context);
        }

        private DbContext _context;
    }
}
