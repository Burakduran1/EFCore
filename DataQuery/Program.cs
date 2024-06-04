using Microsoft.EntityFrameworkCore;
EFCoreDbContext context = new();
#region En temel basit bir sorgulama nasıl yapılır ?
#region Method Syntax
//var value = await context.Urunler.ToListAsync(); //Urunler dbset uzerinden...
#endregion
#region Query Syntax
//var value2 = (from burak in context.Urunler
//              select burak).ToList();
#endregion
#endregion

#region IQueryable ve IEnumrable nedir ? Basit Olarak
//var value = await (from urun in context.Urunler
//            select urun).ToListAsync();
#region IQueryable
//Sorguya Karşılık gelir.
//Ef Core üzerinden yapılmış olan sorgunun execute edilmemiş halini ifade eder.
#endregion
#region IEnumerable 
//Sorgunun çalıştırılıp/execute edilip verilerin in memorye yüklenmiş halini ifade eder.
#endregion
#endregion

#region Sorguyu Execute Etmek için ne yapmamız gerekmektedir ?
//bu kod parçasını breakpoint ile geçtiğinde çalışmış olmayacak, bunu execute eden tetikleyen birşey olamalı.Ne zamanki execute edilir işte o zaman IQueryable --> IEnumerable'a dönüşür. Bu yüzden dışarıdan değer alırken son değeri baz alır. Yani a'nın başta değeri 1 iken sırayla çalışma mantığında sorguya a'nın değeri 1 girmesi gerekirken eğer   sorgu execute edilmediyse son değer neyse onu baz alır.
//int a = 1;
//var value = (from urun in context.Urunler 
//            where urun.Id >a 
//             select urun); 
//a = 2;

//foreach(Urun urun in value)
//{
//    Console.WriteLine(urun.UrunAdi);
//}

//var value = (from urun in context.Urunler //bu query bu hali IQueryable iken aşşağıdaki forecah içerinde çağırınc IEnumrable olmuş olur
//            select urun);                  //bu olaya da Deffered Execution(Ertelenmiş Çalışma)
#region Foreach
//foreach(Urun urun in value)
//{
//    Console.WriteLine(urun.UrunAdi);
//}
#endregion
#region ToListAsync sorguyu execute etmek için kullanılabilir.
//var value = await context.Urunler.ToListAsync();
#endregion
#endregion

#region Çoğul veri getiren sorgulama fonksiyonları
#region ToListAsync()
//Üretilen Sorguyu Execute Ettirmemizi sağlayan fonksiyondur. IQueryableden IEnumrable geçiş yapmamızı sağlar.
#region Method Syntax
//var urunler = await context.Urunler.ToListAsync();
#endregion
#region Query Syntax
//var urunler = await (from urun in context.Urunler
//           select urun).ToListAsync(); ya da
//var urunler = (from urun in context.Urunler
//               select urun);
//var datas = await urunler.ToListAsync();
#endregion
#endregion

#region Where
//Oluşturulan sorguya where şartını eklememizi sağlayan fonksiyondur.
#region Method Syntax
//var urunler = await context.Urunler.Where(u => u.UrunAdi.StartsWith("F")).ToListAsync();
//Console.WriteLine(urunler);
#endregion
#region Query Syntax
//var urunler = (from urun in context.Urunler
//               where urun.Id > 2 || urun.UrunAdi.EndsWith("w")
//               select urun);
//var data = await urunler.ToListAsync();
//Console.WriteLine();
#endregion
#endregion

#region OrderBy (Ascending artan)
#region Method Syntax
//var urun = context.Urunler.Where(u => u.Id > 1 || u.UrunAdi.StartsWith("B")).OrderByDescending(u => u.Id).ToList();
//Console.WriteLine(urun);

#endregion
#region Query Syntax
//var value = from urun in context.Urunler
//            where urun.Id > 1 || urun.UrunAdi.StartsWith("B")
//            orderby urun.UrunAdi descending
//            select urun;
//var deger = value.ToList();
//Console.WriteLine(deger);
#endregion
#region ThenBy
//OrderBy üzerinde yapılan sıralama işlemini farklı kolonlarada uygulamamızı sağlayan bir fonksiyondur.(Ascending)
//var value = context.Urunler.Where(u => u.Id > 1 || u.UrunAdi.EndsWith("W")).OrderBy(u => u.UrunAdi).ThenBy(u => u.Fiyat).ThenBy(u => u.Id);//öncelik ürün adı daha sonra fiyat ondan sonrada ID
//Console.WriteLine(value.ToList());
#endregion
#endregion

