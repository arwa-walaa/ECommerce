using ECommerce.Shared.CommenResults;
using ECommerce.Shared.DTOS.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceServiceApstarction
{
    public interface IOrderService
    {
        //create order ()
        //orderDTO [A]
        Task<Result<OrderToReturnDTO>> CreateOrderAsync( OrderDTO orderDTO , string Email);
    }
}
