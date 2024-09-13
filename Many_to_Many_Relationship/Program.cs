using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("Hello, World!");





#region Default Convention
//İki entity arasındaki iliskiyi navigation propertyler uzerinden cogul olarak kurmaliyiz. (ICollection, List)
//Default Convetion'da cross table'ı manuel oluşturmak zorunda değiliz. EF core tasarıma uygun bir şekilde cross table'ı otomatik basacak ve generate edecektir.
//Ve oluşturulan cross table içerisinde composit primary key'i otomatik olusturulacaktır.
//public class kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }

//    public ICollection<Yazar> Yazarlar { get; set; }

//}

//public class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    public ICollection<kitap> Kitaplar { get; set; }
//}

#endregion

#region DataAnnotations
//Cross table manuel olarak olusturulmak zorundadır.
// Entity'lerde olusturdugumuz cross table entity si ile bire cok iliski kurulmalı.
//CT'da  composite primary key'i data annotation(Attributes)lar ile manuel kuramıyoruz. Bunun icin de Fluent API'da calisma yapmamiz gerekir.
//Cross table'a karsilik bir entity modeli olusturuyorsak eger  bu context sınıfı icerisinde DbSet property'si olarak bildirmek mecburiyetinde degiliz!
//public class kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }
//    public ICollection<KitapYazar> Yazarlar { get; set; }


//}

//public class KitapYazar
//{
//    //[Key]
//    public int KitapId { get; set; }
//    //[Key]
//    public int YazarId { get; set; }
//    public kitap Kitap { get; set; }
//    public Yazar Yazar { get; set; }
//}

//public class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    public ICollection<KitapYazar> Kitaplar { get; set; }

//}

#endregion

#region Fluent API
//Cross table manuel olusturulmali,
//DbSet olarak eklenmesine luzum yok.
//Composite Pk Haskey metodu ile kurulmalı!
public class kitap
{
    public int Id { get; set; }
    public string KitapAdi { get; set; }
    public ICollection<KitapYazar> Yazarlar { get; set; }


}

public class KitapYazar
{

    public int KitapId { get; set; }
    public int YazarId { get; set; }
    public kitap Kitap { get; set; }
    public Yazar Yazar { get; set; }
}

public class Yazar
{
    public int Id { get; set; }
    public string YazarAdi { get; set; }
    public ICollection<KitapYazar> Kitaplar { get; set; }

}
#endregion


public class EFCoreDbContext : DbContext
{
    public DbSet<kitap> Kitaplar { get; set; }
    public DbSet<Yazar> Yazarlar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=?; Datatbase=?EFCoreDb; Integrated Security=?; TrustServerCertificate=True;");
    }
    //Data Annotations
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<KitapYazar>()
    //        .HasKey(ky => new { ky.KitapId, ky.YazarId });

    //}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KitapYazar>()
             .HasKey(k => new { k.KitapId, k.YazarId });

        modelBuilder.Entity<KitapYazar>()
            .HasOne(ky => ky.Kitap)
            .WithMany(ky => ky.Yazarlar)
            .HasForeignKey(ky => ky.KitapId);

        modelBuilder.Entity<KitapYazar>()
            .HasOne(ky => ky.Yazar)
            .WithMany(y => y.Kitaplar)
            .HasForeignKey(ky => ky.YazarId);

    }

}





