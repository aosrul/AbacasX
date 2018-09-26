using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AbacasX.UI.Apis
{
    [Route("api/[controller]")]
    public class IdentityApiController : Controller
    {
        //Get api/values
        [HttpGet, Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "TradezDigital", "DigitalBroker", "DigitalInvestor", "AbacasAdmin", "AbacasOps" };
        }
    }
}