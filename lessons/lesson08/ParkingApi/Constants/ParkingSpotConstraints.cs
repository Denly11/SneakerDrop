namespace ParkingApi.Constants;

public static class ParkingSpotConstraints
{
    // Písmeno, pomlčka, tři číslice (např. B-017). Malá písmena se převedou na velká.
    public const string LabelPattern = @"^[A-Za-z]-\d{3}$";
}
