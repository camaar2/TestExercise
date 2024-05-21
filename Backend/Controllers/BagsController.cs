using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Linq;
using System.Diagnostics;


namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BagsController : ControllerBase
    {
        private readonly DataContext _context;

        public BagsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBags()
        {
            var bags = _context.Bags
                .Include(s => s.Parcels)
                .ToList();
            return Ok(bags);
        }

        [HttpPost]
        public IActionResult CreateBag(Bag bag)
        {
            var shipment = _context.Shipments.FirstOrDefault(s => s.Id == bag.ShipmentId);
            if (shipment == null || shipment.IsFinalized)
            {
                return BadRequest("Cannot add bags to a non-existent or finalized shipment.");
            }

            _context.Bags.Add(bag);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBagById), new { id = bag.Id }, bag);
        }

        [HttpGet("{id}")]
        public IActionResult GetBagById(int id)
        {
            var bag = _context.Bags
                .Include(b => b.Parcels)
                .FirstOrDefault(b => b.Id == id);

            if (bag == null)
            {
                return NotFound();
            }
            return Ok(bag);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBag(int id, Bag updatedBag)
        {
            var existingBag = _context.Bags.FirstOrDefault(b => b.Id == id);
            if (existingBag == null)
            {
                return NotFound();
            }

            var shipment = _context.Shipments.FirstOrDefault(s => s.Id == existingBag.ShipmentId);
            if (shipment == null || shipment.IsFinalized)
            {
                return BadRequest("Cannot update bags of a non-existent or finalized shipment.");
            }

            existingBag.BagNumber = updatedBag.BagNumber;
            existingBag.BagWeight = updatedBag.BagWeight;
            existingBag.BagPrice = updatedBag.BagPrice;
            existingBag.ShipmentId = updatedBag.ShipmentId;
            existingBag.IsLetter = updatedBag.IsLetter;
            existingBag.LetterCount = updatedBag.LetterCount;
            existingBag.ParcelCount = updatedBag.ParcelCount;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBag(int id)
        {
            var bag = _context.Bags.FirstOrDefault(b => b.Id == id);
            if (bag == null)
            {
                return NotFound();
            }

            var shipment = _context.Shipments.FirstOrDefault(s => s.Id == bag.ShipmentId);
            if (shipment == null || shipment.IsFinalized)
            {
                return BadRequest("Cannot delete bags from a non-existent or finalized shipment.");
            }

            _context.Bags.Remove(bag);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

