using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbacasX.infrastructure;
using AbacasX.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderManagerService;

namespace AbacasX.Apis
{
    [Route("api/orders")]
    public class OrdersApiController : Controller
    {
        IOrderService _orderService;
        ILogger _logger;

        public OrdersApiController(IOrderService orderService, ILoggerFactory loggerFactory)
        {
            _orderService = orderService;
            _logger = loggerFactory.CreateLogger(nameof(OrdersApiController));
        }


        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<OrderData>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Orders()
        {
            try
            {
                //return Ok(new OrderData[] { new OrderData { OrderId = 1, BuySellType = OrderLegBuySellEnum.Buy, ClientAccountId = 0, ClientId = 0, OrderPrice = 1, OrderPriceTerms = OrderPriceTermsEnum.Token1PerToken2, OrderType = OrderTypeEnum.Standard, Token1Id = "AAPL", Token1Amount = 1000, Token2Id = "GOOG", Token2Amount = 100 } });
                var orders = await _orderService.GetClientOrdersAsync(0);
                return Ok(orders);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpGet]
        [NoCache]
        [Route("history")]
        [ProducesResponseType(typeof(List<OrderData>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> OrderHistory()
        {
            try
            {
                //return Ok(new OrderData[] { new OrderData { OrderId = 1, BuySellType = OrderLegBuySellEnum.Buy, ClientAccountId = 0, ClientId = 0, OrderPrice = 1, OrderPriceTerms = OrderPriceTermsEnum.Token1PerToken2, OrderType = OrderTypeEnum.Standard, Token1Id = "AAPL", Token1Amount = 1000, Token2Id = "GOOG", Token2Amount = 100 } });
                var orders = await _orderService.GetClientHistoricalOrdersAsync(0);
                return Ok(orders);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpGet]
        [NoCache]
        [Route("clientPosition")]
        [ProducesResponseType(typeof(List<ClientPositionData>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> ClientPositions()
        {
            try
            {
                var clientPositions = await _orderService.GetClientPositionsAsync(0);
                return Ok(clientPositions);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpGet]
        [NoCache]
        [Route("clientBlockChainTransactions")]
        [ProducesResponseType(typeof(List<ClientPositionData>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> ClientBlockChainTransactions()
        {
            try
            {
                var clientBlockChainTransactions = await _orderService.GetClientBlockChainTransactionsAsync(0);
                return Ok(clientBlockChainTransactions);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }


        [HttpGet("{id}", Name = "GetOrderRoute")]
        [NoCache]
        [ProducesResponseType(typeof(OrderData), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Orders(int OrderId)
        {
            try
            {
                //return Ok(new OrderData[] { new OrderData { OrderId = 1, BuySellType = OrderLegBuySellEnum.Buy, ClientAccountId = 0, ClientId = 0, OrderPrice = 1, OrderPriceTerms = OrderPriceTermsEnum.Token1PerToken2, OrderType = OrderTypeEnum.Standard, Token1Id = "AAPL", Token1Amount = 1000, Token2Id = "GOOG", Token2Amount = 100 } });
                var order = await _orderService.GetClientOrdersAsync(OrderId);
                return Ok(order);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }


        [HttpPost]
        [NoCache]
        [ProducesResponseType(typeof(OrderData), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreateOrder([FromBody] OrderData order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState});
            }

            try
            {
                var newOrder = await _orderService.AddOrderAsync(order);

                if (newOrder == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                else
                {
                    return Ok(new ApiResponse { Status = true, Order = newOrder });
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