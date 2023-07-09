using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.Identity;
using TicketShop.Domain.Relations;

namespace TicketShop.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public virtual TicketShopApplicationUser Owner { get; set; }

        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }

    }
}
