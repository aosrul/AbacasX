using AbacasX.Model.Models;

namespace AbacasX.DataContracts
{
    public interface IAbacasXUow
    {
        void Commit();

        // Repositories
        IRepository<Asset, string> Assets { get; }
        IRepository<AssetAccount, int> AssetAccounts { get; }
        IRepository<AssetRate, int> AssetRates { get; }
        IRepository<AssetRateProvider, int> AssetRateProviders { get; }
        IRepository<AssetTransfer, int> AssetTransfers { get; }
        IRepository<BlockChainTokenFlow, int> BlockChainTokenFlows { get; }
        IRepository<Client, int> Clients { get; }
        IRepository<ClientAccount, int> ClientAccounts { get; }
        IClientAccountHolderRepository ClientAccountHolders { get; }
        IRepository<ClientAuthentication, int> ClientAuthentications { get; }
        IRepository<ClientKYC, int> ClientKYCs { get; }
        IRepository<ClientLogin, int> ClientLogins { get; }
        IRepository<ClientRegistration, int> ClientRegistrations { get; }
        IRepository<Custodian, int> Custodians { get; }
        IRepository<Order, int> Orders { get; }
        IRepository<OrderFilled, int> OrderFills { get; }
        IOrderFilledMatchRepository OrderFillsMatched {get;}
        IRepository<OrderLeg, int> OrderLegs { get; }
        IRepository<Token, string> Tokens { get; }
        IRepository<TokenAccount, int> TokenAccounts { get; }
        IRepository<TokenConversion, int> TokenConversions { get; }
        IRepository<TokenFlow, int> TokenFlows { get; }
        IRepository<TokenTrade, int> TokenTrades { get; }
        IRepository<Trust, int> Trusts { get; }

        // ITrustAccountRepository TrustAccounts {get;}
    }
}
