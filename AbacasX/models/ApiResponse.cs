using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrderManagerService;
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
        public ModelStateDictionary ModelState { get; set; }
    }
}
