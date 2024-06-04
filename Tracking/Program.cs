// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

EFCoreDbContext context = new EFCoreDbContext();

#region AsNoTracking Metodu
//Context nesnesi üzerinden gelen tüm datalar Change Tracker mekanizması tarafından takip edilmektedir.

//ChangeTracker, takip ettiği nesnelerin sayısıyla doğru orantılı olacak şekilde bir maliyete sahiptir. O yüzden üzerinde işlem yapılmayacak verilerin takip edilmesi bizlere lüzumsuz yere bir maliyet ortaya çıkaracaktır.

//AsNoTracking metodu, context üzerinden sorgu neticesinde gelecek olan verilerin ChangeTracker tarafından takip edilmesini engeller.

//AsNoTracking metodu ile Change Tracker'ın ihtiyaç olmayan verilerde ki maliyetini törpülemiş oluruz.

//AsNoTracking fonksiyonu ile yapılan sorgulamalarda, verileri elde edebilir, bu verileri istenilen noktalarda kullanabilir lakin veriler üzerinde herhangi bir değişiklik/update işlemi yapamayız.

//var urunler = await context.Urunler.AsNoTracking().ToListAsync(); //sadece listeleme islermi yapacaksam gerekisiz bir sekilde takip etmeme gerek yok
////Takibi koparma IQueryable kısmında iken yapilir.
//foreach(var urun in urunler)
//{
//    Console.WriteLine(urun.UrunAdi);
//    urun.UrunAdi = $"yeni--{urun.UrunAdi}"; //burada update islemi var.
//}
//await context.SaveChangesAsync(); //Yine ise yaramaz yapılan güncelleme: Çünki AsNoTracking kullanıldı. Peki takip olmadan nasıl güncelleriz verileri ?



//var urunler = await context.Urunler.AsNoTracking().ToListAsync(); 
//foreach (var urun in urunler)
//{
//    Console.WriteLine(urun.UrunAdi);
//    urun.UrunAdi = $"yeni--{urun.UrunAdi}"; 
//    context.Urunler.Update(urun); //Id'lere gore guncelleme gerceklesir.
//}
//await context.SaveChangesAsync(); //Simdi ChangeTracker mekanizması olmadan guncelleme islemini tamamladık.

#endregion


#region AsNoTrackingWithIdentityResolution
//CT(Change Tracker) mekanizması yinelenen verileri tekil instance olarak getirir. Buradan ekstradan bir performans kazancı söz konusudur.

//Bizler yaptığımız sorgularda takip mekanizmasının AsNoTracking metodu ile maliyetini kırmak isterken bazen maliyete sebebiyet verebiliriz.(Özellikle ilişkisel tabloları sorgularken bu duruma dikkat etmemiz gerekiyor.)

//AsNoTracking ile elde edilen veriler takip edilmeyeceğinden dolayı yinelenen verilerin ayrı instancelerde olmasına sebebiyet veriyoruz. Çünkü ChangeTracker mekanizması takip ettiği nesneden bellekte varsa eğer aynı nesneden birdaha oluşturma gereği duymaksızın o nesneye ayrı noktalarda ki ihtiyacı aynı instance üzerinden gidermektedir.

//Böyle bir durumda hem takip mekanizmasının maliyetini ortadan kaldırmak hemde yinelenen dataları tek bir instance üzerinden karşılamak için AsNoTrackingWithIdentityResolution fonksiyonunu kullanabiliriz.


//var urunler = await context.Urunler.Include(u => u.Parcalar).AsNoTracking().ToListAsync();
//var urunler = await context.Urunler.Include(u => u.Parcalar).AsNoTrackingWithIdentityResolution().ToListAsync();

//AsNoTrackingWithIdentityResolution fonksiyonu AsNoTracking fonksiyonuna nazaran görece yavaştır/maliyetlidir. Lakin CT'a nazaran daha performanslı ve az maliyetlidir.

#endregion

#region AsTracking
//Context üzerinden gelen dataların CT tarafından takipm edilmesini iradeli bir şekilde ifade etmemizi sağlayan fonksiyondur.
//Niye kullanalım ?
//Bir sonraki inceleyeceğimiz UseQueryTrackingBehavior metodunun davranışı gereği uygulama seviyesinde CT'ın default olarak devrede olup olmamasını ayarlıyor olacaağız. Eğer ki default olarak pasif hale getirilirse böyle durumda takip mekanizmasının ihtiyaç olduğu sorgularda AsTracking fonksiyonunu kullanabilir ve böylece takip mekanizmasını iradeli bir şekilde devereye sokmuş oluruz.

var urunler = await context.Urunler.AsTracking().ToListAsync();

#endregion

#region UseQueryTrackingBehavior
//EFCore seviyesinde/uygulama sayesinde ilgili contextten gelen verilerin üzerinde CT mekanizmasının davranışı temel seviyede belirlememizi sağlayan fonksiyondur. Yani konfigürasyon fonksiyonudur.
#endregion





Console.WriteLine();

public class EFCoreDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=Burak; Database=EFCoreDb; Integrated Security=True;TrustServerCertificate=True;");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); // normalde default olarak AllTrackingdir. Bu kod satırı ile artık default olarak nesnelerin takip edilmeyecegini bildiriyoruz.
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrunParca>().HasKey(up => new { up.UrunId, up.ParcaId });
    }

    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }
    public DbSet<UrunParca> UrunParca { get; set; }

}




public class Urun
{
    public Urun() => Console.WriteLine("Urun nesnesi olusturuldu");
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }
    public ICollection<Parca> Parcalar { get; set; }

}

public class Parca
{
    public Parca() => Console.WriteLine("Parça Nesnesi Oluşturuldu");
    public int Id { get; set; }
    public string ParcaAdi { get; set; }
}

public class UrunParca
{
    public int UrunId { get; set; }
    public int ParcaId { get; set; }

    public Urun Urun { get; set; }
    public Parca Parca { get; set; }
}
