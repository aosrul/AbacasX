using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{

    public class TokenRate
    {
        public string TokenId { get; set; }
        public string AssetId { get; set; }
        public string PriceCurrency { get; set; }

        public double Multiplier { get; set; }
        public double BidRate { get; set; }
        public double AskRate { get; set; }

        public RateTermsEnum RateTerms { get; set; }
        public DateTime LastUpdate { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }
    }
}
