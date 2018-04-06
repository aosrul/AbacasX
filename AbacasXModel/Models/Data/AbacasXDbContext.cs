using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models.Data
{
    class AbacasXDbContext : DbContext
    {
        public AbacasXDbContext() : base("AbacasXDbContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Properties<decimal>()
            .Configure(prop => prop.HasPrecision(18, 6));
        }

        public DbSet<Asset> Asset { get; set; }
        public DbSet<AssetAccount> AssetAccounts { get; set; }
        public DbSet<AssetRate> AssetRate { get; set; }
        public DbSet<AssetTransferTokenFlow> AssetTokenFlow { get; set; }
        public DbSet<AssetTransfer> AssetTransfer { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<ClientAuthentication> ClientAuthentication { get; set; }
        public DbSet<ClientKYC> ClientKYC { get; set; }
        public DbSet<ClientLogin> ClientLogin { get; set; }
        public DbSet<ClientRegistration> ClientRegistration { get; set; }
        public DbSet<Custodian> Custodian { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderFilled> OrderFilled { get; set; }
        public DbSet<OrderFilledMatch> OrderFilledMatch { get; set; }
        public DbSet<OrderLeg> OrderLeg { get; set; }
        public DbSet<AssetRateProvider> RateProvider { get; set; }
        public DbSet<Token> Token { get; set; }
        public DbSet<TokenAccount> TokenAccount { get; set; }
        public DbSet<TokenAccountHolder> TokenAccountHolder { get; set; }
        public DbSet<TransactionTokenFlow> TransactionTokenFlow { get; set; }
        public DbSet<Trust> Trust { get; set; }

        static AbacasXDbContext()
        {
            Database.SetInitializer(new AbacasXDbInitializer());
        }
    }
}