#region OrderBy (Descending azalan)
//Descending olarak sırlama yapmamızı sağlayan fonksiyondur.
#region Method Syntax
//var value = context.Urunler.Where(u => u.Id > 1).OrderByDescending(u => u.Id);
//var output = value.ToList();
//Console.WriteLine(output);
#endregion
#region QuerySyntax
//var value = from urun in context.Urunler
//            where urun.Id > 2
//            orderby urun.Id descending
//            select urun;
//var output = value.ToList();
//Console.WriteLine(output);

#endregion
#endregion
#endregion

#region Tekil Veri Getiren Sorgulama Fonksiyonları
//Yapılan Sorguda sade ve sadece tek bir verinin gelmesi amaçlanıyorsa Single ya da SingleOrDefault fonksiyonları kullanılabilir.
#region SingleAsync 
//Eğer ki sorgu neticesinde birden fazla veri geliyorsa ya da hiç gelmiyorsa her iki durumda da exception fırlatır.
#region Tek kayıt geldiğinde
//var value = await context.Urunler.SingleAsync(u => u.Id > 3);
//Console.WriteLine(value);
#endregion
#region Birden fazla kayıt geldiğinde
//var value = await context.Urunler.SingleAsync(u => u.Id > 2);
//Console.WriteLine(value);
#endregion
#region Hiç Kayıt gelmediğinde
//var value = context.Urunler.Single(u => u.Id == 55);
//Console.WriteLine(value);
#endregion

#endregion

#region SingleOrDefaultAsync
//Eğer ki, sorgu neticesinde birden fazla veri geliyorsa exception   fırlartır, hiç veri gelmiyorsa null döner.
#region Tek kayıt geldiğinde
//var value = await context.Urunler.SingleOrDefaultAsync(u => u.Id > 3);
//Console.WriteLine(value);
#endregion
#region Birden fazla kayıt geldiğinde
//var value = await context.Urunler.SingleOrDefaultAsync(u => u.Id > 2);
//Console.WriteLine(value);
#endregion
#region Hiç Kayıt gelmediğinde
//var value = context.Urunler.SingleOrDefault(u => u.Id == 55);
//Console.WriteLine(value);
#endregion
#endregion

#region FirstAsync
//Yapılan sorguda tek bir verinin gelmesi amaçlanıyorsa First ya da FirstOrDefault fonksiyonları kullanılabilir. Single da ise uniq olması gerekir. Burada öyle bir durum yok. Yani ne kadar veri getiriyorsa getirsin önemi yok.
//FirstAsync yapılan sorgu neticesinde elde edilen verilerin ilkini getirir. Eğer ki hiç veri gelmiyorsa hata fırlatır.
#region Tek kayıt döndürür, birden fazla değer olsa bile
//var value = await context.Urunler.FirstAsync(u => u.Id > 2);
//Console.WriteLine(value);
#endregion
#region Birden fazla kayıt geldiğinde
//var value = await context.Urunler.FirstAsync(u => u.Id > 2);
//Console.WriteLine(value);
#endregion
#region Hiç Kayıt gelmediğinde
//var value = context.Urunler.First(u => u.Id == 55); hata alırız
//Console.WriteLine(value);
#endregion
#endregion

#region FirstOrDefaultAsync
//Sorgu neticesinde elde edilen verilerden ilkini getirir. Eğer ki hiç veri gelmiyorsa default olarak null degerini dondurur.
#region Tek kayıt döndürür, birden fazla değer olsa bile
//var value = await context.Urunler.FirstOrDefaultAsync(u => u.Id > 2);
//Console.WriteLine(value);
#endregion
#region Birden fazla kayıt geldiğinde
//var value = await context.Urunler.FirstOrDefaultAsync(u => u.Id > 2);
//Console.WriteLine(value);
#endregion
#region Hiç Kayıt gelmediğinde
//var value = context.Urunler.FirstOrDefault(u => u.Id == 55);  //hata almayız!
//Console.WriteLine(value);
#endregion
#endregion

#region SingleAsync, SingleOrDefaultAsync, FirstAsync, FirstOrDefaultAsync Karşılaştırması

#endregion

#region FindAsync 
//Find fonksiyonu, primary key kolonuna özel hızlı bir şekilde sorgulama yapmamızı sağlayan bir fonksiyondur.
/*DbContext nesnesi içinde zaten izlenmekte olan (tracked) varlıklar arasında arama yapar. Yani, daha önce bu bağlamda alınmış ve değişiklik izleme mekanizması tarafından takip edilen varlıkları kontrol eder.Veritabanı Sorgusu (Database Query): Eğer aranan varlık bellekte bulunamazsa, FindAsync veritabanına bir sorgu gönderir ve belirtilen ana(key) değeri ile eşleşen varlığı veritabanında arar.*/
//Kayıt bulunmazsa null degeri döndürür
/*Urun urun = context.Urunler.FirstOrDefault(u => u.Id == 4);*/ //böyle uzatmadan bulmak icin FindAsync kullanılır.
                                                                //Urun urun = await context.Urunler.FindAsync(4);
                                                                //Console.WriteLine(urun);
