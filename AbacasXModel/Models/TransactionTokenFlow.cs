using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    class TransactionTokenFlow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TradeTokenFlowId { get; set; }

        public int TransactionId { get; set; }
        public string TokenId { get; set; }
        public TokenFlowTypeEnum TokenFlowType { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountOffset { get; set; } // How much has been offset for G/L purposes.

        public int TokenAccountId { get; set; }

        // Trader to whom the flow is attributed.
        public int ClientId { get; set; }

        public DateTime FlowDateTime { get; set; }
        public DateTime SettlementDateTime { get; set; }
        public TokenFlowProcessingStatusEnum TokenFlowProcessingStatus { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual Transaction Transaction { get; set; }
    }

    
    public enum TokenFlowProcessingStatusEnum
    {
        Pending,
        InProgress,
        Processed,
        Canceled
    }

    public enum TokenFlowTypeEnum
    {
        Credit,
        Debit
    }
}
