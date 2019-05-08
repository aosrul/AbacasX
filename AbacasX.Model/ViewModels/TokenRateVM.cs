using AbacasX.Model.Models;
using System;

namespace AbacasX.Model.ViewModels
{

    public enum TokenRateTermsEnum
    {
        TokenPerCurrency,
        CurrencyPerToken
    }



    /// <summary>
    /// The Token Rate View Model is the combination of properties from the Token and Asset 
    /// definitions along with a subscription to an asset rate feed. 
    /// </summary>
    public class TokenRateVM
    {
        public TokenRateVM(Token TokenRecord, Asset AssetRecord)
        {
            TokenId = TokenRecord.TokenId;
            AssetId = TokenRecord.AssetId;

            Multiplier = TokenRecord.Multiplier;

            PriceCurrency = AssetRecord.PriceCurrency;
            RateTerms = AssetRecord.PriceTerms == RateTermsEnum.AssetPerCurrency ? 
                TokenRateTermsEnum.TokenPerCurrency : 
                TokenRateTermsEnum.CurrencyPerToken;

            TokenBidRate = 0.0;
            TokenAskRate = 0.0;
            AssetBidRate = 0.0;
            AssetAskRate = 0.0;

            BidRateChangeType = RateChangeEnum.NoChange;
            AskRateChangeType = RateChangeEnum.NoChange;
        }

        
        public double AssetBidRate { get; set; }
        public double AssetAskRate { get; set; }

        public double Multiplier { get; set; }

        public string TokenId { get; set; }
        public string AssetId { get; set; }

        public string PriceCurrency { get; set; }

        public double TokenBidRate { get; set; }
        public double TokenAskRate { get; set; }

        public RateChangeEnum BidRateChangeType { get; set; }
        public RateChangeEnum AskRateChangeType { get; set; }

        public TokenRateTermsEnum RateTerms { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
