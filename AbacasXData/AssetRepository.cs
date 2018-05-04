using AbacasXContracts;
using AbacasXData.Contracts;
using AbacasXModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXData
{
    public class AssetRepository : EFRepository<Asset>, IAssetRepository
    {
        public AssetRepository(DbContext context) : base(context) { }

        public IQueryable<Token> GetTokens()
        {
            return null;
        }
    }
}
