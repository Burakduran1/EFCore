using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
Console.WriteLine();

#region Default Convention
// her iki entity'de navigation property ile birbirlerini tekil olarak referans ederek fiziksel bir iliskinin olacagi ifade edilir.
// One to One iliski turunde, dependent entity'nin hangisi oldugunu default olarak belirleyebilmek  pek kolay degildir. Bu durumda fiziksel olarak bir foreign key'e karsilik property/kolon tanımlayarak cozum getirebiliyoruz.
//Boylece foreign key'e karsilik property tanımlayarak luzumsuz bir kolon olusturmus oluyoruz.
//public class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; } //navigation property // Ilgili tur entity turunden ise navigation propertydir.
//}
//public class CalisanAdresi //CalisanAdresi Calisan entitysine baglidir. Yani calisanadrsi dependent entity.
//{
//    public int Id { get; set; }
//    public int CalisanId { get; set; } //foreign key
//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; }
//}
#endregion

#region DataAnnotations
//navigation propertyler tanımlanmalıdır.
//Foreign Key kolonunun ismi default convention'ın dışında bir kolon olacaksa eğer ForeignKey attribute ile bunu bildirebiliriz.
//Foreign key kolonu oluşturmak zorunda değiliz.
//1'e 1 ilişkide ekstardan foreign key kolonuna ihtiyaç olmayacağından dolayı dependent entity'deki Id kolonu hem foreign key hem de Primary key olarak kullanmayı tercih ediyoruz. Bu duruma özen gösterilmelidir.
//public class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; } //navigation property // Ilgili tur entity turunden ise navigation propertydir.
//}
//public class CalisanAdresi //CalisanAdresi Calisan entitysine baglidir. Yani calisanadrsi dependent entity.
//{
//    [Key, ForeignKey(nameof(Calisan))]
//    public int Id { get; set; }

//    //[ForeignKey(nameof(Calisan))] //navigation property ismi verilir.
//    //public int CalisanId { get; set; }
//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; }
//}
#endregion

#region Fluent API
//navigation propetyler tanımlanmalı 
//Fluent API yonteminde entity'ler arasındaki iliski context sınıfı içerisinde OnModelCreating fonkisyonun overrid edilerek metotlar aracılıgıyla tasarlanması gerekmektedir. Kısacası tum sorumluluk bu fonksiyon icerisindeki calismalardadir.

public class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }

    public CalisanAdresi CalisanAdresi { get; set; } //navigation property // Ilgili tur entity turunden ise navigation propertydir.
}
public class CalisanAdresi //CalisanAdresi Calisan entitysine baglidir. Yani calisanadrsi dependent entity.
{

    public int Id { get; set; }
    public string Adres { get; set; }
    public Calisan Calisan { get; set; }
}
#endregion


public class EFCoreDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<CalisanAdresi> CalisanAdresleri { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("?");
    }
    //Model'ların(entity) veritabanında generate edilecek yapıları  bu fonksiyonda içerisinde konfigüre edilir.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalisanAdresi>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.CalisanAdresi)
            .WithOne(c => c.Calisan)
            .HasForeignKey<CalisanAdresi>(c => c.Id); //calisanadresi dependent burada primary key özelliği ezilmiş olur, tekrar yukarıda bunun pk oldugunun bildirmemiz gerekir.
    }

}


