using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.Models
{
    /// <summary>
    /// This is the Token Flow (Tokens) either created, or liquidated based on either a deposit
    /// or withdrawal of the base asset. In the event of a deposit, the reference code of the deposit notification
    /// will link the incoming flow with the asset transfer record and ultimately with a token conversion record representing
    /// the minting of the new tokens in exchange for the base asset.
    /// 
    /// In the event of a withdrawal, the referencecode is a link with the outgoing payment request of the base asset and links
    /// the withdrawal AssetTransfer with this token conversion.
    /// </summary>
    public class TokenConversion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TokenConversionId { get; set; }

        // A Transfer creates the conversion
        public int AssetTransferId { get; set; }

        // Conversion direction
        public ConversionTypeEnum ConversionType;

        // Processing Status
        public ConversionStatusEnum ConversionStatus;
        
        [MaxLength(35)]
        public string TokenId { get; set; }

        public int TokenAccountId { get; set; }
       
        public decimal TokenAmount { get; set; }

        
        [MaxLength(50)]
        [Required]
        public string ReferenceCode { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual TokenAccount TokenAccount { get; set; }
        public virtual AssetTransfer AssetTransfer { get; set; }
    }

    public enum ConversionTypeEnum
    {
        AssetToToken,
        TokenToAsset
    }

    public enum ConversionStatusEnum
    {
        Requested,
        InProgress,
        Completed,
        Canceled,
        Failed
    }
}
