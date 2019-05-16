using AbacasX.Model.Models;
using AbacasX.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AbacasWebX.Rate.Contracts
{
    [DataContract]
    public class TokenPairRateData
    {
        [DataMember]
        public string Token1Id { get; set; }
        [DataMember]
        public string Token2Id { get; set; }
        [DataMember]
        public TokenPairRateTermsEnum RateTerms { get; set; }
        [DataMember]
        public double BidRate { get; set; }
        [DataMember]
        public double AskRate { get; set; }

        [DataMember]
        public double Token1BidRate { get; set; }
        [DataMember]
        public double Token1AskRate { get; set; }
        [DataMember]
        public TokenRateTermsEnum Token1RateTerms { get; set; }

        [DataMember]
        public double Token2BidRate { get; set; }
        [DataMember]
        public double Token2AskRate { get; set; }
        [DataMember]
        public TokenRateTermsEnum Token2RateTerms { get; set; }

        [DataMember]
        public string Currency1 { get; set; }
        [DataMember]
        public double Currency1BidRate { get; set; }
        [DataMember]
        public double Currency1AskRate { get; set; }
        [DataMember]
        public RateTermsEnum Currency1RateTerms { get; set; }

        [DataMember]
        public string Currency2 { get; set; }
        [DataMember]
        public double Currency2BidRate { get; set; }
        [DataMember]
        public double Currency2AskRate { get; set; }
        [DataMember]
        public RateTermsEnum Currency2RateTerms { get; set; }

        [DataMember]
        public double CurrencyPairBidRate { get; set; }

        [DataMember]
        public double CurrencyPairAskRate { get; set; }

        [DataMember]
        public CurrencyPairRateTermsEnum CurrencyPairRateTerms { get; set; }

        [DataMember]
        public DateTime LastUpdate { get; set; }

    }
}