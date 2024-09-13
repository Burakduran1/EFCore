// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new ApplicationDbContext();

#region View Nedir ?
// Oluşturduğumuz complex sorguları ihtiyaç durumlarında daha rahat bir şekilde kullanabilmek için basitleştiren bir veritabanı objesidir.
#endregion

#region EF-Core ile View kullanımı

#region View  Oluşturma
//1. adım : boş bir migration oluşturulmalıdır.
//2. adım: migration içerisindeki up fonk. View'in create komutlarını, down fonk. ise drop komutları yazılmalıdır.
//3. adım: migrate ediniz.
//Migration geri alınırsa, up kısmında nasıl manuel tanımlıyorsak, down kısmında da silmemiz gerekir.
#endregion
#region View'i DbSet Olarak Ayarlama
//View'i EF-Core üzerinden sorgulayabilmek için view neticesini karşılayabilecek bir entity oluşturulması  ve bu entity türünden DbSet property'sinin  eklenmesi gerekmektedir. Column adları aynı olmalı view ile property'lerin.
#endregion
#region DbSet'in bir View Olduğunu Bildirmek

//modelBuilder.Entity<PersonOrder>()
//    .ToView("OrderCount")
//    .HasNoKey();
#endregion
#region EF Core'da View'lerin Özellikleri!
//Viewlerde primary key olmaz! Bu yüzden iligli DbSet'in HasNoKey ile işaretlenmesi gerekmektedir
//View neticesinde gelen veriler Change Tracker ile takip edilmezler. Haliyle üzerlerinde yapılan değişiklikleri EF-Core veri tabanına yansıtmaz.
//var query = context.PersonOrders.First();
//query.Name = "Burak";
context.SaveChanges();
#endregion

//var query = context.PersonOrders
//    .Where(p => p.Adet > 10)
//    .ToList();
Console.ReadLine();
#endregion












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
public class PersonOrder
{
    public string Name { get; set; }
    public int Adet { get; set; }
}

public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PersonOrder> PersonOrders { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("?");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);

        modelBuilder.Entity<PersonOrder>()
            .ToView("OrderCount")
            .HasNoKey();

    }
}