using Microsoft.EntityFrameworkCore;
Console.WriteLine("");
#region Veri Nasıl Eklernir ?
//EFCoreDbContext context = new();
//Product product = new()
//{

//    ProductName = "Davut"
//};
//await context.AddAsync(product);

//await context.SaveChangesAsync();
#endregion
#region Ef Core Açısından Bir verinin eklenmesi gerektiği nasıl anlaşılıyor ?
//EFCoreDbContext context = new();
//Product product = new()
//{
//  ProductName = "Burak"
//};


//Console.WriteLine(context.Entry(product)); 

//await context.AddAsync(product);
//Console.WriteLine(context.Entry(product));

//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(product));
#endregion
#region Veri tabanına birden fazla veri eklerken nelere dikkat edilmelidir ?
//EFCoreDbContext context = new();
//Product product = new()
//{
//    ProductName = "Toyota"
//};
//Product product1 = new()
//{
//    ProductName = "BMW"
//};
//Product product2 = new()
//{
//    ProductName = "Mercedes"
//};
//Product product3 = new()
//{
//    ProductName = "Fiat"
//};
//await context.AddAsync(product);
//await context.SaveChangesAsync();

//await context.AddAsync(product1);
//await context.SaveChangesAsync();

//await context.AddAsync(product2);
//await context.SaveChangesAsync();

//await context.AddAsync(product3);
//await context.SaveChangesAsync();

#endregion
#region SaveChanges() kullanımına dikkat edilmelidir.
//"SaveChanges() fonksiyonuher tetiklendiğinde bir transaction oluşturacağından dolayı EF-Core  ile yapılan her bir işleme özel kullanmaktan kaçınmalıyız! Çünkü her işleme özel transaction veri tabanı açısından ekstradan maliyet demektir. O yüzden mümkün mertebe tüm işlemlerimizi tek bir transaction eşliğinde veri tabanına gönderebilmek için SaveChanges() aşşağıdaki gibi tek seferde kullanmak hem maliyet hem de yönetilebilirlik açısından katkıda bulunmuş olacaktır."
//EFCoreDbContext context = new();
//Product product = new()
//{
//    ProductName = "Toyota"
//};
//Product product1 = new()
//{
//    ProductName = "BMW"
//};
//Product product2 = new()
//{
//    ProductName = "Mercedes"
//};
//Product product3 = new()
//{
//    ProductName = "Fiat"
//};
//await context.AddAsync(product);
//await context.AddAsync(product1);
//await context.AddAsync(product2);
//await context.AddAsync(product3);

//await context.SaveChangesAsync();
#endregion
#region AddRange kullanımı

//EFCoreDbContext context = new();
//Product product = new()
//{
//    ProductName = "Toyota"
//};
//Product product1 = new()
//{
//    ProductName = "BMW"
//};
//Product product2 = new()
//{
//    ProductName = "Mercedes"
//};
//Product product3 = new()
//{
//    ProductName = "Fiat"
//};

//await context.Products.AddRangeAsync(product, product1, product2, product3);
//await context.SaveChangesAsync();

#endregion
#region Eklenen verinin generate edilen Id'sini elde etme
//EFCoreDbContext context = new();
//Product product = new()
//{
//    ProductName = "Test"
//};
//await context.AddAsync(product);
//await context.SaveChangesAsync();
//Console.WriteLine(product.Id);
#endregion
#region Veri Ekleme
//EFCoreDbContext context = new EFCoreDbContext();
//Employee employee = new()
//{
//    ProductName = "Meto"
//};
//Employee employee1 = new()
//{
//    ProductName = "Ali"
//};
//await context.Employees.AddRangeAsync(employee, employee1);
//await context.SaveChangesAsync();
#endregion
#region Veri Güncelleme
//EFCoreDbContext context = new EFCoreDbContext();
//Employee employee = await context.Employees.FirstOrDefaultAsync(u => u.Id == 2);
//employee.ProductName= "Test";

