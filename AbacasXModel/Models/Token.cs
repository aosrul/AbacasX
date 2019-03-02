using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    public class Token
    {
        [Key]
        [MaxLength(35)]
        public string TokenId { get; set; }

        public string AssetId { get; set; }
        public int AssetAccountId { get; set; }
        public int CustodianId { get; set; }
        public int TrustId { get; set; }

        [MaxLength(35)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int Multiplier { get; set; }

        public TokenStatusEnum TokenStatus { get; set; }

        public decimal Balance { get; set; }
        public decimal AvailableBalance { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }


        public virtual Asset Asset { get; set; }
        public virtual AssetAccount AssetAccount { get; set; }
        public virtual Trust Trust { get; set; }
        public virtual Custodian Custodian { get; set; }
    }
    
    public enum TokenStatusEnum
    {
        Generating,
        Active,
        Suspended,
        Canceled
    }
        
    public enum TokenPriceTermsEnum
    {
        CurrencyPerToken,
        TokenPerCurrency
    }
}
