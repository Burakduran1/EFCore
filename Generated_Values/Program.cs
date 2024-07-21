// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

ApplicationDbContext context = new();

#region GeneratedValue nedir ? 
//EF Core'da üretilen değerlerle ilgili çeşitli modellerin ayrıntılarını yapılandırmamızı sağlayan bir konfigürasyondur.
#endregion

#region DefaultValues
//EF Core herhangi bir tablonun herhangi bir kolonuna yazılım tarafından bir değer gönderilmediği taktirde bu kolona hangi değerin yazılacağını belirten yapıdır.
#region HasDefaultValue
//Statik veri
#endregion

#region HasDefaultValueSql
//Sql cümleciği 
#endregion

#endregion

#region Computed Columns

#region HasComputedColumnSql
//Tablo içerisindeki kolonlar üzerinde yapılan aritmetik işlemler neticesinde üretilen kolondur.
#endregion
#endregion

#region Value Generation
#region Primary Keys
//Herhangi bir  tablodaki satırları kimlik vari şekilde tanımlayan, tekil (uniqe olan sütun veya sütünlardır.
#endregion
#region Identity
//Identity, yalnızca otomatik olarak artan bir sütundur. Bir sütun, PK olmaksızın identity olarak tanımlanabilir. !! Bir tablo içerisinde identity kolonu sadece tek bir tane olarak tanımlanabilir.
#endregion

//Bu iki özellik  genellikle    birlikte kullanılmaktadır. O yüzden EF Core PK  olan bir kolonu otomatik olarak Identity olacak şekilde yapılandırmaktadır. Ancak böyle olması için bir gereklilk yoktur!
#endregion


#region DatabaseGenerated
#region DatabaseGeneratedOption.None - ValueGeneratedNever
//Bir kolonda değer üretilmeyecekse eğer None ile işaretliyoruz.
//EF Core'un default olarak PK kolonlarda getirdiği Identity özelliğini kaldırmak istiyorsak eğer None'ı kullanabiliriz.
#endregion
#endregion

//Person p = new Person()
//{
//    Name = "Burak",
//    Surname = "Duran",
//    Premium = 10,
//    TotalGain = 100,
//    PersonCode = Guid.NewGuid()
//};

//await context.AddAsync(p);
//await context.SaveChangesAsync();

//Person p = new()
//{
//    Name = "Ali",
//    Surname = "Duran",
//    Premium = 10,
//    TotalGain = 40,
//    PersonCode = Guid.NewGuid()
//};
//await context.AddAsync(p);
//await context.SaveChangesAsync();




















public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=Burak; Database=GeneratedValue; Integrated Security=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .Property(p => p.Salary)
            //.HasDefaultValue(100);
            .HasDefaultValueSql(" FLOOR(RAND() * 1000 )");
        modelBuilder.Entity<Person>()
            .Property(p => p.TotalGain)
            .HasComputedColumnSql("([Salary] + [Premium]) * 10");

        modelBuilder.Entity<Person>()
            .Property(p => p.PersonId)
            .ValueGeneratedNever();
    }
}



public class Person
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // yada fluentAPI ile map et
    public int PersonId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Premium { get; set; }
    public int Salary { get; set; }
    public int TotalGain { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PersonCode { get; set; }
}