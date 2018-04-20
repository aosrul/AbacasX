using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    public class AssetAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssetAccountId { get; set; }

        // Foreign Keys
        [MaxLength(35)]
        public string AssetId { get; set; }

        public int CustodianId { get; set; }


        // Custodian Account Number
        [MaxLength(40)]
        [Required]
        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }
        public decimal AvailableBalance { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual Custodian Custodian { get; set; }
    }
}
