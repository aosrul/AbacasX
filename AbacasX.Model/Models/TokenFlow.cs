using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.Models
{
    public class TokenFlow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TokenFlowId { get; set; }

        public int TradeId { get; set; }
        public string TokenId { get; set; }
        public TokenFlowTypeEnum TokenFlowType { get; set; }
        public decimal Amount { get; set; }

        public int TokenAccountId { get; set; }

        // Trader to whom the flow is attributed.
        public int ClientId { get; set; }

        public DateTime FlowDateTime { get; set; }
        public DateTime SettlementDateTime { get; set; }
        public TokenFlowProcessingStatusEnum TokenFlowProcessingStatus { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual TokenTrade TokenTrade { get; set; }
        public virtual TokenAccount TokenAccount { get; set; }
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
