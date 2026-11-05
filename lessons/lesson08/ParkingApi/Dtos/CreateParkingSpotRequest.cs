using System.ComponentModel.DataAnnotations;
using ParkingApi.Constants;
using ParkingApi.Entities;

namespace ParkingApi.Dtos;

public class CreateParkingSpotRequest
{
    [Required]
    [RegularExpression(ParkingSpotConstraints.LabelPattern)]
    public string Label { get; set; } = default!;

    [Required]
    [EnumDataType(typeof(ParkingSpotType))]
    public ParkingSpotType? Type { get; set; }
}
