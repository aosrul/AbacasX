using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.ViewModels
{
    public class CurrencyPairRateVM
    {

        #region Definitions

        public string Currency1 { get; set; }
        public double Currency1BidRate { get; set; }
        public double Curency1AskRate { get; set; }
        public RateTermsEnum Currency1RateTerms { get; set; }

        public string Currency2 { get; set; }
        public double Currency2BidRate { get; set; }
        public double Currency2AskRate { get; set; }
        public RateTermsEnum Currency2RateTerms { get; set; }


        public double BidRate { get; set; }
        public double AskRate { get; set; }
        public CurrencyPairRateTermsEnum RateTerms { get; set; }

        public RateChangeEnum BidRateChangeType { get; set; }
        public RateChangeEnum AskRateChangeType { get; set; }
        public DateTime LastUpdate { get; set; }

        #endregion

        public CurrencyPairRateVM(AssetRateVM Asset1, AssetRateVM Asset2, CurrencyPairRateTermsEnum CurrencyPairRateTerms)
        {
            Currency1 = Asset1.AssetId;
            Currency1RateTerms = Asset1.RateTerms;

            Currency2 = Asset2.AssetId;
            Currency2RateTerms = Asset2.RateTerms;

            RateTerms = CurrencyPairRateTerms;

            BidRateChangeType = RateChangeEnum.NoChange;
            AskRateChangeType = RateChangeEnum.NoChange;
        }
    }

    public enum CurrencyPairRateTermsEnum
    {
        Currency1PerCurrency2,
        Currency2PerCurrency1
    }
}
