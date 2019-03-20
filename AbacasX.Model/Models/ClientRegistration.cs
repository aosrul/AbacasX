using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.Models
{
   
    public class ClientRegistration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientRegistrationId { get; set; }

        public int ClientId { get; set; }

        public RegistrationStatusEnum RegistrationStatus { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual Client Client { get; set; }
    }

    public enum RegistrationStatusEnum
    {
        Pending,
        InProgress,
        Completed,
        Canceled,
        Failed
    }
}
