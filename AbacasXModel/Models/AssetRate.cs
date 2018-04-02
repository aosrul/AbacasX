using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    class AssetRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RateID { get; set; }

        [MaxLength(35)]
        [Required]
        public string AssetCode { get; set; }

        [MaxLength(10)]
        [Required]
        public string PriceCurrency { get; set; }

        [Required]
        public int ProviderID { get; set; }

        [MaxLength(30)]
        public string ProviderRateID { get; set; }

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
        public virtual RateProvider RateProvider { get; set; }
    }

    public enum RateTermsEnum
    {
        AssetPerCurrency,
        CurrencyPerAsset
    }

}
