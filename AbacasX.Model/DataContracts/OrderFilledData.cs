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
    public class OrderFilledData
    {
        [DataMember]
        public int OrderLegId { get; set; }

        [DataMember]
        public int TransactionId { get; set; }

        [DataMember]
        public DateTime FilledDateTime { get; set; }

        [DataMember]
        public string Token1Id { get; set; }

        [DataMember]
        public decimal Token1Amount { get; set; }

        [DataMember]
        public string Token2Id { get; set; }

        [DataMember]
        public decimal Token2Amount { get; set; }

        [DataMember]
        public decimal PriceFilled { get; set; }

        [DataMember]
        public OrderPriceTermsEnum OrderPriceTerms { get; set; }
    }
}
