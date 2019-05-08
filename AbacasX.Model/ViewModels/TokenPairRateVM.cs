using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.ViewModels
{
    public class TokenPairRateVM
    {
        public string Token1Id { get; set; }
        public string Token2Id { get; set; }
        public TokenPairRateTermsEnum RateTerms { get; set; }

        public double BidRate { get; set; }
        public double AskRate { get; set; }

        public double Token1BidRate { get; set; }
        public double Token1AskRate { get; set; }
        public TokenRateTermsEnum Token1RateTerms { get; set; }

        public double Token2BidRate { get; set; }
        public double Token2AskRate { get; set; }
        public TokenRateTermsEnum Token2RateTerms { get; set; }

        public string Currency1 { get; set; }
        public double Currency1BidRate { get; set; }
        public double Currency1AskRate { get; set; }
        public RateTermsEnum Currency1RateTerms { get; set; }

        public string Currency2 { get; set; }
        public double Currency2BidRate { get; set; }
        public double Currency2AskRate { get; set; }
        public RateTermsEnum Currency2RateTerms { get; set; }

        public DateTime LastUpdate { get; set; }


        public TokenPairRateVM(Token Token1Record, Token Token2Record, TokenPairRateTermsEnum TokenPairRateTerms)
        {
            Token1Id = Token1Record.TokenId;
            Token2Id = Token2Record.TokenId;
            RateTerms = TokenPairRateTerms;
        }
    }


    public enum TokenPairRateTermsEnum
    {
        Token1PerToken2,
        Token2PerToken1
    }
}
