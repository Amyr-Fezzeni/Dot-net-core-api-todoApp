 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoapp.Data;
using todoapp.models;

namespace todoapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : Controller
    {
        private readonly CardDbContext cardDbContext;
        public CardController(CardDbContext cardDbContext)
        {
            this.cardDbContext = cardDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await cardDbContext.cards.ToListAsync();
       
            return Ok(cards);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var card = await cardDbContext.cards.FirstOrDefaultAsync(x => x.Id == id);
            if (card != null)
            {
                return Ok(card);
            }
            return NotFound("Card not found");
        }

        [HttpPost]
      
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {

            card.Id = Guid.NewGuid();
            await cardDbContext.cards.AddAsync(card);
            await cardDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var c = await cardDbContext.cards.FirstOrDefaultAsync(x => x.Id == id);
            if (c != null)
            {
                c.cardName = card.cardName;
                c.cardDescription = card.cardDescription;
                c.cardImportance = card.cardImportance;
                await cardDbContext.SaveChangesAsync();
                return Ok(c);
            }
            else
            {
                return NotFound("card not found");
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var c = await cardDbContext.cards.FirstOrDefaultAsync(x => x.Id == id);
            if (c != null)
            {
                cardDbContext.Remove(c);
                await cardDbContext.SaveChangesAsync();
                return Ok(c);
            }
            else
            {
                return NotFound("card not found");
            }
        }
    
    
    }
}
