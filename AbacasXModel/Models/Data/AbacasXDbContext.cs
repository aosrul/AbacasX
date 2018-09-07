using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models.Data
{
    public class AbacasXDbContext : DbContext
    {
        public AbacasXDbContext() : base("AbacasXDbContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Properties<decimal>()
            .Configure(prop => prop.HasPrecision(18, 6));

            modelBuilder.Configurations.Add(new TokenConfiguration());
            /*
            modelBuilder.Entity<Token>()
                .HasRequired(n => n.Custodian)
                .WithMany()
                .HasForeignKey(n => n.CustodianId)
                .WillCascadeOnDelete(false);
                */

            modelBuilder.Entity<TokenAccount>()
                .HasRequired(n => n.Token)
                .WithMany()
                .HasForeignKey(n => n.TokenId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrustAccount>()
                 .HasRequired(n => n.Token)
                 .WithMany()
                 .HasForeignKey(n => n.TokenId)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrustAccount>()
                 .HasRequired(n => n.AssetAccount)
                 .WithMany()
                 .HasForeignKey(n => n.AssetAccountId)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrustAccount>()
                 .HasRequired(n => n.Trust)
                 .WithMany()
                 .HasForeignKey(n => n.TrustId)
                 .WillCascadeOnDelete(false);


            modelBuilder.Entity<ClientAccountHolder>()
                .HasRequired(n => n.ClientAccount)
                .WithMany()
                .HasForeignKey(n => n.ClientAccountId)
                .WillCascadeOnDelete(false);

        }

        public DbSet<Asset> Asset { get; set; }
        public DbSet<AssetAccount> AssetAccount { get; set; }
        public DbSet<AssetRate> AssetRate { get; set; }
        public DbSet<AssetRateProvider> AssetRateProvider { get; set; }
        public DbSet<AssetTransfer> AssetTransfer { get; set; }

        public DbSet<BlockChainTokenFlow> BlockChainTokenFlow { get; set; }

        public DbSet<Client> Client { get; set; }
        public DbSet<ClientAccount> ClientAccount { get; set; }
        public DbSet<ClientAccountHolder> ClientAccountHolder { get; set; }
        public DbSet<ClientAuthentication> ClientAuthentication { get; set; }
        public DbSet<ClientKYC> ClientKYC { get; set; }
        public DbSet<ClientLogin> ClientLogin { get; set; }
        public DbSet<ClientRegistration> ClientRegistration { get; set; }

        public DbSet<Custodian> Custodian { get; set; }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderFilled> OrderFilled { get; set; }
        public DbSet<OrderFilledMatch> OrderFilledMatch { get; set; }
        public DbSet<OrderLeg> OrderLeg { get; set; }


        public DbSet<Token> Token { get; set; }
        public DbSet<TokenAccount> TokenAccount { get; set; }
        public DbSet<TokenConversion> TokenConversion { get; set; }
        public DbSet<TokenFlow> TokenFlow { get; set; }
        public DbSet<TokenTrade> TokenTrade { get; set; }

        public DbSet<Trust> Trust { get; set; }
        public DbSet<TrustAccount> TrustAccount { get; set; }


        static AbacasXDbContext()
        {
            Database.SetInitializer(new AbacasXDbInitializer());
        }
    }
}
