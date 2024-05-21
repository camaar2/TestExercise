using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Models;

public class Parcel
{
    public int Id { get; set; }
    [Required]
    [RegularExpression(@"^[A-Z]{2}[0-9]{7}[A-Z]{2}$", ErrorMessage = "Parcel number format is invalid.")]
    public string? ParcelNumber { get; set; }
    [Required]
    [MaxLength(100)]
    public string RecipientName { get; set; } = default!;
    [Required]
    [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Destination country code must be a 2-letter code.")]
    public string? DestinationCountry { get; set; }
    [Range(0,999.999, ErrorMessage = "Weight must be between 0 and 999.999.")]
    public decimal Weight { get; set; }
    public bool IsLetter { get; set; }
    [Range(0, 999999.99, ErrorMessage = "Price must be between 0 and 999999.99.")]
    public decimal Price { get; set; }
    public string? Currency { get; set; }
    [JsonIgnore]
    public Bag? Bag { get; set; }
    public int? BagId { get; set; }
}


