﻿using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Domain.Relations
{
    public class TicketInShoppingCart : BaseEntity
    {
        public Guid TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        public Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart UserCart { get; set; }

        public int Quantity { get; set; }
    }
}
