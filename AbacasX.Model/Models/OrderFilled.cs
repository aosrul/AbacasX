using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.Models
{
    public class OrderFilled
    {
        [Key, Column(Order = 0)]
        public int OrderLegId { get; set; }

        [Key, Column(Order = 1)]
        public int TransactionId { get; set; }

        public DateTime FilledDateTime { get; set; }

        [Required]
        public string Token1Id { get; set; }
        public decimal Token1Amount { get; set; }

        [Required]
        public string Token2Id { get; set; }
        public decimal Token2Amount { get; set; }

        public decimal PriceFilled { get; set; }
        public OrderPriceTermsEnum OrderPriceTerms { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual OrderLeg OrderLeg { get; set; }
    }
}
