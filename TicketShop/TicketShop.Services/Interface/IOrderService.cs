using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;
using TicketShop.Domain;

namespace TicketShop.Services.Interface
{
    public interface IOrderService
    {
        public List<Order> getAllOrders();
        public List<Order> getOrdersForUser(string userId);
        public Order getOrderDetails(Guid Id);
    }
}