#region Composite Primary Key Durumu
//UrunParca urun = await context.UrunParca.FindAsync(3,1);
//Console.WriteLine(urun);
#endregion
#endregion

#region FindAsync ile SingleAsync, SingleOrDefaultAsync, FirstAsync, FirstOrDefaultAsync fonksiyonlarının karşılaştırılması
#endregion

#region LastAsync
//First ve FirstOrDefaultun tam tersidir. Lakin Last yada LastOrDefault kullanırken orderby kullanmamız gerekir. Çok kullanılmaz bu fonk. Eğer ki hiç veri gelmiyorsa hata fırlatır.   
//var urun = await context.Urunler.OrderBy(u=>u.UrunAdi).LastAsync(u => u.Id > 2);
//Console.WriteLine(urun);
#endregion

#region LastOrDefaultAsync
//First ve FirstOrDefaultun tam tersidir. Lakin Last yada LastOrDefault kullanırken orderby kullanmamız gerekir. Çok kullanılmaz bu fonk. Eğer ki hiç veri gelmiyorsa null döner.
//var urun = await context.Urunler.OrderBy(u => u.UrunAdi).LastOrDefaultAsync(u => u.Id > 2);
//Console.WriteLine(urun);
#endregion
#endregion

#region Diğer Sorgulama Fonksiyonları 
#region CountAsync 
//Oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini sayısal olarak (int) bizlere bildiren fonksiyondur.
//var urunler = (await context.Urunler.ToListAsync()).Count(); // bu işlem maliyetli olur. Gider tüm ürünleri sorgular daha sonra bellekte 
//Console.WriteLine(urunler);                                 //count ile sayısını hesaplar. ToList eklemeden daha az maliyetle hesaplanmalı.
//var urunler =  await context.Urunler.CountAsync();
//Console.WriteLine(urunler);
#endregion
#region LongCountAsync
//Oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini sayısal olarak (Long) bizlere bildiren fonksiyondur.
//var urunler = await context.Urunler.LongCountAsync();
//Console.WriteLine(urunler);
//var urunler = await context.Urunler.LongCountAsync(u=>u.Fiyat>1500); //şartlama da yapılabilir
//Console.WriteLine(urunler);

#endregion
#region AnyAsync
//Sorgu neticesinde verinin gelip gelmediğini bool türünde dönen fonksiyondur.
//var urunler = await context.Urunler.AnyAsync();
//Console.WriteLine(urunler);
//var urunler = await context.Urunler.AnyAsync(u => u.Id > 100);
//Console.WriteLine(urunler);
//var urunler = await context.Urunler.Where(u => u.UrunAdi.Contains("a")).AnyAsync();
//Console.WriteLine(urunler);
#endregion
#region MaxAsync
//var fiyat = await context.Urunler.MaxAsync(u => u.Fiyat); //en yuksek fiyat
//Console.WriteLine(fiyat);
#endregion
#region MinAsync
//var fiyat = await context.Urunler.MinAsync(u => u.Fiyat);
//Console.WriteLine(fiyat);
#endregion
#region Distinct
//Sorguda mükerrer kayıtlar varsa  bunları tekileştiren bir işleve sahip fonksiyondur. Distinct execute edebilmek için ToList kullanılmalı!
/*var value = await context.Urunler.Distinct().ToListAsync();*/ //Çünkü Distinct fonk. geriye IQueryable  döner.
                                                                //Console.WriteLine(value);
#endregion
#region AllAsync
// Bir sorgu neticesinde gelen verilerin, verilen şarta uyup uymadığını kontrol etmektedir. Eğer ki tüm veriler şarta uymuyorsa true, uymuyorsa false döndürecektir.
//var value = await context.Urunler.AllAsync(u => u.Fiyat > 0); // tüm ürünlerin fiyatı 0 dan büyükse true 
//Console.WriteLine(value);
//var value = await context.Urunler.AllAsync(u=>u.UrunAdi.Contains("a")); //tüm ürün adları içinde 'a' geçiyor mu ?
//Console.WriteLine(value);
#endregion
#region SumAsync 
//Vermiş olduğumuz sayısal propertynin toplamını alır.
//var value = await context.Urunler.SumAsync(u => u.Fiyat);
//Console.WriteLine(value);
//#endregion
#endregion
#region AverageAsync
//var value = await context.Urunler.AverageAsync(u => u.Fiyat);
//Console.WriteLine(value);
#endregion
#region ContainsAsync
//Like '%...%' Sorgusu oluşturmamızı sağlar. Bunu where ile yapmamız gerekiyor.
//var value = await context.Urunler.Where(u => u.UrunAdi.Contains("a")).ToListAsync();
//Console.WriteLine(value);
#endregion
#endregion

