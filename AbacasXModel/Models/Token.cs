using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    class Token
    {
        [Key]
        [MaxLength(35)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TokenId { get; set; }

        [MaxLength(35)]
        [Required]
        public string Name { get; set; }
        

        [MaxLength(35)]
        [Required]
        public string Denomination { get; set; }

        [Required]
        public int Multiplier { get; set; }

      
        public TokenPriceTermsEnum PriceTerms { get; set; }

        public TokenStatusEnum TokenStatus { get; set; }

        public decimal Balance { get; set; }
        public decimal AvailableBalance { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        
        public virtual Asset BaseAsset { get; set; }
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
