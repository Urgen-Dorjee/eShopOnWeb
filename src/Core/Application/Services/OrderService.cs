﻿using Ardalis.GuardClauses;
using eWebShop.Application.Contracts;
using eWebShop.Application.Entities;
using eWebShop.Application.Entities.BasketAggregate;
using eWebShop.Application.Entities.OrderAggregate;
using eWebShop.Application.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eWebShop.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAsyncRepository<Order> _orderRepository;
        private readonly IAsyncRepository<Basket> _basketRepository;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public OrderService(IAsyncRepository<Basket> basketRepository, IAsyncRepository<CatalogItem> itemRepository,
            IAsyncRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _itemRepository = itemRepository;
        }
        public async Task CreateOrderAsync(int basketId, Address shippingAddress)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);
            Guard.Against.NullBasket(basketId, basket);
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var catalogItem = await _itemRepository.GetByIdAsync(item.CatalogItemId);
                var itemOrdered = new CatalogItemOrdered(catalogItem.Id, catalogItem.Name, catalogItem.PictureUri);
                var orderItem = new OrderItem(itemOrdered, item.UnitPrice, item.Quantity);
                items.Add(orderItem);
            }
            var order = new Order(basket.BuyerId, shippingAddress, items);
            await _orderRepository.AddAsync(order);
        }
    }
}
