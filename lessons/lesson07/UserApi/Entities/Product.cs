namespace UserApi.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Category { get; set; } 
    public int UserId { get; set; }//je to tak řešeno v assigment.md, co tam má být
}
