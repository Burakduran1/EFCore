// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Reflection;
Console.WriteLine();
ApplicationDbContext context = new();
#region Explicit Loading

//Oluşturulan sorguya eklenecek verilerin şartlara bağlı bir şekilde/ihtiyaçlara istinaden yüklenmesini sağlayan bir yaklaşımdır.

//var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == 1);
//if (employee.Name == "Burak")
//{
//    var orders = await context.Orders.Where(o => o.EmployeeId == employee.Id).ToListAsync();
//}

#region Reference

//Explicit Loading sürecinde ilişkisel olarak sorguya eklemek istenen tablonun navigation propertysi eğer ki tekil bir türse bu tabloyu reference ile sorguya ekleyebilmekteyiz.
//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 3);

//await context.Entry(employee).Reference(e => e.Region).LoadAsync();

Console.ReadLine();
#endregion

#region Collection
//Explicit Loading sürecinde ilişkisel olarak sorguya eklemek istenen tablonun navigation propertysi eğer ki çoğul/koleksiyonel bir türse bu tabloyu collection ile sorguya ekleyebilmekteyiz.

var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == 1);

await context.Entry(employee).Collection(e => e.Orders).LoadAsync();
Console.WriteLine();

#endregion


#endregion









public class Employee
{
    public int Id { get; set; }
    public int RegionId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }

    public List<Order> Orders { get; set; }
    public Region Region { get; set; }
}
public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }

    public Employee Employee { get; set; }
}


class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Region> Regions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<Employee>()
            .HasData(new Employee { Id = 1, Name = "Burak", Surname = "Duran" });
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("?");
    }
}