// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.Data.SqlClient;

ApplicationDbContext context = new();


//Eğer ki, sorgunuzu LINQ ile ifade eddemiyorsanız yahut link LINQ'in ürettiği sorguya nazaran daha optimize bi sorguyu manuel geliştirmek  ve efcore üzerinden execute etmek istiyorsak, efcore bu davarnışı desteklmekte.

#region FromSqlInterpolated 
//EF Core 7.0 sürümünden önce ham sorguları execute edebildiğimiz fonksiyondur. 
//var persons = await context.Persons.FromSqlInterpolated($"SELECT * FROM Persons").ToListAsync();


#endregion

#region FromSql - EF Core 7.0

#region Query Execute 
//var person = await context.Persons.FromSql($"SELECT * FROM Persons").ToListAsync();
#endregion
#region Stored Procedure Execute
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons 4").ToListAsync();
#endregion
#region Parametreli Sorgu Oluşturma
#region Örnek 1
//int personId = 3;
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons Where PersonId={personId}")
//    .ToListAsync();
#endregion
#region Örnek 2
//int personId = 3;
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons {personId} ").ToListAsync();

#endregion
#region Örnek 3
//SqlParameter personId = new("PersonId","3");
//personId.Direction = System.Data.ParameterDirection.Input;
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons Where PersonId={personId}").ToListAsync();
#endregion
#endregion
#endregion

#region Dynamic SQL Oluşturma ve Parametre Girme -FromSqlRaw
//string columnName = "PersonId", value = "3";
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons Where {columnName}={value}").ToListAsync();

//EF-Core dinamik olarak oluşturulan sorgularda özellikle kolon isimleri parametreleştirilmişse o sorguyu ÇALIŞTIRMAYACAKTIR !


//string columnName = "PersonId";
//SqlParameter value = new("PersonId", "3");
//var persons = context.Persons.FromSqlRaw($"SELECT * FROM Persons WHERE {columnName} = @PersonId", value).ToList();

//FromSql ve FromSqlInterpolated metotlarında SQL Injection vs. gibi güvenlik önlemleri alınmış vaziyettedir, Lakin dinamik olarak sorguları oluşturuyorsanız eğer burada güvenlik geliştirici sorumludur. Yani gelen sorugda/veri yorumlar, noktalı virgüller yahut SQL'e özel karakterlerin algılanması ve bunların temizlenmesi geliştirici tarafından gerekmektedir.

#endregion

#region SqlQuery - Entity olmayan scalar sorguların çalıştırlması - Non Entity - EF-Core 7.0
//Entity'si olmayan scalar sorguların çalıştırılıp sonucunu elde etmemizi sağlayan yeni bir fonksiyondur.
//var query = context.Database.SqlQuery<int>($"SELECT PersonId FROM Persons").ToList();

//var persons = context.Persons.FromSql($"SELECT * FROM Persons")
//   .Where(p => p.Name.Contains("a"))
//   .ToList();

//var query = context.Database.SqlQuery<int>($"SELECT PersonId Value FROM Persons") //hem raw hem de linq bir arada geçiyorsa kolon adını 
//    .Where(x => x > 0) //bu şart ifadesi arka palnda default olarak Value 'ya göre şekillendirilecktir.
//    .ToList();

//SqlQuery'de (raw sorgu) LINQ operatörletiyle sorguya ekstradan katkıda bulunmak istiyorsanız eğer bu sorgu neticesinde gelecek olan kolon adını Value olarak bildirmeniz gerekmektedir. Çünkü, SqlQuery metodu sorguyu bir subquery olarak  generate etmektedir.Haliyle bu durumdan dolayı LINQ ile verilen şart ifadeleri statik olarak Value kolonuna göre tasarlanmıştır. O yüzden bu şekilde bir çalışma zorunlu gerekmektedir.
#endregion

#region ExecuteSql
/*var datas = context.Database.ExecuteSql($"Update Persons SET Name ='Fatma' Where PersonId=1");*/ //Normal şartlarda EF-Core üzerinde yapıtığmız update işlemlerinde SaveChanges çağırmak zorundayız. ExecuteSql operasyonu gerçekleştiriyorsanız SaveChanges çağırmak zorunda değilisiniz.
#endregion

#region Sınırlılıklar 
//Queryler entity türünün tüm özell ikleri için kolonlarda değer döndürmelidir.
/*var query = context.Persons.FromSql($"SELECT Name, PersonId FROM Persons").ToList(); *///Eğer SQL sorgusu sadece Name kolonunu dönerse ve PersonId kolonu eksikse, EF Core bu eksik veriyi doğru şekilde eşleyemez ve muhtemelen bir hata oluşur.
//Sütun isimleri property isimleriyle aynı olmalıdır.

//Sql sorgusu join yapısı içeremez!
var query = context.Persons.FromSql($"SELECT * FROM Persons")
    .Include(p => p.Orders)
    .ToList();
#endregion

Console.WriteLine();



public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public ICollection<Order> Orders { get; set; }
}

public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }
    public Person Person { get; set; }
}


public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=Burak; Database=Sql_Queries; Integrated Security=true; TrustServerCertificate=true; ");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);

    }
}