#region Sorgu Sonucu Dönüşüm Fonksiyonları
// Bu fonksiyonlar ile sorgu neticesinde elde edilen verileri isteğimiz doğrultusunda farklı türlerde projeksiyon edebiliyoruz.
#region ToDictionaryAsync
// Sorgu neticesinde gelecek olan veriyi bir dictionary olarak elde etmek/tutmak/karşılamak istiyorsak eğer kullanılır!

//var value = await context.Urunler.ToDictionaryAsync(u=>u.Id, u=>u.Fiyat); //key-value formatında tutmamızı sağlar.
//Console.WriteLine(value);

//ToList ile aynı amaca hizmet etmektedir. Yani oluşturulan sorguyu execute edip neticesini alırlar.
//ToList : Gelen sorgu neticesinde entity türünde bir koleksiyona(List<TEntity>) dönüştürmekteyken, ToDictionary ise : Gelen sorgu neticesini Dictionary türünden bir koleksiyona dönüştürecektir.
#endregion
#region ToArrayAsync
//Oluşturlan sorguyu dizi olarak ele alır.
//ToList ile muadil amaca hizmet eder. Yani sorguyu execute eder lakin gelen sonucu entity dizisi olarak elde eder.
//var urunler = await context.Urunler.ToArrayAsync();
//Console.WriteLine(urunler);
#endregion
#region Select
//Select fonksiyonun işlevsel olarak birden fazla davranışı söz konusudur.
//1. Select fonksiyonu, generate edilecek sorgunun çeklilecek kolonlarını ayarlamamızı sağlar.
//var urunler = await context.Urunler.Select(u=>new Urun
//{
//    Id= u.Id,
//    Fiyat=u.Fiyat
//}).ToListAsync();
//Console.WriteLine(urunler);

//2. Select fonksiyonu, gelen verileri farklı türlerde karşılamamızı sağlar. T; anonim
//var urunler = await context.Urunler.Select(u => new 
//{
//    Id = u.Id,
//    Fiyat = u.Fiyat
//}).ToListAsync();
//Console.WriteLine(urunler);

//var d = new Urun(); // bu bir ürün nesnesidir.
//var a = new // bu ise tipsiz anonim bir nesnedir.
//{
//    A = "Burak"
//};

#endregion
#region SelectMany
// Select ile aynı amaca hizmet eder. Lakin, ilişkisel tablolar neticesinde gelen koleksiyonel verileri de tekilleştirip projeksiyon etmemizi sağlar.   
//var urunler = await context.Urunler.Include(u=>u.Parcalar).SelectMany(u => u.Parcalar, (u,p)=> new
//{
//    u.Id,
//    u.Fiyat,
//    p.ParcaAdi
//}).ToArrayAsync();
//Console.WriteLine(urunler);
#endregion

#endregion

#region GroupBy fonksiyonu
//Gruplama yapmamızı sağlayn fonksiyondur.
#region Method Syntax
//var datas = await context.Urunler.GroupBy(d => d.Fiyat).Select(group => new
//{
//    Count = group.Count(),
//    Fiyat = group.Key
//}).ToListAsync();
//Console.WriteLine(datas);
#endregion
#region Query Syntax
//var datas = await (from urun in context.Urunler //ürünler içerisinedki her bir ürün urun ile temsil edilir.
//            group urun by urun.Fiyat //ürün içindeki hangi kolon gruplanacak ?
//            into @group
//            select new
//            {
//                Fiyat = @group.Key,
//                Group = @group.Count()

//            }).ToListAsync();
//Console.WriteLine(datas);

#endregion

#endregion

#region ForEach Fonksiyonu
// Bir sorgulama fonksiyonu felan değildir.
//Sorgulama neticesinde elde edilen koleksiyonel veriler üzerinde iterasyonel olarak dönmemizi ve teker teker verileri elde edip işlemler yapabilmemizi sağlayan bir fonksiyondur. Foreach döngüsünün metot halidir.

//var datas = await (from urun in context.Urunler //ürünler içerisinedki her bir ürün urun ile temsil edilir.
//                   group urun by urun.Fiyat //ürün içindeki hangi kolon gruplanacak ?
//            into @group
//                   select new
//                   {
//                       Fiyat = @group.Key,
//                       Group = @group.Count()

//                   }).ToListAsync();
//datas.ForEach(x =>
//{
//    Console.WriteLine(x.Fiyat);
//});
#endregion

public class EFCoreDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=Burak; Database=EFCoreDb; Integrated Security=True;TrustServerCertificate=True;");
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
