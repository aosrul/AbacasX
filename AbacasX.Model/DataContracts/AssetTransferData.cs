﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Model.DataContracts
{
    [DataContract]
    public class AssetDepositData
    {
        [DataMember]
        public string referenceId;

        [DataMember]
        public string assetId;

        [DataMember]
        public decimal amount;

        [DataMember]
        public int clientId;
    }

    [DataContract]
    public class AssetWithdrawalData
    {
        [DataMember]
        public string referenceId;

        [DataMember]
        public string tokenId;

        [DataMember]
        public decimal amount;

        [DataMember]
        public int clientId;
    }

    [DataContract]
    public class AssetTransferData
    {
        [DataMember]
        public string referenceId;

        [DataMember]
        public string forAccountOf;

        [DataMember]
        public int clientId;

        [DataMember]
        public string tokenId;

        [DataMember]
        public decimal tokenAmount;

        [DataMember]
        public string assetId;

        [DataMember]
        public decimal assetAmount;

        [DataMember]
        public TransferStatusEnum transferStatus;

        [DataMember]
        public TransferTypeEnum transferType;
    }
}
