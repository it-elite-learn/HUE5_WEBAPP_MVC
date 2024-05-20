using Microsoft.EntityFrameworkCore;

public class LibContext : DbContext
{
	public LibContext(DbContextOptions<LibContext> options)
		: base(options)
	{
	}

	public DbSet<App.Models.Employee> Employee { get; set; } = default!;
}
