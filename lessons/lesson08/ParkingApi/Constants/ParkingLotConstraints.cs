namespace ParkingApi.Constants;

public static class ParkingLotConstraints
{
    public const int NameMinLength = 3;
    public const int NameMaxLength = 100;

    public const int CapacityMin = 1;
    public const int CapacityMax = 2000;

    public const double HourlyRateCzkMin = 0.01;
    public const double HourlyRateCzkMax = 500;
    public const int HourlyRateCzkMaxDecimalPlaces = 2;

    public const string OpeningHoursTimeFormat = "HH:mm";
    public const string OpeningHoursTimePattern = @"^([01]\d|2[0-3]):[0-5]\d$";

}