using AbacasX.Model.ViewModels;
using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AbacasX.Rate.Contracts
{
    [DataContract]
    public class CurrencyPairRateData
    {
        [DataMember]
        public string Currency1 { get; set; }

        [DataMember]
        public RateTermsEnum Currency1RateTerms { get; set; }

        [DataMember]
        public double Currency1BidRate { get; set; }

        [DataMember]
        public double Currency1AskRate { get; set; }

        [DataMember]
        public string Currency2 { get; set; }

        [DataMember]
        public RateTermsEnum Currency2RateTerms { get; set; }

        [DataMember]
        public double Currency2BidRate { get; set; }

        [DataMember]
        public double Currency2AskRate { get; set; }

        [DataMember]
        public double BidRate { get; set; }

        [DataMember]
        public double AskRate { get; set; }

        [DataMember]
        public CurrencyPairRateTermsEnum RateTerms { get; set; }

        [DataMember]
        public RateChangeEnum BidRateChangeType { get; set; }

        [DataMember]
        public RateChangeEnum AskRateChangeType { get; set; }

        [DataMember]
        public DateTime LastUpdate { get; set; }
    }
}
