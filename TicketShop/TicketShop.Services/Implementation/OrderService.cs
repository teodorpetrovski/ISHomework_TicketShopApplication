using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;
using TicketShop.Domain;
using TicketShop.Repository.Interface;
using TicketShop.Services.Interface;
using System.Linq;

namespace TicketShop.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> getAllOrders()
        {
            return this._orderRepository.getAllOrders();
        }

        public Order getOrderDetails(Guid Id)
        {
            return this._orderRepository.getOrderDetails(Id);
        }

        public List<Order> getOrdersForUser(string userId)
        {
            return this._orderRepository.getAllOrders().Where(z => z.UserId == userId).ToList();
        }
    }
}
