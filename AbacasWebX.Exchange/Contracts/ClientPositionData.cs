using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AbacasWebX.Exchange.Contracts
{
    [DataContract]
    public class ClientPositionData
    {
        [DataMember]
        public string TokenId;

        [DataMember]
        public decimal TokenAmount;

        [DataMember]
        public decimal TokenRate;

        [DataMember]
        public string TokenRateIn;

        [DataMember]
        public decimal TokenValue;
    }
}