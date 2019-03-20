using AbacasX.DataContracts;
using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Data
{
    class OrderFilledMatchRepository : EFRepository<OrderFilledMatch, int>, IOrderFilledMatchRepository
    {
        public OrderFilledMatchRepository(DbContext context) : base(context) { }

        public override OrderFilledMatch GetById(int id)
        {
            throw new InvalidOperationException("Need both TransactionId and OffsetTransactionId");
        }

        public override void Delete(int id)
        {
            throw new InvalidOperationException("Need both TransactionId and OffsetTransactionId");
        }

        public void Delete(int TransactionId, int OffsetTransactionId)
        {
            throw new NotImplementedException();
        }

        public OrderFilledMatch GetByIds(int TransactionId, int OffsetTransactionId)
        {
            return DbSet.FirstOrDefault(ps => ((ps.TransactionId == TransactionId) && (ps.OffsetTransactionId == OffsetTransactionId) ||
                                               (ps.TransactionId == OffsetTransactionId) && (ps.OffsetTransactionId == TransactionId)));
        }
    }
}
