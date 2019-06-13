using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbacasWebX.Exchange.ExchangeSystem
{
    public class WatchOrder
    {
        private decimal _orderPrice;

        public OrderLeg orderLegRecord = new OrderLeg();
        public int ClientId;

        public WatchOrder(OrderLeg orderLeg)
        {
            _orderPrice = orderLeg.OrderPrice;
            orderLegRecord = orderLeg;
            if (orderLegRecord.Order != null)
                ClientId = orderLegRecord.Order.ClientId;
        }

        public OrderLegBuySellEnum BuySellType
        {
            get { return orderLegRecord.BuySellType; }
        }

        public decimal OrderPrice
        {
            get { return _orderPrice; }
            set { _orderPrice = value; }
        }

        public decimal AmountOutstanding
        {
            get
            {
                return orderLegRecord.Token1Amount - orderLegRecord.Token1AmountFilled;
            }
        }
    }
}