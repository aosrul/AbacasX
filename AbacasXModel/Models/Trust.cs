using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    public class Trust
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrustId { get; set; }

        [MaxLength(40)]
        [Required]
        public string TrustName { get; set; }


        [Timestamp]
        public Byte[] Timestamp { get; set; }
    }
}
