using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbacasX.infrastructure;
using AbacasX.models;
using AbacasX.UI.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace AbacasX.UI.Apis
{
    [Route("api/custodian")]
    public class CustodianApiController : Controller
    {
        ILogger _logger;

        public CustodianApiController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(nameof(CustodianApiController));
        }

        [HttpPost]
        [NoCache]
        [Route("depositNotification")]
        [ProducesResponseType(typeof(DepositNotification), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public ActionResult DepositNotification([FromBody] DepositNotification depositNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            try
            {

                if (depositNotification == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                else
                {
                    return Ok(new ApiResponse { Status = true });
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }


        [HttpPost]
        [NoCache]
        [Route("depositNotificationResponse")]
        [ProducesResponseType(typeof(DepositNotificationResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public ActionResult DepositNotificationResponse([FromBody] DepositNotificationResponse depositNotificationResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            try
            {

                if (depositNotificationResponse == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                else
                {
                    return Ok(new ApiResponse { Status = true });
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpPost]
        [NoCache]
        [Route("withdrawalRequest")]
        [ProducesResponseType(typeof(WithdrawalRequest), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public ActionResult WithdrawalRequest([FromBody] WithdrawalRequest withdrawalRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            Task.WaitAny();

            try
            {

                if (withdrawalRequest == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                else
                {
                    return Ok(new ApiResponse { Status = true });
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpPost]
        [NoCache]
        [Route("withdrawalRequestResponse")]
        [ProducesResponseType(typeof(WithdrawalRequestResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public ActionResult WithdrawalRequestResponse([FromBody] WithdrawalRequestResponse withdrawalRequestResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            Task.WaitAny();

            try
            {

                if (withdrawalRequestResponse == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                else
                {
                    return Ok(new ApiResponse { Status = true });
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }
    }
}
