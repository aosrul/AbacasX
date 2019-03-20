using AbacasX.DataContracts;
using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.DataContracts
{
    public interface IClientAccountHolderRepository : IRepository<ClientAccountHolder, int>
    {
        ClientAccountHolder GetByIds(int ClientId, int ClientAccountId);
        void Delete(int ClientId, int ClientAccountId);
    }
}
