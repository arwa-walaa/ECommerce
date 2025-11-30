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

        //get all orders
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetOrders()
        {
            string? email = GetEmailFromToken();
            var result = await _orderService.GetAllOrdersAsync(email);
            return HandelResult(result);
        }

        //get order by id
        [Authorize]
        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderById(Guid id)
        {
            string? email = GetEmailFromToken();
            var result = await _orderService.GetOrderByIdAsync(id, email);
            return HandelResult(result);
        }

        //get all delivery methods
       
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTO>>> GetDeliveryMethods()
        {
            var result = await _orderService.GetAllDeliveryMethodsAsync();
            return HandelResult(result);
        }



    }
}
