using AbacasXModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Rate.Models
{
    public class CurrencyCrossRate
    {
        public AssetRate Currency1Rate { get; set; }
        public AssetRate Currency2Rate { get; set; }

        public double BidRate { get; set; }
        public double AskRate { get; set; }

        public CurrencyCrossRateTermsEnum CurrencyCrossRateTerms { get; set; }
    }

    public enum CurrencyCrossRateTermsEnum
    {
        Currency1PerCurrency2,
        Currency2PerCurrency1
    }
}

