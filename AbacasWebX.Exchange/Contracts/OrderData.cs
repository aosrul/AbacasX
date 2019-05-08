using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AbacasWebX.Exchange.Contracts
{
    [DataContract]
    public class OrderData
    {
        [DataMember]
        public int OrderId;

        [DataMember]
        public int ClientId;

        [DataMember]
        public int ClientAccountId;

        [DataMember]
        public OrderLegBuySellEnum BuySellType;

        [DataMember]
        public string Token1Id;

        [DataMember]
        public decimal Token1Amount;

        [DataMember]
        public string Token2Id;

        [DataMember]
        public decimal Token2Amount;

        [DataMember]
        public decimal OrderPrice;

        [DataMember]
        public OrderPriceTermsEnum OrderPriceTerms;

        [DataMember]
        public OrderTypeEnum OrderType;

        [DataMember]
        public OrderStatusEnum OrderStatus;

        [DataMember]
        public decimal PriceFilled;
    }
}