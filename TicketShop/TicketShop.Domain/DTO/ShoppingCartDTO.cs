using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.Relations;

namespace TicketShop.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketInShoppingCart> Tickets { get; set; }

        public double TotalPrice { get; set; }
    }
}
