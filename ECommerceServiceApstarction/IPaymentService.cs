using ECommerce.Shared.DTOS.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceServiceApstarction
{
    public interface IPaymentService
    {

        //servce [BasketId]=>Basket DTO
        Task<BasketDTO?> CreateOrUpdatePaymentIntentAsync(string basketId);
    }
}
