using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    public class ClientKYC
    {
        [Key]
        public int ClientRegistrationId { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual ClientRegistration ClientRegistration { get; set; }
    }
}
