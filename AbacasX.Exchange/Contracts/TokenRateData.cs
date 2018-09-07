using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Exchange.Contracts
{
    [DataContract]
    public class TokenRateData
    {
        [DataMember]
        public string TokenId;

        [DataMember]
        public decimal TokenRate;

        [DataMember]
        public string TokenRateIn;
    }
}
