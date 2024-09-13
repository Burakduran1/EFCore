using Microsoft.EntityFrameworkCore;
Console.WriteLine();
ApplicationDbContext context = new();
#region Table Per Hierarchy (TPH) Nedir?
//Kalıtımsal ilişkiye sahip olan entitylerin olduğu senaryolarda her bir hiyerarşiye karşılık bir tablo oluşturan davranıştır.
#endregion
#region Neden Table Per Hierarchy Yaklaşımında Bir Tabloya İhtiyacımız Olsun?
//İÇerisinde benzer alanlara sahip olan entityleri migrate ettiğimizde her entitye karşılık bir tablo oluşturmaktansa bu entityleri tek bir tabloda modellemek isteyebilir ve bu tablodaki kayıtları discriminator kolonu üzerinden birbirlerinden ayırabiliriz. İşte bu tarz bir tablonun oluşturulması ve bu tarz bir tabloya göre sorgulama, veri ekleme, silme vs. gibi operasyonların şekillendirilmesi için TPH davranışını kullanabiliriz.
#endregion
#region TPH Nasıl Uygulanır?
//EF Core'da entity aransında temel bir kalıtımsal ilişki söz konusuysa eğer default oalrak kabul edilen davranıştır.
//O yüzden herhangi bir konfigüreasyon gerektirmez!
//Entityler kendi aralarında kalıtımsal ilişkiye sahip olmalı ve bu entitylerin hepsi DbContext nesnesine DbSet olarak eklenmelidir! 
#endregion
#region Discriminator Kolonu Nedir?
//Table Per Hierarchy yaklaşımı neticesinde kümülatif olarak inşa edilmiş tablonun hangi entitye karşılık veri tuttuğunu ayırt edebilmemizi sağlayan bir kolondur.
//EF Core tarafından otomatik olarak tabloya yerleştirilir.
//Default olarak içerisinde entity isimlerini tutar.
//Discriminator kolonunu komple özelleştirebiliriz.
#endregion
#region Discriminator Kolon Adı Nasıl Değiştirilir?
//Öncelikle hiyerarşinin başında hangi sınıf varsa onun Fluent API'da konfigürasyonuna gidilmeli.
//Ardından HasDiscriminator fonksiyonu ile özelleştirilmeli.
#endregion
#region Discriminator Değerleri Nasıl Değiştirilir?
//Yine hiyearşinin bşaındaki entity konfigürasyonlarına gelip, HasDiscriminator fonksiyonu ile özelleştirmede bulunarak ardından HasValue fonksiyonu ile hangi entitye karşılık hangi değerin girieceğini belirtilen türde ifade edebilirsiniz.
#endregion
//Employee employee = new Employee() { Name = "Burak Can", Surname = "Duran" };
//await context.Employees.AddAsync(employee);
//await context.SaveChangesAsync();
#region TPH'da Veri Ekleme
//Davranışların hiçbirinde veri eklerken,silerken, güncellerken vs. normal operasyonların dışında bir işlem yapılmaz!
//Hangi davranışıo kullanıyorsanız EF Core ona göre arkaplanda modellemeyi gerçekleştirecektir.
//Employee employee = new() { Name = "Burak Can", Surname = "Duran", Department = "IT" };
//Employee employee1 = new() { Name = "Ensar", Surname = "Duran", Department = "IT" };
//Customer customer = new() { CompanyName = "Duran'lar Galeri", Name = "Ali Rıza", Surname = "Duran" };
//Customer customer1 = new() { Name = "Nisanur", Surname = "Duran", CompanyName = "Ziraat Teknoloji" };
//Technician technician = new() { Name = "Rıfkı", Surname = "Kıllıbacak", Department = "Kalorifer ve Kazan Dairesi" };
//await context.AddRangeAsync(employee, employee1, customer, customer1, technician);
//await context.SaveChangesAsync();
#endregion
#region TPH'da Veri Silme
//TPH davranışında silme operasyonu yine entity üzerinden gerçekleştirilir.
//var employee = await context.Employees.FindAsync(1);
//var employee = await context.Employees.FindAsync(3);
//context.Employees.Remove(employee);
//await context.SaveChangesAsync();
//var customers = await context.Customers.ToListAsync();
//context.Customers.RemoveRange(customers);
//await context.SaveChangesAsync();   
#endregion
#region TPH'da Veri Güncelleme
//TPH davranışında güncelleme operasyonu yine entity üzerinden gerçekleştirilir.
//var value  = await context.Employees.FindAsync(5);
//value.Name = "Kazim";
//await context.SaveChangesAsync();
#endregion
#region TPH'da Veri Sorgulama
//Veri sorgulama oeprasyonu bilinen DbSet propertysi üzerinden sorgulamadır. Ancak burada dikkat edilmesi gereken bir husus vardır. O da şu;
//var employees = await con text.Employees.ToListAsync();
//var techs = await context.Technicians.ToListAsync();
//kalıtımsal ilişkiye göre yapılan sorgulamada üst sınıf alt sınıftaki verileride kapsamaktadır. O yüzden üst sınıfların sorgulamalarında alt sınıfların verileride gelecektir buna dikkat edilmelidir.
//Sorgulama süreçlerinde EF Core generate edilen sorguya bir where şartı eklemektedir.
var value = await context.Employees.ToListAsync();

#endregion
#region Farklı Entity'ler de Aynı İsimde Sütunların Olduğu Durumlar
//Entitylerde mükerrer kolonlar olabilir. Bu kolonları EF core isimsel olarak özelleştirip ayıracaktır.
#endregion
abstract class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}
class Employee : Person
{
    public string? Department { get; set; }
}
class Customer : Person
{
    public int A { get; set; }
    public string? CompanyName { get; set; }
}
class Technician : Employee
{
    public int A { get; set; }
    public string? Branch { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Technician> Technicians { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Person>()
        //    .HasDiscriminator<string>("ayirici")
        //    .HasValue<Person>("A")
        //    .HasValue<Employee>("B")
        //    .HasValue<Customer>("C")
        //    .HasValue<Technician>("D");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=?; Datatbase=?TPH; Integrated Security=?;");
    }
}