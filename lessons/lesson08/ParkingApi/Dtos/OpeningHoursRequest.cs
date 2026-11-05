using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ParkingApi.Constants;

namespace ParkingApi.Dtos;

public class OpeningHoursRequest : IValidatableObject
{
    // Formát HH:mm
    [Required]
    [RegularExpression(ParkingLotConstraints.OpeningHoursTimePattern)]
    public string Open { get; set; } = default!;

    // Formát HH:mm
    [Required]
    [RegularExpression(ParkingLotConstraints.OpeningHoursTimePattern)]
    public string Close { get; set; } = default!;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var open = TimeOnly.ParseExact(Open, ParkingLotConstraints.OpeningHoursTimeFormat, CultureInfo.InvariantCulture);
        var close = TimeOnly.ParseExact(Close, ParkingLotConstraints.OpeningHoursTimeFormat, CultureInfo.InvariantCulture);

        if (open >= close)
        {
            yield return new ValidationResult("Close must be later than Open.", [nameof(Close)]);
        }
    }
}
