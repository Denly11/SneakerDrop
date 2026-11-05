using System.ComponentModel.DataAnnotations;
using ParkingApi.Constants;

namespace ParkingApi.Dtos;

public class ParkingLotRequest : IValidatableObject
{
    [Required]
    [StringLength(ParkingLotConstraints.NameMaxLength, MinimumLength = ParkingLotConstraints.NameMinLength)]
    public string Name { get; set; } = default!;

    [Required]
    [Range(ParkingLotConstraints.CapacityMin, ParkingLotConstraints.CapacityMax)]
    public int? Capacity { get; set; }

    [Required]
    [Range(ParkingLotConstraints.HourlyRateCzkMin, ParkingLotConstraints.HourlyRateCzkMax)]
    public decimal? HourlyRateCzk { get; set; }

    [Required]
    public bool? HasEvCharging { get; set; }

    // null = nonstop provoz
    public OpeningHoursRequest? OpeningHours { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var rate = HourlyRateCzk!.Value;

        if (decimal.Round(rate, ParkingLotConstraints.HourlyRateCzkMaxDecimalPlaces) != rate)
        {
            yield return new ValidationResult(
                $"HourlyRateCzk can have at most {ParkingLotConstraints.HourlyRateCzkMaxDecimalPlaces} decimal places.",
                [nameof(HourlyRateCzk)]);
        }
    }
}
