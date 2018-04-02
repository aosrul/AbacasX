using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    class Asset
    {
        [Key]
        [MaxLength(35)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AssetCode { get; set; }

        [MaxLength(35)]
        [Required]
        public string AssetName { get; set; }
        public AssetTypeEnum AssetType { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }
    }
}

public enum AssetTypeEnum
{
    Currency,
    CryptoCurrency,
    PreciousMetal,
    Stock,
}