using ECommerce.Shared.DTOS.OrderDTOs;
using ECommerceServiceApstarction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePresentation.Controllers
{
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //create order
        [Authorize]
        [HttpPost] 
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder( OrderDTO orderDTO)
        {
            string? email = GetEmailFromToken();
            var result = await _orderService.CreateOrderAsync(orderDTO, email);
            return HandelResult(result);

        }



      
    }
}
