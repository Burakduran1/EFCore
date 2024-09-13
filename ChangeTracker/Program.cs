using Microsoft.EntityFrameworkCore;

EFCoreDbContext context = new EFCoreDbContext();

#region Change Tracking nedir ?
//Context nesnesi üzerinden gelen tüm nesneler/veriler otomatik olarak bir takip mekanizması tarafından izlenirler.İşte bu takip mekanizmasına Change Tracker ile nesneler üzerindeki değişiklikler/işlemler takip edilerek netice itibariyle bu işlemlerin fıtratına uygun sql sorgucukları generate edilir. İşte bu işleme de Change Tracking denir.
#endregion



#region Entry Metodu
#region OriginalValues Property'si
//var fiyat = context.Entry(urun).OriginalValues.GetValue<float>(nameof(Urun.Fiyat));
//var isim = context.Entry(urun).OriginalValues.GetValue<string>(nameof(Urun.UrunAdi));

#endregion
#region CurrentValues Propertysi
//Mevcut degerin heapteki karşılığını getirdi.
//var urunadi = context.Entry(urun).CurrentValues.GetValue<string>(nameof(Urun.UrunAdi));
//Console.WriteLine();
#endregion
#region GetDatabaseValues Metodu
#endregion
#endregion

#region ChangeTracker Propertysi
// Context üzerinden takip edilen nesnelere erişebilmemizi sağlayan ve gerektiği taktirde işlemler gerçekleştirmemizi sağlayan bir propertydir.
// Context sınıfının base class'ı olan DbContext sınıfının bir member'ıdır.

//var urunler = await context.Urunler.ToListAsync();
//urunler[1].Fiyat = 123; //Update
//context.Urunler.Remove(urunler[4]); //Delete
//var datas = context.ChangeTracker.Entries();

//context.SaveChanges();

//Console.WriteLine();
#endregion

#region DetectChanges Metodu
//EF Core, context nesnesi tarafından izlenen tüm nesnelerdeki değişiklikleri Change Tracker sayesinde takip edebilmekte ve nesnelerde olan verisel değişiklikler yakalanarak bunların anlık görüntüleri(snapshot)'ini oluşturabilir.
//Yapılan değişikliklerin veri tabanına gönderilmeden önce algılandığından emin olmak gerekir. SaveChanges fonksiyonu çağrıldığı anda nesneler EF core  tarafından otomatik kontrol edilirler.
//Ancak, yapılan operasyonlarda güncel tracking verilerinden emin olabilmek için değişikliklerin algılanmasını opsiyonel gerçekleştirmek isteyebiliriz. İşte bunun için DetectChanges fonksiyonu kullanılabilir ve her ne kadar EF core değişikleri otomatik algılıyor olsa da siz yine de iradenizle kontrole zorlayabilirsiniz.

//var urun =  await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);

//urun.Fiyat = 44;

//context.ChangeTracker.DetectChanges();

//context.SaveChanges();
#endregion

#region AutoDetectChangesEnabled Property'si
//İlgili metotlar(SaveChanges, Entries) tarafından DetectChanges metodunun otomatik olarak tetiklenmesinin konfigürasyonunu  yapmamızı sağlayn propertydir.
//SaveChanges fonksiyonu tetiklendiğinde DetectChanges metodunu içerisine default olarak çağırmaktadır. Bu durumda DetectChanges fonksiyonun kullanımını irademizle yönetmek ve maliyet/performans optimizasyonu yapmak istediğimiz durumlarda AutoDetectChangesEnabled özelliğini kapatabiliriz.

#endregion

#region Entries Metodu
//Contexte ki entry metodunun koleksiyonel versiyonudur.
//Change tracker mekanizması tarafından izlenen her entity nesnesinin bilgisini EntityEntry türünden elde etmemizi sağlar ve belirli işlemler yapabilmemize olanak sağlar.
//Entries metodu DetectChanges metodunu tetikler. Bu durum da tıpkı SaveChanges da olduğu gibi bir maliyettir.
//Burada ki maliyetten kaçınmak için AutoDetectChangesEnabled özelliğine false değeri verilebilir.

//var value = await context.Urunler.ToListAsync();
//value.FirstOrDefault(u => u.Id == 1).Fiyat = 44;
//context.Urunler.Remove(value.FirstOrDefault(u => u.Id == 2));
//value.FirstOrDefault(u => u.Id == 3).UrunAdi = "Tofaşk";

//context.ChangeTracker.Entries().ToList().ForEach(e =>
//{
//    if(e.State == EntityState.Unchanged)
//    {

//    }
//    else if (e.State == EntityState.Deleted)
//    {

//    }
//});



#endregion

#region AcceptAllChanges Metodu
//SaveChanges() ve ya SaveChanges(true) tetiklendiğinde EF core herşeyin yolunda olduğunu varsayarak track ettiği verilerin takibini keser yeni değişikliklerin takip edilmesini bekler. Böyle bir durumda beklenmeyen bir durum/olası bir hata söz konusu olursa eğer EF Core takip ettiği nesneleri bırakacağı için bir düzeltme mevzu bahis olamayacaktır.

//Haliyle bu durumda devreye SaveChanges(false) ve AcceptAllChanges metotları girecektir.

