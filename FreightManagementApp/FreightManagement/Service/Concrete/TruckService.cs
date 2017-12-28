using FreightManagement.Domain.Model;
using FreightManagement.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FreightManagement.Service.Concrete
{
    public class TruckService : BaseService<Truck>
    {
        public TruckService(DbContext context) : base()
        {
            _context = context;
        }

        /// <summary>
        /// Add new entity to db.
        /// </summary>
        /// <param name="item">New entity.</param>
        public override void Create(Truck item)
        {
            if(item == null)
            {
                string message = "Try to add null item!";
                ArgumentNullException ex = new ArgumentNullException(message);
                Logger.Error(ex, $"{message} {ex.Message}");
                throw ex;
            }
            _context.Set<Truck>().Add(item);
            SaveChanges(_context);
        }

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="id">Id of entity to delete.</param>
        async public override void Delete(int id)
        {
            Truck truck = await _context.Set<Truck>().FindAsync(id);
            if(truck != null)
            {
                _context.Entry(truck).State = EntityState.Deleted;
            }

            SaveChanges(_context);
        }

        /// <summary>
        /// Get all entries of specified table.
        /// </summary>
        /// <returns>List of entries.</returns>
        public override Task<List<Truck>> GetAll()
        {
            return _context.Set<Truck>().ToListAsync();
        }

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id">Id of entity to retrive.</param>
        /// <returns>Entity.</returns>
        async public override Task<Truck> GetById(int id)
        {
            Truck truck = await _context.Set<Truck>().FindAsync(id);
            if(truck != null)
            {
                return truck;
            }

            return null;
        }

        /// <summary>
        /// Get last entity.
        /// </summary>
        /// <returns>Entity.</returns>
        async public override Task<Truck> GetLast()
        {
            var maxId = await _context.Set<Truck>().MaxAsync(c => c.Id);
            Truck truck = await GetById(maxId);
            if(truck != null)
            {
                return truck;
            }

            return null;
        }

        /// <summary>
        /// Update specific entity.
        /// </summary>
        /// <param name="item">Entity to update.</param>
        public override void Update(Truck item)
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
