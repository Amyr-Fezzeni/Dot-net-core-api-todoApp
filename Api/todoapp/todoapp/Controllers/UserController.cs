using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoapp.Data;
using todoapp.models;

namespace todoapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class UserController : Controller
    {
        private readonly CardDbContext cardDbContext;
        public UserController(CardDbContext cardDbContext)
        {
            this.cardDbContext = cardDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await cardDbContext.users.ToListAsync();

            return Ok(users);
        }
        
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetUser")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await cardDbContext.users.FirstOrDefaultAsync(x => x.Id == id);
            user.cards = await cardDbContext.cards.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("User not found");
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {

            user.Id = Guid.NewGuid();
            await cardDbContext.users.AddAsync(user);
            await cardDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
    }
}
