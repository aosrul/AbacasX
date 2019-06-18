using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.DataContracts
{
    [DataContract]
    public class TokenDetail
    {
        [DataMember]
        public string TokenId { get; set; }

        [DataMember]
        public string AssetId { get; set; }

        [DataMember]
        public int AssetAccountId { get; set; }

        [DataMember]
        public int CustodianId { get; set; }

        [DataMember]
        public int TrustId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public double Multiplier { get; set; }

        [DataMember]
        public TokenStatusEnum TokenStatus { get; set; }

        [DataMember]
        public decimal Balance { get; set; }

        [DataMember]
        public decimal AvailableBalance { get; set; }

        [DataMember]
        public string TradingViewSymbol { get; set; }
    }
}
