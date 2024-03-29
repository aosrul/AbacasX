﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public int TokenAccountId { get; set; }
        public int? ChildOrderId { get; set; }       
        public int ClientId { get; set; }       // Who entered the order

        public OrderTypeEnum OrderType { get; set; }
        public OrderFillTypeEnum OrderFillType { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public OrderAllocationTypeEnum OrderAllocationType { get; set; }
        public OrderFillStatusEnum OrderFillStatus { get; set; }
        public OrderExpirationTypeEnum OrderExpirationType { get; set; }
        public OrderPriceTermsEnum OrderPriceTerms { get; set; }


        public DateTime OrderExpirationDateTime { get; set; }
        public DateTime OrderCreatedDateTime { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }
    }

    public enum OrderAllocationTypeEnum
    {
        SingleFund,
        MultipleFunds
    }

    public enum OrderTypeEnum
    {
        Market,
        Limit,
        IfDone,
        OCO,
        IfDoneOCO
    }

    public enum OrderExpirationTypeEnum
    {
        GoodTillCancel,
        GoodTillDateTime,
        GoodTillClose,
    }

    public enum OrderStatusEnum
    {
        Contingent,  // Child Order awaiting outcome of Parent Order
        Pending,     // Pending Acceptance by Trading Desk
        Active,      // Active Order
        Suspended,
        Canceled,
        Filled,
        Expired
    }

    public enum OrderFillStatusEnum
    {
        None,
        Partial,
        Full
    }

    public enum OrderFillTypeEnum
    {
        AllOrNothing,
        PartialAllowed
    }

    public enum OrderPriceTermsEnum
    {
        Token1PerToken2,
        Token2PerToken1
    }
}
