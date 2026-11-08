using System.ComponentModel.DataAnnotations;
using ParkingApi.Entities;
using ParkingApi.Constants;

namespace ParkingApi.Dtos;

public class CreateParkingPassRequest : IValidatableObject
{
    [Required]
    public int? ParkingLotId { get; set; }

    [Required]
    [StringLength(ParkingPassConstants.LicensePlateMaxLength, MinimumLength = ParkingPassConstants.LicensePlateMinLength)]
    public string LicensePlate { get; set; } = default!;

    [Required]
    [RegularExpression(".*@.*", ErrorMessage = "The Email field must contain '@'.")]
    public string HolderEmail { get; set; } = default!;

    [Required]
    public DateOnly? ValidFrom  { get; set; }

    [Required]
    [EnumDataType(typeof(ParkingPassDuration))]
    public ParkingPassDuration? DurationMonths { get; set; }

    [Required]
    public bool? RequiresCharging { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => throw new NotImplementedException();
}
