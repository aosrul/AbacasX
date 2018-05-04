using AbacasXData.Contracts;
using AbacasXModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXContracts
{
    public interface IAssetRepository : IRepository<Asset>
    {
        /// <summary>
        /// Get the Tokens for which this asset is the base
        /// </summary>
        /// <returns></returns>
        IQueryable<Token> GetTokens();
    }
}
