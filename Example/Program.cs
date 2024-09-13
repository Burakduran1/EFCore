
using Microsoft.EntityFrameworkCore;

Console.WriteLine("");

public class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=?; Datatbase=?Example; Integrated Security=?;");


    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .Property(p => p.TotalAmount)
            .HasComputedColumnSql("[UnitPrice] * [Quantity]");
    }


}




public class Order
{
    public int Id { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
}
