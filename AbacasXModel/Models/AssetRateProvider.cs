using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    class AssetRateProvider
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RateProviderId { get; set; }
        
        [MaxLength(40)]
        [Required]
        public string ProviderName { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }
    }
}
