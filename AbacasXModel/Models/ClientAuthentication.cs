﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel.Models
{
    class ClientAuthentication
    {
        [Key]
        public int ClientId { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual Client User { get; set; }
    }
}
