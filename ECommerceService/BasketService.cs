using AutoMapper;
using ECommerce.Shared.DTOS.BasketDTOs;
using ECommerceDomain.Contarcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService
{
    public class BasketService : ECommerceServiceApstarction.IBasketService
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepo basketRepo, IMapper mapper)
        {
           _basketRepo = basketRepo;
           _mapper = mapper;
        }
        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket)
        {

            var CustomerBasket = _mapper.Map<ECommerceDomain.Entities.BasketModule.CustomerBasket>(basket);
            var createdOrUpdatedBasket =await _basketRepo.CreateOrUpdateBasketAsync(CustomerBasket);
            return _mapper.Map<BasketDTO>(createdOrUpdatedBasket);

        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _basketRepo.DeleteBasketAsync(basketId);


        }

        public async Task<BasketDTO> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);
            return _mapper.Map<BasketDTO>(basket);
        }
    }
}
