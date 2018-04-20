using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    public class TokenTrade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TradeId { get; set; }

        public int ClientId { get; set; }       // Who entered the order
        public int OrderLegId { get; set; }    

        public TransactionSourceEnum TransactionSource { get; set; }

        [MaxLength(35)]
        public string ExternalReferenceId { get; set; }

        public TransactionBuySellTypeEnum BuySellType { get; set; }
        [MaxLength(35)]
        public string Token1ID { get; set; }
        public decimal Token1Amount { get; set; }

        [MaxLength(35)]
        public string Token2ID { get; set; }
        public decimal Token2Amount { get; set; }
        public TransactionPriceTermsEnum PriceTerms { get; set; }
        public decimal Price { get; set; }

        public DateTime TransactionDate { get; set; }
        public decimal TransactionFee { get; set; }

        public TransactionStatusEnum TransactionStatus { get; set; }
        public TransactionProcessingStatusEnum ProcessingStatus { get; set; }


        [Timestamp]
        public Byte[] Timestamp { get; set; }
    }

    public enum TransactionBuySellTypeEnum
    {
        Buy,
        Sell
    }


    public enum TransactionPriceTermsEnum
    {
        Token1PerToken2,
        Token2PerToken1
    }

    public enum TransactionSourceEnum
    {
        ManualEntry,
        OrderFilled,
    }

    
    public enum TransactionStatusEnum
    {
        Pending,
        Active,
        Canceled
    }

    public enum TransactionProcessingStatusEnum
    {
        Pending,
        Active,
        Completed,
        Canceled
    }
}

