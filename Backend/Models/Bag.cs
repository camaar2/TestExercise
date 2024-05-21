using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Models;

public class Bag
{
    public int Id { get; set; }
        
    [Required]
    [MaxLength(15)]
    [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Bag number can only contain letters and digits.")]
    public string? BagNumber { get; set; }
    [BagWithParcels]    
    public ICollection<Parcel> Parcels { get; set; } = new List<Parcel>();
    [BagWithLetters]    
    public int? LetterCount { get; set; }
    [BagWithParcels]
    [Range(0, 999.999, ErrorMessage = "Weight must be between 0 and 999.999.")]
    public decimal? BagWeight { get; set; }
    [BagWithParcels]
    [Range(0, 999999.99, ErrorMessage = "Price must be between 0 and 999999.99.")]
    public decimal? BagPrice { get; set; }
    [BagWithParcels]
    public int? ParcelCount { get; set; }
        
    
    [JsonIgnore]
    public Shipment? Shipment { get; set; }
    public int? ShipmentId { get; set; }
        
    public bool IsLetter { get; set; }
}
public class BagWithParcelsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var bag = (Bag)validationContext.ObjectInstance;
        
        if (!bag.IsLetter && !bag.Parcels.Any())
        {
            return new ValidationResult("Bag with parcels must have at least one parcel.");
        }
        if (bag.BagWeight == null)
        {
            return new ValidationResult("Weight must be specified for bag with parcels.");
        }
        if (bag.BagPrice == null)
        {
            return new ValidationResult("Price must be specified for bag with parcels.");
        }

        return ValidationResult.Success!;
    }
}
public class BagWithLettersAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var bag = (Bag)validationContext.ObjectInstance;
        
        if (bag.IsLetter)
        {
            if (bag.LetterCount == null || bag.LetterCount == 0)
            {
                return new ValidationResult("Count of letters must be greater than zero for bag with letters.");
            }
        }

        return ValidationResult.Success!;
    }
}
