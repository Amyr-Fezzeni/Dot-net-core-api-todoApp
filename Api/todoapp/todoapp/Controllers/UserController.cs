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
        [Route("{id}")]
        [ActionName("GetUser")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            var user = await cardDbContext.users.FirstOrDefaultAsync(x => x.Id == id);


            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("User not found");
        }
        [HttpGet]
        [Route("cards/{id}")]
        public async Task<IActionResult> GetAllUserCards([FromRoute] string id)
        {
            var user = await cardDbContext.users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var list = cardDbContext.cards.Where(card => card.userId == id);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {

            user.Id = Guid.NewGuid().ToString();
            user.isLogedIn = false;
            await cardDbContext.users.AddAsync(user);
            await cardDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] User user)
        {
            var u = await cardDbContext.users.FirstOrDefaultAsync(x => x.Id == id);
            if (u != null)
            {
                u.userName = user.userName;
                u.password = user.password;
                u.isLogedIn = u.isLogedIn;
                await cardDbContext.SaveChangesAsync();
                return Ok(u);
            }
            else
            {
                return NotFound("User not found");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var u = await cardDbContext.users.FirstOrDefaultAsync(x => x.Id == id);
            if (u != null)
            {   
                var cardList = cardDbContext.cards.Where(card => card.userId == id).ToList();
                for (int i =0; i< cardList.Count; i++)
                {
                    cardDbContext.Remove(cardList[i]);
                }
                cardDbContext.Remove(u);
                await cardDbContext.SaveChangesAsync();
                return Ok(u);
            }
            else
            {
                return NotFound("User not found");
            }
        }

    }
}
