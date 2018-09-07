using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Exchange.Contracts
{
    [DataContract]
    public class BlockChainData
    {
        [DataMember]
        public int BlockNumber;

        [DataMember]
        public string Date;

        [DataMember]
        public int OrderId;

        [DataMember]
        public string PayReceive;

        [DataMember]
        public string TokenId;

        [DataMember]
        public decimal TokenAmount;

        [DataMember]
        public string Address;

        [DataMember]
        public string TransactionHash;
    }
}
