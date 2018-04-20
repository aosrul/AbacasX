using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    /// <summary>
    /// An Asset Transfer is either an incoming deposit, or an outgoing withdrawal.
    /// A deposit will generate an AssetTransferTokenFlow credit to a TokenAccount
    /// A withdrawal will generate an AssetTransferTokenFlow debit from a TokenAccount
    /// A withdrawal will only be completed when the AssetTransferTokenFlow is processed.
    /// </summary>
    public class AssetTransfer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssetTransferId { get; set; }

        public int AssetAccountId { get; set; }
        public int CustodianId { get; set; }

        // A completed Transfer will have a corresponding TokenConversion Record
        public int? TokenConversionId { get; set; }

        [MaxLength(35)]
        [Required]
        public string AssetId { get; set; }
                
        public decimal Amount { get; set; }
        public TransferStatusEnum TransferStatus { get; set; }

        public TransferTypeEnum TransferType { get; set; }

        // Deposit Fields

        [MaxLength(75)]
        [Required]
        public string ForAccountOf { get; set; }

        [MaxLength(50)]
        [Required]
        public string ReferenceCode { get; set; }
        

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual AssetAccount AssetAccount { get; set; }
    }
}


public enum TransferTypeEnum
{
    Deposit,
    Withdrawal
}

public enum TransferStatusEnum
{
    Requested,
    InProgress,
    Completed,
    Canceled,
    Failed
}

