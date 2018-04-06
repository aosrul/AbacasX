using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    class AssetAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssetAccountId { get; set; }

        public int CustodianId { get; set; }

        [MaxLength(40)]
        [Required]
        public string AccountNumber { get; set; }

        
        [MaxLength(35)]
        [Required]
        public string AssetCode { get; set; }
        public decimal Balance { get; set; }
        public decimal AvailableBalance { get; set; }

        
        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual Custodian Custodian { get; set; }
    }
}
