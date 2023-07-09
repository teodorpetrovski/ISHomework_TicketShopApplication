using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System;
using TicketShop.Domain.DTO;
using TicketShop.Services.Interface;
using TicketShop.Domain.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace TicketShop.Web.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        //private readonly ILogger<TicketController> _logger;

        public TicketController(ILogger<TicketController> logger, ITicketService ticketService)
        {
            //_logger = logger;
            _ticketService = ticketService;
        }

        // GET: Products
        public IActionResult Index(DateTime? date)
        {
        // _logger.LogInformation("User Request -> Get All products!");
            if(date == null)
            return View(this._ticketService.GetAllTickets());

            return View(this._ticketService.GetAllTicketsByDate(date));
        }

        // GET: Products/Details/5
        public IActionResult Details(Guid? id)
        {
            //_logger.LogInformation("User Request -> Get Details For Product");
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetDetailsForTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
           // _logger.LogInformation("User Request -> Get create form for Product!");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,MovieName,MovieCover,MovieDescription,TicketPrice,MovieRating,Genre,ValidFor")] Ticket ticket)
        {
           // _logger.LogInformation("User Request -> Inser Product in DataBase!");
            if (ModelState.IsValid)
            {
                ticket.Id = Guid.NewGuid();
                this._ticketService.CreateNewTicket(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(Guid? id)
        {
            //_logger.LogInformation("User Request -> Get edit form for Product!");
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetDetailsForTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,MovieName,MovieCover,MovieDescription,TicketPrice,MovieRating,Genre,ValidFor")] Ticket ticket)
        {
           // _logger.LogInformation("User Request -> Update Product in DataBase!");

            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._ticketService.UpdateExistingTicket(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(Guid? id)
        {
           // _logger.LogInformation("User Request -> Get delete form for Product!");

            if (id == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetDetailsForTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            //_logger.LogInformation("User Request -> Delete Product in DataBase!");

            this._ticketService.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult AddTicketToCart(Guid id)
        {
            var result = this._ticketService.GetShoppingCartInfo(id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTicketToCart(AddToShoppingCartDto model)
        {

            //_logger.LogInformation("User Request -> Add Product in ShoppingCart and save changes in database!");


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._ticketService.AddToShoppingCart(model, userId);

            if (result)
            {
                return RedirectToAction("Index", "Ticket");
            }
            return View(model);
        }
        private bool ProductExists(Guid id)
        {
            return this._ticketService.GetDetailsForTicket(id) != null;
        }
    }
}
