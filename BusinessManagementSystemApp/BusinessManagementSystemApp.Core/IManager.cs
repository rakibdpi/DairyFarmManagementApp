using System.Collections.Generic;

namespace BusinessManagementSystemApp.Core
{
    public interface IManager<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        int Add(TEntity dto, string user);
        int AddRange(IEnumerable<TEntity> dtos, string user);
        int Update(int id, TEntity dto, string user);
        int LogicalRemove(int id, string user);
        int Remove(int id);
        int RemoveRange(IEnumerable<TEntity> dtos);
    }
}