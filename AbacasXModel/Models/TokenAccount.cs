using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbacasXModel
{
   
    class TokenAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TokenAccountId { get; set; }
       
        [MaxLength(40)]
        [Required]
        public string AccountName { get; set; }

        [MaxLength(30)]
        [Required]
        public string AccountNumber { get; set; }
        public AccountStatusType AccountStatus { get; set; }

        
        [Timestamp]
        public Byte[] Timestamp { get; set; }
    }

    public enum AccountStatusType
    {
        Pending,
        Active,
        Suspended,
        Closed
    }
}
