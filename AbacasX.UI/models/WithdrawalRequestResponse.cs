using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AbacasX.UI.models
{
    public class WithdrawalRequestResponse
    {
        [MaxLength(50)]
        [Required]
        public string WithdrawalRequestId;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string AssetId { get; set; }

        [Required]
        public DateTime WithdrawalRequestDateTime { get; set; }


        public string PayeeName { get; set; }
        public string PayeeAddress { get; set; }

        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingNumber { get; set; }
        public string BankAddress { get; set; }
        public string BankPhoneNumber { get; set; }

        public WithdrawalRequestResponseStatusEnum WithdrawalRequestStatus { get; set; }

        public string ResponseDescription { get; set; }
    }

    public enum WithdrawalRequestResponseStatusEnum
    {
        Received,
        Confirmed,
        Completed,
        Failed
    }
}
