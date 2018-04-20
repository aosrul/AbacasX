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
    /// This is the Token Flow (Tokens) either created, or liquidated based on either a deposit
    /// or withdrawal of the base asset. 
    /// </summary>
    public class TokenConversion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TokenConversionId { get; set; }

        // A Transfer creates the conversion
        public int AssetTransferId { get; set; }
        
        [MaxLength(35)]
        public string TokenId { get; set; }

        public int TokenAccountId { get; set; }
       
        public decimal TokenAmount { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual TokenAccount TokenAccount { get; set; }
        public virtual AssetTransfer AssetTransfer { get; set; }
    }
}