//await context.SaveChangesAsync();
#endregion
#region Change Tracker Nedir ?
//Context nesnesi üzerinden gelen verilerin takibinden sorumlu mekanizmadır. Bu takip mekanizması sayesinde context üzerinden gelen verilerle ilgili işlemler neticesinde update yahut delete sorgularının oluşturulacağı anlaşılır.
#endregion
#region Takip Edilemeyen Nesneler Nasıl Güncellenir ? 
//EFCoreDbContext context = new EFCoreDbContext();
//Employee employee = new()
//{
//    Id = 1,
//    ProductName = "Ali"
//};
#endregion
#region Update()
//ChangeTracker mekanizması tarafından takip edilmeyen nesnelerin güncellenebilmesi için Update fonksiyonu kullanılır!
//Update Fonksiyonyu kullanabilmek için kesinlikle ilgili nesnede Id değeri verilmelidir!
//context.Employees.Update(employee);
//await context.SaveChangesAsync();
#endregion
#region Entity State nedir ?
//Bir entity instance'nın durumunu ifade eden bir referanstır.
//EFCoreDbContext context = new EFCoreDbContext();
//Employee e = new();
//Console.WriteLine(context.Entry(e).State);
//Employee e1 = new()
//{
//    Id = 1,
//    ProductName ="Ali Can"
//};
//context.Employees.Update(e1);
//context.SaveChanges();
//Console.WriteLine(context.Entry(e1).State);

#endregion
#region Birden fazla veri güncellenirken nelere dikkat edilmelidir ?
//EFCoreDbContext context = new();
//var employee = await context.Employees.ToListAsync();
//foreach(var emp in employee)
//{
//    emp.ProductName += "*";
//    //context.SaveChanges(); //maliyet artar birden fazla transaction oluşturur.
//}
//await context.SaveChangesAsync();   
#endregion
#region EF Core Açısından Bir Verinin Güncellenmesi Gerektiği Nasıl Anlaşılıyor ?
//EFCoreDbContext context = new EFCoreDbContext();
//Employee employee = context.Employees.FirstOrDefault(u =>u.Id == 1);
//Console.WriteLine(context.Entry(employee).State);
//employee.ProductName = "Test";

//Console.WriteLine(context.Entry(employee).State);

//context.SaveChanges();

//Console.WriteLine(context.Entry(employee).State);



#endregion
#region Veri Nasıl Silinir ?
//EFCoreDbContext context = new EFCoreDbContext();
//var emp = context.Employees.FirstOrDefault(u=>u.Id== 2);
//context.Employees.Remove(emp);
//context.SaveChanges();
#endregion
#region Silme işleminde ChangeTracker'ın rolü
//Context nesnesi üzerinden gelen verilerin takibinden sorumlu mekanizmadır. Bu takip mekanizması sayesinde context üzerinden gelen verilerle ilgili işlemler neticesinde update yahut delete sorgularının oluşturulacağı anlaşılır.
#endregion
#region Takip edilmeyen nesneler nasıl silinir ? //Yani Contexten gelmeyen
//EFCoreDbContext context = new EFCoreDbContext();
//Employee employee = new()
//{
//    Id = 3
//};
//context.Employees.Remove(employee);
//await context.SaveChangesAsync();   

#endregion
#region EntityState ile silme işlemi
//EFCoreDbContext context = new EFCoreDbContext();
//Employee employee = new() { Id = 4 };
//context.Entry(employee).State = EntityState.Deleted;
//await context.SaveChangesAsync();
#endregion
#region Birden Fazla Veri Silerken Nelere Dikkat Edilmelidir ?
//EFCoreDbContext context = new EFCoreDbContext();
//List <Employee> employee = context.Employees.Where(u => u.Id >= 5 && u.Id <= 7).ToList();
//context.Employees.RemoveRange(employee);
//context.SaveChanges();
#endregion
public class EFCoreDbContext : DbContext  //ETicaretContext diye veri tabanına karşılık gelen bir sınıfım var.
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //konfigürasyon ayarları gerçekleştirilir.
    {
        optionsBuilder.UseSqlServer("Server=Burak; Database=EFCoreDb; Integrated Security=True; TrustServerCertificate=True;");
        //Provider
        //ConnectionString
        //Lazy Loading

    }
    public DbSet<Employee> Employees { get; set; }
}

public class Employee
{
    public int Id { get; set; }
    public string ProductName { get; set; }

}

