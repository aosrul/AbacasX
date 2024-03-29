﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.Models
{
    public class OrderLeg
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderLegId { get; set; }

        // Foreign Key to Order
        public int OrderId { get; set; }
        
        public DateTime OrderLegCreatedDateTime { get; set; }

        public OrderLegTypeEnum OrderLegType { get; set; }
        public OrderLegStatusEnum OrderLegStatus { get; set; }

        // Buy/Sell Type applies to Token1
        public OrderLegBuySellEnum BuySellType { get; set; }
        public OrderLegFillStatusEnum OrderLegFillStatus { get; set; }

        [MaxLength(35)]
        [Required]
        public string Token1Id { get; set; }
        public int Token1AccountId { get; set; }
        public decimal Token1Amount { get; set; }
        public decimal Token1AmountFilled { get; set; }

        [MaxLength(35)]
        [Required]
        public string Token2Id { get; set; }
        public decimal Token2Amount { get; set; }
        public int Token2AccountId { get; set; }

        public decimal OrderPrice { get; set; }
        public OrderPriceTermsEnum OrderPriceTerms { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }


        public virtual Order Order { get; set; }
    }


    #region Order Leg Enumerations
    public enum OrderLegFillStatusEnum
    {
        None,
        Partial,
        Full
    };

    public enum OrderLegStatusEnum
    {
        Contingent,
        Pending,
        Active,
        Suspended,
        Canceled,
        Filled,
        Expired
    }
    public enum OrderLegBuySellEnum
    {
        Buy,
        Sell
    }
    public enum OrderLegTypeEnum
    {
        Market,
        Limit,
        Stop,
        IfDone
    }
    #endregion

}
