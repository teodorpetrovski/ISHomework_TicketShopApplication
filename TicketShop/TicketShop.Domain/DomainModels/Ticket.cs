using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TicketShop.Domain.Relations;

namespace TicketShop.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        [Required]
        public string MovieName { get; set; }
        [Required]
        public string MovieCover { get; set; }
        [Required]
        public string MovieDescription { get; set; }
        [Required]
        public double TicketPrice { get; set; }
        [Required]
        public double MovieRating { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public DateTime ValidFor { get; set; }


        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }

    }
}
