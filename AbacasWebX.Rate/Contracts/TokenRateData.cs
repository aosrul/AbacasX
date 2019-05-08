using AbacasX.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AbacasWebX.Rate.Contracts
{
    [DataContract]
    public class TokenRateData
    {
        [DataMember]
        public string TokenId { get; set; }

        [DataMember]
        public string PriceCurrency { get; set; }

        [DataMember]
        public TokenRateTermsEnum RateTerms { get; set; }

        [DataMember]
        public double AssetBidRate { get; set; }

        [DataMember]
        public double AssetAskRate { get; set; }

        [DataMember]
        public double Multiplier { get; set; }

        [DataMember]
        public double BidRate { get; set; }

        [DataMember]
        public double AskRate { get; set; }

        [DataMember]
        public RateChangeEnum BidRateChangeType { get; set; }

        [DataMember]
        public RateChangeEnum AskRateChangeType { get; set; }

        [DataMember]
        public DateTime LastUpdate { get; set; }
    }
}