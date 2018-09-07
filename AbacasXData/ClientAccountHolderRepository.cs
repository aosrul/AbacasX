using AbacasXContracts;
using AbacasXModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXData
{
    public class ClientAccountHolderRepository : EFRepository<ClientAccountHolder, int>, IClientAccountHolderRepository
    {
        public ClientAccountHolderRepository(DbContext context) : base(context) { }

        public override ClientAccountHolder GetById(int id)
        {
            throw new InvalidOperationException("Need both ClientId and ClientAccountId");
        }

        public override void Delete(int id)
        {
            throw new InvalidOperationException("Need both ClientId and ClientAccountId");
        }

        public void Delete(int ClientId, int ClientAccountId)
        {
            throw new NotImplementedException();
        }

        public ClientAccountHolder GetByIds(int ClientId, int ClientAccountId)
        {
            return DbSet.FirstOrDefault(ps => (ps.ClientId == ClientId) && (ps.ClientAccountId == ClientAccountId));
        }
    }
}
