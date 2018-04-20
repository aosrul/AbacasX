using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    class BlockChainTokenFlow
    {
        [Key]
        public int TokenFlowId { get; set; }

        public string BlockChainId { get; set; }

        public int BlockNumber { get; set; }

        public TokenBlockChainStatusEnum  BlockChainStatus {get; set;}

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual TokenFlow TokenFlow { get; set; }
    }

    public enum TokenBlockChainStatusEnum
    {
        Pending,
        Sent,
        Confirmed,
        Failed,
        Canceled
    }
}
