using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    public class Asset
    {
        [Key]
        [MaxLength(35)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AssetId { get; set; }

        [MaxLength(50)]
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