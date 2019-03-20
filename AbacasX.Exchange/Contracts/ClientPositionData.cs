using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace AbacasX.Exchange.Contracts
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
