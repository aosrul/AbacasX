using AbacasXModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Rate.Models
{
    public class TokenCrossRate
    {
        public TokenRate Token1Rate { get; set; }
        public TokenRate Token2Rate { get; set; }

        public double BidRate { get; set; }
        public double AskRate { get; set; }
        public TokenCrossRateTermsEnum TokenCrossRateTerms { get; set; }
    }


    public enum TokenCrossRateTermsEnum
    {
        Token1PerToken2,
        Token2PerToken1
    }
}
