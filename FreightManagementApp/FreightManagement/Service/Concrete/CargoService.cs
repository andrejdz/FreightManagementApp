using FreightManagement.Domain.Model;
using FreightManagement.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FreightManagement.Service.Concrete
{
    public class CargoService : BaseService<Cargo>
    {
        public CargoService(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add new entity to db.
        /// </summary>
        /// <param name="item">New entity.</param>
        public override void Create(Cargo item)
        {
            if(item == null)
            {
                string message = "Try to add null item!";
                ArgumentNullException ex = new ArgumentNullException(message);
                _logger.Error(ex, $"{message} {ex.Message}");
                throw ex;
            }
            _context.Set<Cargo>().Add(item);
            SaveChanges(_context);
        }

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="id">Id of entity to delete.</param>
        async public override void Delete(int id)
        {
            Cargo cargo = await _context.Set<Cargo>().FindAsync(id);
            if(cargo != null)
            {
                _context.Entry(cargo).State = EntityState.Deleted;
            }

            SaveChanges(_context);
        }

        /// <summary>
        /// Get all entries of specified table.
        /// </summary>
        /// <returns>List of entries.</returns>
        public override Task<List<Cargo>> GetAll()
        {
            return _context.Set<Cargo>().ToListAsync();
        }

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id">Id of entity to retrive.</param>
        /// <returns>Entity.</returns>
        async public override Task<Cargo> GetById(int id)
        {
            Cargo cargo = await _context.Set<Cargo>().FindAsync(id);
            if(cargo != null)
            {
                return cargo;
            }

            return null;
        }

        /// <summary>
        /// Get last entity.
        /// </summary>
        /// <returns>Entity.</returns>
        async public override Task<Cargo> GetLast()
        {
            int maxId = await _context.Set<Cargo>().MaxAsync(c => c.Id);
            Cargo cargo = await GetById(maxId);
            if(cargo != null)
            {
                return cargo;
            }

            return null;
        }

        /// <summary>
        /// Update specific entity.
        /// </summary>
        /// <param name="item">Entity to update.</param>
        public override void Update(Cargo item)
        {
            if(item == null)
            {
                string message = "Try to update nonexistent item!";
                ArgumentNullException ex = new ArgumentNullException(message);
                _logger.Error(ex, $"{message} {ex.Message}");
                throw ex;
            }

            _context.Entry(item).State = EntityState.Modified;

            SaveChanges(_context);
        }

        private DbContext _context;
    }
}
