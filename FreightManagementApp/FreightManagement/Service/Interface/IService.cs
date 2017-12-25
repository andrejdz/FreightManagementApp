using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FreightManagement.Service.Interface
{
    public interface IService<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<TEntity> GetLast();
        void Create(TEntity item);
        void Update(TEntity item);
        void Delete(int id);
        void SaveChanges(DbContext dbContext);
    }
}
