﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXModel
{

    class TokenAccountHolder
    {
        [Key, Column(Order=1)]
        public int UserId { get; set; }

        [Key, Column(Order = 2)]
        public int AccountId { get; set; }
        public AccountHolderTypeEnum AccountHolderType { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual Client User { get; set; }
        public virtual TokenAccount TokenAccount { get; set; }
    }


    public enum AccountHolderTypeEnum
    {
        Individual,
        Joint,
    };
}
