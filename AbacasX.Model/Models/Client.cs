using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }

        [Required]
        public ClientTypeEnum ClientType { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }


        [MaxLength(15)]
        [Required]
        public string EncryptedPassword { get; set; }
        
        [Timestamp]
        public Byte[] Timestamp { get; set; }
    }

    public enum ClientTypeEnum
    {
        Investor,
        Broker
    }
}
