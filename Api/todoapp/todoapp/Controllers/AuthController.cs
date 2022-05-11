using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoapp.Data;
using todoapp.models;

namespace todoapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly CardDbContext cardDbContext;
        public AuthController(CardDbContext cardDbContext)
        {
            this.cardDbContext = cardDbContext;
        }

        [HttpPost]
        [Route("/login/{userName}/{password}")]
        public async Task<IActionResult> LogIn([FromRoute] string userName, [FromRoute] string password)
        {
            var user = await cardDbContext.users
                .FirstOrDefaultAsync(user => user.userName == userName && user.password == password);


            if (user != null)
            {
                if (user.isLogedIn)
                {
                    return NotFound("you are already connected in other device");
                }
                user.isLogedIn = true;
                await cardDbContext.SaveChangesAsync();
                return Ok(user);
            }
            return NotFound("Username or Password incorrect");
        }

        [HttpPost]
        [Route("/logout")]
        public async Task<IActionResult> LogOut([FromBody] string id)
        {
            var user = await cardDbContext.users
                .FindAsync(id);
            if (user != null)
            {
                user.isLogedIn = false;
                await cardDbContext.SaveChangesAsync();
                return Ok("disconnected");

            }
            return NotFound("user id invalid");

        }

    }
}
