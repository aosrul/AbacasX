using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    public class ClientAuthentication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientAuthenticationId { get; set; }

        public int ClientId { get; set; }

        public AuthenticationTypeEnum AuthenticationType { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual Client User { get; set; }
    }

    public enum AuthenticationTypeEnum
    {
        Password,
        Text,
        Email
    }
}
