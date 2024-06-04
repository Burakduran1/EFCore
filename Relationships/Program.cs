using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

Console.WriteLine();

#region Relationships(ilişkiler) Terimleri

#region Principal Entity(Asıl Entity)
//Kendi basina var olabilen tabloyu modelleyen entity'e denir.
//Departmanlar tablosunu modelleyen "Departman Entity'sidir"
#endregion 

#region Dependent Entity(Bağımlı Entity)
//Kendi başına var olamayan, bir başka tabloya bağımlı(ilişkisel olarak bagimli) olan tabloyu modelleyen entity'e denir.
//Calisanlar tablosunu modelleyen "Calisan" entity'sidir.
#endregion

#region Foreign Key
//Principal entity ile  Dependent entity arasinda ki iliskiyi saglayan key'dir.
//Dependent Entity'de tanimlanir.
//Principle entity'deki Principal Key'i tutar.
#endregion

#region Principal Key 
//Principal Entity'deki id'nin kendisidir.
//Principal Entity'nin kimligi olan kolonu ifade eden propertydir.
#endregion

#endregion

#region NavigationProperty Nedir ?
//İlişkisel tablolar arasındaki fiziksel erişimi entity class'ları üzerinden saglayan property'lerdir.
//Bir property'nin navigation property olabilmesi icin kesinlikle entity turunden olmasi gerekiyor.
//NavigationProperty'ler entity'lerdeki tanımlarına göre n'e n yahut 1'e n şeklinde ilişki türlerini ifade etmektedirler.
#endregion

#region Relationship types(Iliski Turleri)

#region One to One
//Kari Koca arasindaki iliski
#endregion

#region One to Many     
//Calisan ile departman arasi iliski, anne ve cocuk arasi iliski vs.

#endregion

#region Many to Many
//Calisanlar ile projeler arasindaki iliski
#endregion
#endregion

#region EFCore'da ilişki yapilandirma yontemleri

#region Default Conventions
//Varsayilan entity kurallarini kullanarak  yapilan iliski yapilandirma yontemleridir.

//Navigation Property'leri kullanarak iliski sablonlarini cikarmaktadir.
#endregion

#region  Data Annotations Attributes
// Entity'nin niteliklerine gore ince ayarlar yapamamizi saglayan attribute'lardır. [Key], [ForeignKey]
#endregion

#region Fluent API
//Entity modellerindeki iliskileri yapilandirirken daha detayli calismamizi saglayan yontemdir.

#region HasOne
//İligli entity'nin iliskisel  entity'ye birebir  ya da bire çok olacak şekilde ilişkisini yapılandırmaya baslayan metottur.
#endregion

#region HasMany
//Ilgili entity'nin  iliskisel entity'ye coka bir  ya da coka cok  olacak sekilde iliskisini yapilnadirmaya baslayan metottur.
#endregion

#region WithOne
// HasOne ya da HasMany'den sonra birebir ya da çoka bir olacak sekilde iliski yapilandirmasini tamamlayan metottur.
#endregion

#region WithMany
// HasOne ya da HasMany'den sonra bire çok ya da çoka çok olacak sekilde iliski yapilandirmasini tamamlayan metottur.
#endregion


#endregion

#endregion


//Calisan calisan = new()
//{
//    CalisanAdi = "Burak Can",
//    Departman = ""
//};


public class EFCoreDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=Burak; Database=EFCoreDb; Integrated Security=True;TrustServerCertificate=True;");
    }

    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<Departman> Departmanlar { get; set; }
}


public class Calisan
{
    public int Id { get; set; }
    public string CalisanAdi { get; set; }
    public int DepartmanId { get; set; }

    public Departman Departman { get; set; } //calisanin sadece bir departmani olabilir.
}
public class Departman
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }

    public List<Calisan> Calisanlar { get; set; } //Departmanin ise birden fazla calisani olabilir.
}
