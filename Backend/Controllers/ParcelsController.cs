using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Linq;


namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParcelsController : ControllerBase
    {
        private readonly DataContext _context;

        public ParcelsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parcel>>> GetParcels()
        {
            return await _context.Parcels.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Parcel>> GetParcelById(int id)
        {
            var parcel = await _context.Parcels.FindAsync(id);

            if (parcel == null)
            {
                return NotFound();
            }

            return parcel;
        }
        
        [HttpPost]
        public IActionResult CreateParcel(Parcel parcel)
        {
            var bag = _context.Bags
                .Include(b => b.Shipment)
                .FirstOrDefault(b => b.Id == parcel.BagId);

            if (bag == null || bag.Shipment.IsFinalized)
            {
                return BadRequest("Cannot add parcels to a non-existent bag or a bag in a finalized shipment.");
            }

            _context.Parcels.Add(parcel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetParcelById), new { id = parcel.Id }, parcel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateParcel(int id, Parcel updatedParcel)
        {
            var existingParcel = _context.Parcels.FirstOrDefault(p => p.Id == id);
            if (existingParcel == null)
            {
                return NotFound();
            }

            var bag = _context.Bags
                .Include(b => b.Shipment)
                .FirstOrDefault(b => b.Id == existingParcel.BagId);

            if (bag == null || bag.Shipment.IsFinalized)
            {
                return BadRequest("Cannot update parcels in a non-existent bag or a bag in a finalized shipment.");
            }

            existingParcel.ParcelNumber = updatedParcel.ParcelNumber;
            existingParcel.RecipientName = updatedParcel.RecipientName;
            existingParcel.DestinationCountry = updatedParcel.DestinationCountry;
            existingParcel.Weight = updatedParcel.Weight;
            existingParcel.Price = updatedParcel.Price;
            existingParcel.Currency = updatedParcel.Currency;
            existingParcel.IsLetter = updatedParcel.IsLetter;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteParcel(int id)
        {
            var parcel = _context.Parcels.FirstOrDefault(p => p.Id == id);
            if (parcel == null)
            {
                return NotFound();
            }

            var bag = _context.Bags
                .Include(b => b.Shipment)
                .FirstOrDefault(b => b.Id == parcel.BagId);

            if (bag == null || bag.Shipment.IsFinalized)
            {
                return BadRequest("Cannot delete parcels from a non-existent bag or a bag in a finalized shipment.");
            }

            _context.Parcels.Remove(parcel);
            _context.SaveChanges();

            return NoContent();
        }

        private bool ParcelExists(int id)
        {
            return _context.Parcels.Any(e => e.Id == id);
        }
    }
}

