using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    public class AssetRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RateId { get; set; }

        [MaxLength(35)]
        [Required]
        public string AssetId { get; set; }

        [Required]
        public int RateProviderId { get; set; }


        [MaxLength(10)]
        [Required]
        public string PriceCurrency { get; set; }

      
        [MaxLength(30)]
        public string RateProviderCode { get; set; }

        // Rate Terms from the Provider
        public RateTermsEnum RateTerms { get; set; }

        public double BidRate { get; set; }
        public double AskRate { get; set; }
        public double HighRate { get; set; }
        public double LowRate { get; set; }
        public double OpenRate { get; set; }
        public double CloseRate { get; set; }

        public DateTime LastUpdate { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual AssetRateProvider RateProvider { get; set; }
    }

    public enum RateTermsEnum
    {
        AssetPerCurrency,
        CurrencyPerAsset
    }

}
