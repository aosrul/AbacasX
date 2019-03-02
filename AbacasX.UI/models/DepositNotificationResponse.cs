using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AbacasX.UI.models
{
    public class DepositNotificationResponse
    {
        [MaxLength(50)]
        [Required]
        public string DepositNotificationId;

        [MaxLength(50)]
        [Required]
        public string AccountName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string AssetId { get; set; }

        [Required]
        public DepositNotificationResponseStatusEnum ResponseStatus { get; set; }

        public string ResponseDescription { get; set; }

        [Required]
        public DateTime ResponseDateTime { get; set; }
    }

    public enum DepositNotificationResponseStatusEnum
    {
        Received,
        Canceled,
        Failed
    }
}
