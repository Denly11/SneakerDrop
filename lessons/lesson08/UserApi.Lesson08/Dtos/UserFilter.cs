using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace UserApi.Lesson08.Dtos;

public class UserFilter
{
    public string? Name { get; set; }

    [FromQuery(Name = "min_age")]
    public int? MinAge { get; set; }

    [FromQuery(Name = "max_age")]
    public int? MaxAge { get; set; }

    [Range(1, int.MaxValue)]
    public int? Limit { get; set; }
}
