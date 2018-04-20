using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbacasXModel.Models
{
   
    public class TokenAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TokenAccountId { get; set; }

        public int ClientAccountId { get; set; }

        [MaxLength(35)]
        public string TokenId { get; set; }

        [MaxLength(40)]
        [Required]
        public string AccountName { get; set; }

        [MaxLength(30)]
        [Required]
        public string AccountNumber { get; set; }
        public AccountStatusType AccountStatus { get; set; }

        public decimal Balance { get; set; }
        public decimal AvailableBalance { get; set; }


        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual Token Token { get; set; }
        public virtual ClientAccount ClientAccount { get; set; }
    }

    public enum AccountStatusType
    {
        Pending,
        Active,
        Suspended,
        Closed
    }
}
