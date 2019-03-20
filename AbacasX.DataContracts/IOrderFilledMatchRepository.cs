using AbacasX.Model.Models;

namespace AbacasX.DataContracts
{
    public interface IOrderFilledMatchRepository : IRepository<OrderFilledMatch, int>
    {
        OrderFilledMatch GetByIds(int TransactionId, int OffsetTransactionId);
        void Delete(int TransactionId, int OffsetTransactionId);
    }
}
