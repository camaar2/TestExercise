using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Shipment
{
    public int Id { get; set; }
        
    [Required]
    [RegularExpression(@"^[A-Za-z0-9]{3}-[A-Za-z0-9]{6}$", ErrorMessage = "Shipment number format is invalid.")]
    public string? ShipmentNumber { get; set; }
    [EnumDataType(typeof(Airport))]    
    public string? Airport { get; set; }
    [RegularExpression(@"^[A-Z]{2}[0-9]{4}$", ErrorMessage = "Flight number format is invalid.")]     
    public string? FlightNumber { get; set; }
        
    [Required]
    [FutureDate(ErrorMessage = "Flight date must be in the future.")]
    public DateTime FlightDate { get; set; }
        
    public ICollection<Bag> Bags { get; set; }= new List<Bag>();
    [Required(ErrorMessage = "At least one bag must be assigned to the shipment.")]    
    public bool IsFinalized { get; set; }
}
public enum Airport
{
    TLL,
    RIX,
    HEL
}
public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime date && date < DateTime.Now)
        {
            return new ValidationResult(ErrorMessage);
        }
        return ValidationResult.Success!;
    }
}

