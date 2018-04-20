using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    public class TrustAccount
    {
        [Key, Column(Order = 1)]
        public string TokenId { get; set; }
        [Key, Column(Order = 2)]
        public int AssetAccountId { get; set; }
        [Key, Column(Order = 3)]
        public int TrustId { get; set; }


        public ReconciliationStatusEnum ReconciliationStatus { get; set; }
        public DateTime LastReconciliationDateTime { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual Token Token { get; set; }
        public virtual AssetAccount AssetAccount { get; set; }
        public virtual Trust Trust { get; set; }
    }

    public enum ReconciliationStatusEnum
    {
        VerificationPending,
        VerificationInprogress,
        VerificationComplete,
        VerificationError,
    }
}
