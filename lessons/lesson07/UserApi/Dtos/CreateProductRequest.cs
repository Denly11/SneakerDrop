using System.ComponentModel.DataAnnotations;

namespace UserApi.Dtos;

public class CreateProductRequest
{
    [Required]
    [StringLength(100)]//sám o sobě by dovoloval null, to required nad tím povoluje jen to 0-100
    public string Name { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "1000000")]//decimal ani nepovoluje null, on neumí c# pracovat s deciaml napřímo
    public decimal Price { get; set; }

    //může být null, takže NE required
    [StringLength(50)]//pokud to nyvahovuje vrátí 400
    public string? Category { get; set; }
}
