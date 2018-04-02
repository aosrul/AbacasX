using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    class AssetTransfer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransferId { get; set; }

        public TransferStatusEnum TransferStatus {get; set;}

        public int AccountId { get; set; }

        public TransferTypeEnum TransferType { get; set; }

        public decimal Amount { get; set; }

        [MaxLength(75)]
        public string ForAccountOf { get; set; }

        [MaxLength(50)]
        public string ReferenceCode { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual AssetAccount AssetAccount { get; set; }
    }
}

public enum TransferStatusEnum
{
    InProgress,
    Completed,
    Canceled
}

public enum TransferTypeEnum
{
    TransferIn,
    TransferOut,
    AdjustmentCredit,
    AdjustmentDebit
}
