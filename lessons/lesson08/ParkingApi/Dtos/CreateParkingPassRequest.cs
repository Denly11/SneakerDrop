using System.ComponentModel.DataAnnotations;
using ParkingApi.Entities;
using ParkingApi.Constants;

namespace ParkingApi.Dtos;

public class CreateParkingPassRequest : IValidatableObject
{
    [Required]
    public int? ParkingLotId { get; set; }//? - aby to automaticky nedosadilo při nevyplnění jako hodnotu 0

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

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)//jenom inspektí
    {
        var licensePlateCorrected = LicensePlate.Replace(" ", "").ToUpper();
        if(LicensePlate != licensePlateCorrected)
        {
            yield return new ValidationResult("The LicensePlate field has leading or trailing whitespace.", new[] { nameof(LicensePlate) });
        }


        var today = DateOnly.FromDateTime(DateTime.Today);

        if (ValidFrom < today)
        {
            yield return new ValidationResult("The ValidFrom field must be in the future.", new[] { nameof(ValidFrom) });
        }

        //ta závorka mi pak dá počet dní(předtím jsem tam měl validFrom - today
        if (ValidFrom  > today.AddMonths(ParkingPassConstants.ValidFromMaxTimePeriod))
        { 
            yield return new ValidationResult($"The ValidFrom field must less than {ParkingPassConstants.ValidFromMaxTimePeriod} months in the future.", new[] { nameof(ValidFrom) });
        }
    }
}
