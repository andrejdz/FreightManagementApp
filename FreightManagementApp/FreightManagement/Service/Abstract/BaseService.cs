using FreightManagement.Service.Interface;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
            catch(DbUpdateConcurrencyException ex)
            {
                // Update original values from the database 
                //var entry = ex.Entries.Single();
                //entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                _logger.Error(ex, ex.Message);
                throw;
            }
            catch(DbUpdateException ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
            catch(DbEntityValidationException ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
            catch(NotSupportedException ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
            catch(ObjectDisposedException ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
            catch(InvalidOperationException ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
            #endregion
        }

        protected static Logger _logger = LogManager.GetCurrentClassLogger();
    }
}