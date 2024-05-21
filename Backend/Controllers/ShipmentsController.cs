using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Linq;


namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentsController : ControllerBase
    {
        private readonly DataContext _context;

        public ShipmentsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetShipments()
        {
            var shipments = _context.Shipments
                .Include(s => s.Bags)
                    .ThenInclude(b => b.Parcels)
                .ToList();
            return Ok(shipments);
        }
        
        [HttpPost("{id}/finalize")]
        public IActionResult FinalizeShipment(int id)
        {
            var shipment = _context.Shipments
                .Include(s => s.Bags)
                .FirstOrDefault(s => s.Id == id);

            if (shipment == null)
            {
                return NotFound("Shipment not found.");
            }

            if (shipment.IsFinalized)
            {
                return BadRequest("Shipment is already finalized.");
            }
            
            if (shipment.Bags.Any(b => b.Parcels.Any()))
            {
                shipment.IsFinalized = true;
                _context.SaveChanges();
                return Ok("Shipment finalized successfully.");
            }
            else
            {
                return BadRequest("Cannot finalize shipment with no parcels.");
            }
        }

        [HttpPost]
        public IActionResult CreateShipment(Shipment shipment)
        {
            _context.Shipments.Add(shipment);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetShipmentById), new { id = shipment.Id }, shipment);
        }

        [HttpGet("{id}")]
        public IActionResult GetShipmentById(int id)
        {
            var shipment = _context.Shipments.FirstOrDefault(s => s.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }
            return Ok(shipment);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateShipment(int id, Shipment updatedShipment)
        {
            var existingShipment = _context.Shipments.FirstOrDefault(s => s.Id == id);
            if (existingShipment == null)
            {
                return NotFound();
            }

            existingShipment.ShipmentNumber = updatedShipment.ShipmentNumber;
            existingShipment.Airport = updatedShipment.Airport;
            existingShipment.FlightNumber = updatedShipment.FlightNumber;
            existingShipment.FlightDate = updatedShipment.FlightDate;
            existingShipment.IsFinalized = updatedShipment.IsFinalized;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShipment(int id)
        {
            var shipment = _context.Shipments.FirstOrDefault(s => s.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }

            _context.Shipments.Remove(shipment);
            _context.SaveChanges();

            return NoContent();
        }
    }
}