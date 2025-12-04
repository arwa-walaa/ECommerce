using AutoMapper;
using ECommerce.Shared.DTOS.BasketDTOs;
using ECommerceDomain.Contarcts;
using ECommerceDomain.Entities.BasketModule;
using ECommerceService.Exceptions;
using ECommerceServiceApstarction;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepo basketRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public PaymentService(IBasketRepo basketRepo
            , IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration configuration
            )
        {
            this.basketRepo = basketRepo;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        public async Task<BasketDTO?> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
      
            //1- Configration strip2

            //download strip nuget package
            StripeConfiguration.ApiKey = configuration["StripeSetting:SecretKey"]!;
            //2- Get Basket from repo
            var basket = await basketRepo.GetBasketAsync(basketId);
            if (basket == null)  throw new BasketNotFoundException(basketId);



            //3-Amount
            var Product = unitOfWork.GetRepo<ECommerceDomain.Entities.ProductModule.Product, int>();
            foreach (var item in basket.Items)
            {
                var product = await Product.GetByIdAsync(item.Id);
                if (product == null) throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
                
            }
            var DeliveryMethod = await unitOfWork.GetRepo<ECommerceDomain.Entities.OrderModule.DeliveryMethod, int>()
                .GetByIdAsync(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = DeliveryMethod.Price;
            var amount = (long)((basket.Items.Sum(i => i.Quantity * i.Price) + basket.ShippingPrice.Value) * 100);
            //4- Create or update payment intent
            var PaymentService = new PaymentIntentService();
            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = [ "card" ],
                };
                var paymentIntent = await PaymentService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                };
                await PaymentService.UpdateAsync(basket.PaymentIntentId, options);
            }
            //5- Update basket in repo
            await basketRepo.CreateOrUpdateBasketAsync(basket);
           
            return mapper.Map<CustomerBasket,BasketDTO>(basket);





        }
    }
}
