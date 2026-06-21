using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;

namespace Talabat.API.Controllers
{


    [Authorize]
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }







        // addtocart & updateItemInCart
        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<CustomerBasket?>> UpdateOrCreateBasketAsync(CustomerBasketDto customerBasketDto)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);

            if (email is null) return Unauthorized(new ApiResponse(401));

            var customerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasketDto);
            customerBasket.Id = email;

            var createdOrUpdatedBasket = await _basketRepository.UpdateOrCreateBasketAsync(customerBasket);


            if (createdOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));
            return Ok(createdOrUpdatedBasket);
        }






        [HttpDelete("items/{productId}")]
        public async Task<ActionResult<CustomerBasket>> DeleteBasketItem(int productId)
        {


            var basketId = User.FindFirstValue(ClaimTypes.Email);
            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket is null) return NotFound(new ApiResponse(404));

            if (basket.BasketItems.Any())
            {

                var basketItem = basket.BasketItems.FirstOrDefault(bI => bI.Id == productId);
                if (basketItem is not null)
                {
                    basket.BasketItems.Remove(basketItem);
                }

                basket = await _basketRepository.UpdateOrCreateBasketAsync(basket);
            }
            return Ok(basket);
        }














        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketAsync()
        {

            var basketId = User.FindFirstValue(ClaimTypes.Email);
            if (basketId is null) return Unauthorized(new ApiResponse(401));
            var basket = await _basketRepository.GetBasketAsync(basketId);
            return Ok(basket ?? new CustomerBasket(basketId));
        }






        [HttpDelete]
        public async Task DeleteBasketAsync()
        {
            var basketId = User.FindFirstValue(ClaimTypes.Email);
            await _basketRepository.DeleteBasketAsync(basketId);
        }



    }
}
