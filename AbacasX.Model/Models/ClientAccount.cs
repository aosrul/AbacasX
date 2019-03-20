using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.Models
{
    public class ClientAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientAccountId { get; set; }

        // Primary Account Holder
        public int ClientId { get; set; }

        [MaxLength(40)]
        [Required]
        public string AccountName { get; set; }

        [MaxLength(30)]
        [Required]
        public string AccountNumber { get; set; }
        public AccountStatusType AccountStatus { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual Client Client { get; set; }
    }
}
