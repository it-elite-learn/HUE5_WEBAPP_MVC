namespace App.Models;

public class Employee
{
	public int Id { get; set; }
	public required string Firstname { get; set; } = string.Empty;
	public required string Lastname { get; set; } = string.Empty;
	public DateTime BirthDate { get; set; }
}
