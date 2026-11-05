namespace ParkingApi.Dtos;

public class OpeningHoursResponse
{
    // Formát HH:mm
    public string Open { get; set; } = default!;

    // Formát HH:mm
    public string Close { get; set; } = default!;
}
