using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.Models
{
    public class ClientAccountHolder
    {
        [Key, Column(Order = 1)]
        public int ClientId { get; set; }

        [Key, Column(Order = 2)]
        public int ClientAccountId { get; set; }

        public AccountHolderTypeEnum AccountHolderType { get; set; }


        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual Client Client { get; set; }
        public virtual ClientAccount ClientAccount { get; set; }
    }

    public enum AccountHolderTypeEnum
    {
        Individual,
        Joint,
    };
}
