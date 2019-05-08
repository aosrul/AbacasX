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
    public class AssetRateData
    {
        [DataMember]
        public string AssetId { get; set; }

        [DataMember]
        public string PriceCurrency { get; set; }

        [DataMember]
        public RateTermsEnum RateTerms { get; set; }

        [DataMember]
        public int RateProviderId { get; set; }

        [DataMember]
        public string RateProviderCode { get; set; }

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