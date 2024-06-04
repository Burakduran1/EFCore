using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
Console.WriteLine();

#region Default Convention
//Default Convention yönteminde bire çok ilişkiyi kurarken foreign key kolonuna karsilik gelen bir property tanimlamak mecburiyetinde degiliz. Eger tanimlamazsak EF core bunu kendisi tamamlayacak yok eger tanimlarsak, tanimladigimizi baz alacaktir.
//public class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }
//    public Departman Departman { get; set; }
//}

//public class Departman
//{
//    public int Id { get; set; }
//    public string DepartmanAdi { get; set; }
//    public ICollection<Calisan> Calisanlar { get; set; }
//}
#endregion

#region DataAnnotations
//Default convention yonteminde foreign key kolonuna karsilik gelen property'i tanimladigimizda bu property ismi temel gelenksel entity tanimlama kurallarina uymuyorsa eger Data Annotations'lar ile müdahele edebiliriz.
//public class Calisan
//{
//    public int Id { get; set; }
//    [ForeignKey(nameof(Departman))]
//    public int DId { get; set; }
//    public string Adi { get; set; }
//    public Departman Departman { get; set; }
//}

//public class Departman
//{
//    public int Id { get; set; }
//    public string DepartmanAdi { get; set; }
//    public ICollection<Calisan> Calisanlar { get; set; }
//}
#endregion

#region Fluent API
public class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public Departman Departman { get; set; }
}

public class Departman
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }
    public ICollection<Calisan> Calisanlar { get; set; }
}
#endregion


public class EFCoreDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<Departman> Departmanlar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=Burak; Database=EFCoreDb; Integrated Security=True; TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.Departman)
            .WithMany(c => c.Calisanlar);
    }

}





