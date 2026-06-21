using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Services.Contract;

namespace Talabat.API.Controllers
{

    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }




        [ProducesResponseType(typeof(OrdertoReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrdertoReturnDto>> CreateOrder(OrderDto orderDto)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);

            if (email is null) return Unauthorized(new ApiResponse(401));

            var order = await _orderService.CreateOrderAsync(email , orderDto.BasketId, orderDto.DeliveryMethodId);
            if (order is null) return BadRequest(new ApiResponse(400));
            var orderToReturn = _mapper.Map<OrdertoReturnDto>(order);
            return Ok(orderToReturn);
        }



        [ProducesResponseType(typeof(OrdertoReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrdertoReturnDto>?>> GetOrdersForUser(string buyerEmail)
        {
            var orders = await _orderService.GetOrdersForUserAsync(buyerEmail);
            if (orders is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<IReadOnlyList<OrdertoReturnDto>?>(orders));
        }



        [ProducesResponseType(typeof(OrdertoReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("GetOrder")]
        public async Task<ActionResult<OrdertoReturnDto>> GetOrderByIdForUser(int orderId, string buyerEmail)
        {
            var order = await _orderService.GetOrderByIdForUserAsync(orderId, buyerEmail);
            if (order is not null) return Ok(_mapper.Map<OrdertoReturnDto>(order));
            return NotFound(new ApiResponse(404));
        }



        [ProducesResponseType(typeof(DeliveryMethod), StatusCodes.Status200OK)]
        [HttpGet("deliveryMethod")] //Static Segmant 
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        => Ok(await _orderService.GetDeliveryMethodAsync());

    }
}
