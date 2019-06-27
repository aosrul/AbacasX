using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbacasX.models
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public OrderData Order { get; set; }
        public AssetDepositData Deposit { get; set; }
        public AssetWithdrawalData Withdrawal { get; set; }
        public ModelStateDictionary ModelState { get; set; }
        public string Guid { get; set; }
        public decimal TokenBalance { get; set; }
    }
}
