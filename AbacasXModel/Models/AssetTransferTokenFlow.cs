using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    class AssetTransferTokenFlow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssetTokenFlowId { get; set; }

        // Foreign Key to Token Account
        public int AccountId { get; set; }

        // Foreign Key to Token
        [MaxLength(35)]
        public string TokenId { get; set; }

        // Foreign Key to BaseAssetTransfer
        public int AssetTransferId { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }


        public virtual Token Token { get; set; }
        public virtual TokenAccount TokenAccount { get; set; }
        public virtual AssetTransfer AssetTransfer { get; set; }
    }
}