//SaveChanges(false) , EF Core'a gerekli veritabanı komutlarını yürütmesini söyler ancak gerektiğinde yeniden oynatılabilmesi için değişiklikleri beklemeye/nesneleri takip etmeye devam eder. Taa ki AcceptAllChanges() metodunu irademizle çağırana kadar.

//SaveChanges(false) ile işlemin başarılı olduğundan emin olursanız  AcceptAllChanges() metodu ile nesnelerden takibi kesebilirsiniz.
//var value = await context.Urunler.ToListAsync();
//value.FirstOrDefault(u => u.Id == 1).Fiyat = 44; //update
//context.Urunler.Remove(value.FirstOrDefault(u => u.Id == 2)); //delete
//value.FirstOrDefault(u => u.Id == 3).UrunAdi = "Tofaş"; // update

//context.SaveChangesAsync(false);
//context.ChangeTracker.AcceptAllChanges();

#region HasChanges Metodu
#endregion


#endregion

#region HasChanges metodu
//Takip edilen nesneler arasında değişiklik yapılanların olup olmadığınının bilgisini verir.
//Arkaplanda DetectChanges metodunu tetikler.

//var result = context.ChangeTracker.HasChanges();
#endregion


#region Entity States
//Entity nesnelerinin durumlarını ifade eder.
#region Detached
//Nesnenin change tracker mekanizması tarafından takip edilmediğini ifade eder.
//Urun urun = new();
//Console.WriteLine(context.Entry(urun).State);
//urun.UrunAdi = "sdsdsd";
//await context.SaveChangesAsync();
#endregion

#region Added
//Veri tabanına eklenecek nesneyi ifade eder. Added henüz veri tabanına işlenmeyen veriyi ifade eder. SaveChanges() fonksiyonu çağrıldığında insert sorugusu oluşturulacağı anlamına gelir.
//Urun urun = new()
//{
//    UrunAdi = "Ferrari",
//    Fiyat = 2563
//};
//Console.WriteLine(context.Entry(urun).State); //detached
//await context.Urunler.AddAsync(urun);//bununla birlikte takip mekanizması baslar ve bundan sonra devam eder.
//Console.WriteLine(context.Entry(urun).State); //added
//context.SaveChanges();
//Console.WriteLine(context.Entry(urun).State); //unchanged

#endregion

#region Unchanged
//Veritabanından sorgulandığından beri nesne üzerinde herhangi bir değişiklik yapılmadığını ifade eder. Sorgu neticesinde elde edilen tüm nesneler başlangıçta bu state değerindedir.
//var urunler = await context.Urunler.ToListAsync();

//var data = context.ChangeTracker.Entries();
//Console.WriteLine();
#endregion

#region Modified
//Nesne üzerinde güncelleme yapılcağını ifade eder. SaveChanges() fonksiyonu çağırıldığında     update sorgusu oluşturacağı anlamına gelir.

//var urun = await context.Urunler.FirstOrDefaultAsync(u=>u.Id==3 );
//Console.WriteLine(context.Entry(urun).State);
//urun.Fiyat = 4785;
//Console.WriteLine(context.Entry(urun).State);
//await context.SaveChangesAsync();// parametresiz yani true hali ile çağırıyorsak tracking mekanizmasını boşaltır, nesneleri takip etmekten vaz geçer. Bir sonraki değişikliğe odaklı davranış sergiler.
//Console.WriteLine(context.Entry(urun).State);

//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
//Console.WriteLine(context.Entry(urun).State);
//urun.Fiyat = 5485;
//Console.WriteLine(context.Entry(urun).State);
//await context.SaveChangesAsync(false); // daha takip edilen nesneler bırakılmamış hala modified olarak tanımlanıyor. AcceptAllChanges metodu ile manuel şekilde düzenlenir.
//Console.WriteLine(context.Entry(urun).State);



#endregion

#region Deleted
//Nesnenin silindiğini ifade eder. SaveChanges fonksiyonu çağırıldığında delete sorgusu oluşturulacağı anlamına gelir.
//var urun = await context.Urunler.FirstOrDefaultAsync(u=>u.Id == 6);
//Console.WriteLine(context.Entry(urun).State);
//context.Urunler.Remove(urun);
//Console.WriteLine(context.Entry(urun).State);
//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(urun).State);
#endregion

#endregion

#region Context nesnesi üzerinden Change Tracker
//context.Entry(e).State // O anki nesnenin statelerini kontrol edebiliriz.
//context.ChangeTracker.Entries() //çoğuli

//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
//urun.Fiyat = 625; //modified
//urun.UrunAdi = "Hyundai"; //modified

#endregion

#region Change Tracker'ın Interceptor Olarak Kullanılması

#endregion























public class EFCoreDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("?");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrunParca>().HasKey(up => new { up.UrunId, up.ParcaId });
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) //veri tabanına gönderilmeden önce araya girip operasyon gerçekleştirmek istiyorsak, bu şekilde SaveChanges ovveride edilebilir.
    {
         var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if(entry.State == EntityState.Added)
            {

            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }
    public DbSet<UrunParca> UrunParca { get; set; }

}




public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }
    public ICollection<Parca> Parcalar { get; set; }

}

public class Parca
{
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
