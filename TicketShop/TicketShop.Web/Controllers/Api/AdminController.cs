using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;
using TicketShop.Domain.DTO;
using TicketShop.Domain.Identity;
using TicketShop.Repository.Interface;
using TicketShop.Services.Interface;

namespace TicketShop.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly UserManager<TicketShopApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        
        public AdminController(ITicketService ticketService, UserManager<TicketShopApplicationUser> userManager, Repository.Interface.IUserRepository userRepository)
        {
            _ticketService = ticketService;
            _userManager = userManager;
            _userRepository = userRepository;
            
        }


        [HttpGet("[action]")]
        public List<Ticket> GetTickets(string? genre)
        {
            if(genre == null)
            return this._ticketService.GetAllTickets();


            return this._ticketService.GetAllTicketsByGenre(genre);
        }



        [HttpGet("[action]")]
        public List<AdminRegistrationDto> GetUsers()
        {

            var users= this._userRepository.GetAll();

            return users.Select(user => new AdminRegistrationDto 
            { Email=user.Email.ToString(),
              Role=user.Role
            }).ToList();
        }


        [HttpPost("[action]")]
        public async Task<bool> ChangeRoleAsync(AdminRegistrationDto model)
        {
            var userCheck = _userManager.FindByEmailAsync(model.Email).Result;
            if(userCheck==null)
                return false;
            userCheck.Role = model.Role;
            var result = await _userManager.UpdateAsync(userCheck);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        [HttpPost("[action]")]
        public bool ImportAllUsers(List<AdminRegistrationDto> model)
        {
            bool status = true;
            foreach (var item in model)
            {
                var userCheck = _userManager.FindByEmailAsync(item.Email).Result;
                if (userCheck == null)
                {
                    var user = new TicketShopApplicationUser
                    {
                        
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Role = item.Role,
                        UserCart = new ShoppingCart()
                    };
                    var result = _userManager.CreateAsync(user, item.Password).Result;

                    status = status & result.Succeeded;
                }
                else
                {
                    continue;
                }
            }

            return status;
        }

        


    }
}
