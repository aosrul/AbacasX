using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.ViewModels
{
    public enum RateChangeEnum
    {
        Up,
        Down,
        NoChange
    }

    public class AssetRateVM
    {
        public string AssetId { get; set; }
        public string PriceCurrency { get; set; }
        public RateTermsEnum RateTerms { get; set; }
        public int RateProviderId { get; set; }
        public string RateProviderCode { get; set; }

        public double BidRate { get; set; }
        public double AskRate { get; set; }

        public RateChangeEnum BidRateChangeType { get; set; }
        public RateChangeEnum AskRateChangeType { get; set; }

        public DateTime LastUpdate { get; set; }

        public AssetRateVM(AssetRate AssetRateRecord)
        {
            AssetId = AssetRateRecord.AssetId;
            PriceCurrency = AssetRateRecord.PriceCurrency;
            RateTerms = AssetRateRecord.RateTerms;
            RateProviderId = AssetRateRecord.RateProviderId;
            RateProviderCode = AssetRateRecord.RateProviderCode;

            BidRate = 0.0;
            AskRate = 0.0;

            BidRateChangeType = RateChangeEnum.NoChange;
            AskRateChangeType = RateChangeEnum.NoChange;
        }
    }
}
