using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenovaHrEmploymentAPI.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IList<T>> FindAll(decimal CompanyId);
        Task<T> FindById(string id);
        Task<bool> isExists(string id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();
    }
}
