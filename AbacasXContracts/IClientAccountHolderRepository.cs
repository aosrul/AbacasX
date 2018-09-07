using AbacasXData.Contracts;
using AbacasXModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXContracts
{
    public interface IClientAccountHolderRepository : IRepository<ClientAccountHolder, int>
    {
        ClientAccountHolder GetByIds(int ClientId, int ClientAccountId);
        void Delete(int ClientId, int ClientAccountId);
    }
}
