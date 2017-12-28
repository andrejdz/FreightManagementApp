using FreightManagement.Domain.Model;
using FreightManagement.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FreightManagement.Service.Concrete
{
    public class OrderService : BaseService<Order>
    {
        public OrderService(DbContext context) : base()
        {
            _context = context;
        }

        /// <summary>
        /// Add new entity to db.
        /// </summary>
        /// <param name="item">New entity.</param>
        public override void Create(Order item)
        {
            if(item == null)
            {
                string message = "Try to add null item!";
                ArgumentNullException ex = new ArgumentNullException(message);
                Logger.Error(ex, $"{message} {ex.Message}");
                throw ex;
            }
            _context.Set<Order>().Add(item);
            SaveChanges(_context);
        }

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="id">Id of entity to delete.</param>
        async public override void Delete(int id)
        {
            Order order = await _context.Set<Order>().FindAsync(id);
            if(order != null)
            {
                _context.Entry(order).State = EntityState.Deleted;
            }

            SaveChanges(_context);
        }

        /// <summary>
        /// Get all entries of specified table.
        /// </summary>
        /// <returns>List of entries.</returns>
        public override Task<List<Order>> GetAll()
        {
            return _context.Set<Order>().ToListAsync();
        }

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id">Id of entity to retrive.</param>
        /// <returns>Entity.</returns>
        async public override Task<Order> GetById(int id)
        {
            Order order = await _context.Set<Order>().FindAsync(id);
            if(order != null)
            {
                return order;
            }

            return null;
        }

        /// <summary>
        /// Get last entity.
        /// </summary>
        /// <returns>Entity.</returns>
        async public override Task<Order> GetLast()
        {
            int maxId = await _context.Set<Order>().MaxAsync(c => c.Id);
            Order order = await GetById(maxId);
            if(order != null)
            {
                return order;
            }

            return null;
        }

        /// <summary>
        /// Update specific entity.
        /// </summary>
        /// <param name="item">Entity to update.</param>
        public override void Update(Order item)
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
