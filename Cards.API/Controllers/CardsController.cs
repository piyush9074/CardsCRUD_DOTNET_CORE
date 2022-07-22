using Cards.API.Data;
using Cards.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardDbContext cardDbContext;

        public CardsController(CardDbContext cardDbContext)
        {
            this.cardDbContext = cardDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards =await cardDbContext.Cards.ToListAsync();

            return Ok(cards);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var card = await cardDbContext.Cards.FirstOrDefaultAsync(x => x.ID == id);

            if (card != null)
            {
                return Ok(card);
            }

            return NotFound("Card Not Found");


        }
        //AddSingleCard
        [HttpPost]
        
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.ID= Guid.NewGuid();
            await cardDbContext.Cards.AddAsync(card);
            await cardDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new {id= card.ID }, card);

        }

        //updating A Card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var existingCard = await cardDbContext.Cards.FirstOrDefaultAsync(x => x.ID == id);
            if (existingCard != null)
            {
                existingCard.ID = Guid.NewGuid();
                existingCard.UserName = card.UserName;
                existingCard.CardNumber = card.CardNumber;
                existingCard.ExpiryMonth= card.ExpiryMonth;
                existingCard.ExpiryYear= card.ExpiryYear;

                return Ok(existingCard);

            }
            return NotFound("Card Not Found");
        }
        //Delete A Card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existingCard = await cardDbContext.Cards.FirstOrDefaultAsync(x => x.ID == id);
            if (existingCard != null)
            {
                cardDbContext.Remove(existingCard);
                await cardDbContext.SaveChangesAsync();

                return Ok(existingCard);

            }
            return NotFound("Card Not Found");
        }
    }
}
