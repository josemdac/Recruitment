using RenovaHrEmploymentAPI;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Linq;
using System;
using RenovaHrEmploymentAPI.Services;

namespace RenovaHrEmploymentAPI.Contracts
{
    public interface IFcnGetRepository : IRepositoryBase<object>
    {
        public Task<IList<object>> GetAsList(string name, FcnCommand command);
        public Task<object> GetAsObject(string name, FcnCommand command);
        public void Execute(string name, FcnCommand command);
    }

}
