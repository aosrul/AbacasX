using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.DataContracts
{
    [DataContract]
    public class OrderData
    {
        [DataMember]
        public int OrderId;

        [DataMember]
        public int OrderLegId;

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
        public decimal Token1AmountFilled;

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
        public OrderLegFillStatusEnum OrderFillStatus;

        [DataMember]
        public decimal PriceFilled;
    }
}
