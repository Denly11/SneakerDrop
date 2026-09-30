namespace LinqPractice.Solution.Models;

public class Customer
{
    public required string Name { get; set; }
    public List<Order> Orders { get; set; } = [];
}
