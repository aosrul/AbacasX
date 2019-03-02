using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AbacasX.UI.models
{
    public class DepositNotification
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
        public DateTime NotificationDateTime { get; set; }

        public DepositNotificationStatusEnum DepositNotificationStatus { get; set; }
    }

    public enum DepositNotificationStatusEnum
    {
        Sent,
        Canceled,
    }
}
