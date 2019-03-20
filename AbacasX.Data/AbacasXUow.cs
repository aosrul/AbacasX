using AbacasX.DataContracts;
using AbacasX.Data.Helpers;
using AbacasX.Model.Models;
using System;
using AbacasX.Data.Models.Data;

namespace AbacasX.Data
{
    class AbacasXUow : IAbacasXUow, IDisposable
    {

        private AbacasXDbContext DbContext { get; set; }
        protected IRepositoryProvider RepositoryProvider { get; set; }

        public AbacasXUow(IRepositoryProvider repositoryProvider)
        {
            CreateDbContext();

            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider;
        }

        // Code Camper repositories

        public IRepository<Asset,string> Assets { get { return GetStandardRepo<Asset,string>(); } }
        public IRepository<AssetAccount, int> AssetAccounts { get { return GetStandardRepo<AssetAccount, int>(); } }
        public IRepository<AssetRate, int> AssetRates { get { return GetStandardRepo<AssetRate, int>(); } }
        public IRepository<AssetRateProvider, int> AssetRateProviders { get { return GetStandardRepo<AssetRateProvider, int>(); } }
        public IRepository<AssetTransfer, int> AssetTransfers { get { return GetStandardRepo<AssetTransfer, int>(); } }
        public IRepository<BlockChainTokenFlow, int> BlockChainTokenFlows { get { return GetStandardRepo<BlockChainTokenFlow, int>(); } }
        public IRepository<Client, int> Clients { get { return GetStandardRepo<Client, int>(); } }
        public IRepository<ClientAccount, int> ClientAccounts { get { return GetStandardRepo<ClientAccount, int>(); } }
        public IClientAccountHolderRepository ClientAccountHolders { get { return GetRepo<IClientAccountHolderRepository>(); } }
        public IRepository<ClientAuthentication, int> ClientAuthentications { get { return GetStandardRepo<ClientAuthentication, int>(); } }
        public IRepository<ClientKYC, int> ClientKYCs { get { return GetStandardRepo<ClientKYC, int>(); } }
        public IRepository<ClientLogin, int> ClientLogins { get { return GetStandardRepo<ClientLogin, int>(); } }
        public IRepository<ClientRegistration, int> ClientRegistrations { get { return GetStandardRepo<ClientRegistration, int>(); } }
        public IRepository<Custodian, int> Custodians { get { return GetStandardRepo<Custodian, int>(); } }
        public IRepository<Order, int> Orders { get { return GetStandardRepo<Order, int>(); } }
        public IRepository<OrderFilled, int> OrderFills { get { return GetStandardRepo<OrderFilled, int>(); } }
        public IOrderFilledMatchRepository OrderFillsMatched { get { return GetRepo<IOrderFilledMatchRepository>(); } }
        public IRepository<OrderLeg, int> OrderLegs { get { return GetStandardRepo<OrderLeg, int>(); } }
        public IRepository<Token, string> Tokens { get { return GetStandardRepo<Token, string>(); } }
        public IRepository<TokenAccount, int> TokenAccounts { get { return GetStandardRepo<TokenAccount, int>(); } }
        public IRepository<TokenConversion, int> TokenConversions { get { return GetStandardRepo<TokenConversion, int>(); } }
        public IRepository<TokenFlow, int> TokenFlows { get { return GetStandardRepo<TokenFlow, int>(); } }
        public IRepository<TokenTrade, int> TokenTrades { get { return GetStandardRepo<TokenTrade, int>(); } }
        public IRepository<Trust, int> Trusts { get { return GetStandardRepo<Trust, int>(); } }

        
        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        public void Commit()
        {
            //System.Diagnostics.Debug.WriteLine("Committed");
            DbContext.SaveChanges();
        }

        protected void CreateDbContext()
        {
            DbContext = new AbacasXDbContext();

            // Do NOT enable proxied entities, else serialization fails
            DbContext.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            DbContext.Configuration.ValidateOnSaveEnabled = false;

            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }


        private IRepository<T,I> GetStandardRepo<T,I>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T,I>();
        }
        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }

        #endregion
    }
}
