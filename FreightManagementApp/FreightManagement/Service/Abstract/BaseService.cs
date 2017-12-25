using FreightManagement.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace FreightManagement.Service.Abstract
{
    public abstract class BaseService<TEntity> : IService<TEntity> where TEntity : class
    {
        public abstract void Create(TEntity item);
        public abstract void Delete(int id);
        public abstract Task<List<TEntity>> GetAll();
        public abstract Task<TEntity> GetById(int id);
        public abstract void Update(TEntity item);
        public abstract Task<TEntity> GetLast();

        /// <summary>
        /// Save changes to database.
        /// </summary>
        public void SaveChanges(DbContext context)
        {
            #region Save changes.
            try
            {
                context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                // Update original values from the database 
                //var entry = ex.Entries.Single();
                //entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                throw;
            }
            catch(DbUpdateException)
            {
                throw;
            }
            catch(DbEntityValidationException)
            {
                throw;
            }
            catch(NotSupportedException)
            {
                throw;
            }
            catch(ObjectDisposedException)
            {
                throw;
            }
            catch(InvalidOperationException)
            {
                throw;
            }
            #endregion
        }
    }
}