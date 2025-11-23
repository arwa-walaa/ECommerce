using ECommerce.Shared.DTOS.BasketDTOs;
using ECommerceServiceApstarction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePresentation.Controllers
{
 
    public class BasketController : ApiBaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;

        }
        [HttpGet()]
        public async Task<ActionResult<BasketDTO>> GetBasket(string basketId)
        {
            var basket = await _basketService.GetBasketAsync(basketId);
            //if (basket == null)
            //{
            //    return NotFound();
            //}
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDTO >> CreateOrUpdateBasket( BasketDTO basket)
        {
            var createdOrUpdatedBasket = await _basketService.CreateOrUpdateBasketAsync(basket);
            return Ok(createdOrUpdatedBasket);
        }
        [HttpDelete("{basketId}")]
        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
            var isDeleted = await _basketService.DeleteBasketAsync(basketId);
            //if (!isDeleted)
            //{
            //    return NotFound();
            //}
            return Ok(isDeleted);
        }
    }
}
