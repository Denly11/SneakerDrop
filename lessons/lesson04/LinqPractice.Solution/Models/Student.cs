namespace LinqPractice.Solution.Models;

public class Student
{
    public required string Name { get; set; }
    public List<int> Grades { get; set; } = [];
}
