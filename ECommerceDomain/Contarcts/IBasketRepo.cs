using ECommerceDomain.Entities.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomain.Contarcts
{
    public interface IBasketRepo
    {
        Task<CustomerBasket?> GetBasketAsync( string basketId);
        Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket basket,TimeSpan timeToLive=default);
        Task<bool> DeleteBasketAsync( string basketId);
    }
}
