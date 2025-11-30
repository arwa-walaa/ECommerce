using AutoMapper;
using ECommerce.Shared.CommenResults;
using ECommerce.Shared.DTOS.OrderDTOs;
using ECommerceDomain.Contarcts;
using ECommerceDomain.Entities.BasketModule;
using ECommerceDomain.Entities.OrderModule;
using ECommerceDomain.Entities.ProductModule;
using ECommerceServiceApstarction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepo _basketRepo;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork , IBasketRepo basketRepo)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
           _basketRepo = basketRepo;
        }

        public async Task<Result<OrderToReturnDTO>> CreateOrderAsync(OrderDTO orderDTO, string Email)
        {
            var orderAddress = _mapper.Map<OrderAddress>(orderDTO.Address);
            var basket = await  _basketRepo.GetBasketAsync(orderDTO.BasketId);
            if (basket == null) return Error.NotFound("Basket not found!");
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepo<Product, int>().GetByIdAsync(item.Id);
                if (product == null) return Error.NotFound($"Product with id {item.Id} not found!");
                
                orderItems.Add(CreateOrderItem(item, product));
            }

            var DeliveryMethod = await _unitOfWork.GetRepo<DeliveryMethod, int>().GetByIdAsync(orderDTO.DeliveryMethodId);
            if (DeliveryMethod == null) return Error.NotFound($"Delivery Method with id {orderDTO.DeliveryMethodId} not found!");

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            var order = new Order()
            {
                UserEmail = Email,
                Address = orderAddress,
                DeliveryMethod = DeliveryMethod,
                Items = orderItems,
                Subtotal = subtotal
            };
            await _unitOfWork.GetRepo<Order, Guid>().AddAsync(order);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result <= 0) return Error.Failure("Failed to create order!");
            return _mapper.Map<OrderToReturnDTO>(order);


        }

        private static OrderItem CreateOrderItem(BasketItem item, Product product)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrdered
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    PictureUrl = product.PictureUrl
                },
                Price = product.Price,
                Quantity = item.Quantity


            };
        }
    }
}
