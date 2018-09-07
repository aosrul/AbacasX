using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbacasXData.Contracts;
using AbacasXModel.Models;

namespace AbacasXContracts
{
    public interface IOrderFilledMatchRepository : IRepository<OrderFilledMatch, int>
    {
        OrderFilledMatch GetByIds(int TransactionId, int OffsetTransactionId);
        void Delete(int TransactionId, int OffsetTransactionId);
    }
}
