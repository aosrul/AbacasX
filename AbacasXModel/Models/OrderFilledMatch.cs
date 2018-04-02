using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    class OrderFilledMatch
    {
        [Key, Column(Order = 1)]
        public int TransactionId { get; set; }
        [Key, Column(Order = 2)]
        public int OffsetTransactionId { get; set; }

        public string Token1Id { get; set; }
        public decimal Token1Amount { get; set; }

        public string Token2Id { get; set; }
        public decimal Token2Amount { get; set; }

        public decimal PriceFilled { get; set; }
        public OrderPriceTermsEnum OrderPriceTerms { get; set; }
        public DateTime MatchedDateTime { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }
    }
}
