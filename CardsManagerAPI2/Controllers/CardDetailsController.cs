using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CardsManagerAPI2.Models;

namespace CardsManagerAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardDetailsController : ControllerBase
    {
        private readonly CardDetailsDbContext _context;

        public CardDetailsController(CardDetailsDbContext context)
        {
            _context = context;
        }

        // GET: api/CardDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardDetails>>> GetPaymentDetails()
        {
            return await _context.PaymentDetails.ToListAsync();
        }

        // GET: api/CardDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardDetails>> GetCardDetails(int id)
        {
            var cardDetails = await _context.PaymentDetails.FindAsync(id);

            if (cardDetails == null)
            {
                return NotFound();
            }

            return cardDetails;
        }

        // PUT: api/CardDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardDetails(int id, CardDetails cardDetails)
        {
            if (id != cardDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(cardDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CardDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CardDetails>> PostCardDetails(CardDetails cardDetails)
        {
            _context.PaymentDetails.Add(cardDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCardDetails", new { id = cardDetails.Id }, cardDetails);
        }

        // DELETE: api/CardDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardDetails(int id)
        {
            var cardDetails = await _context.PaymentDetails.FindAsync(id);
            if (cardDetails == null)
            {
                return NotFound();
            }

            _context.PaymentDetails.Remove(cardDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardDetailsExists(int id)
        {
            return _context.PaymentDetails.Any(e => e.Id == id);
        }
    }
}
