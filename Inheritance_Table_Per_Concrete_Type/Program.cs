﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
Console.WriteLine();

ApplicationDbContext context = new();

#region Table Per Concrete Type (TPC) Nedir?
//TPC davranışı, kalıtımsal ilişkiye sahip olan entitylerin olduğu çalışmalarda sadece concrete/somut olan entity'lere karşılık bir tablo oluşturacak davranış modelidir.
//TPC, TPT'nin daha performanslı versiyonudur.
#endregion
#region TPC Nasıl Uygulanır?
//Hiyerarşik düzlemde abstract olan yapılar üzerinden OnModelCreating üzerinden Entity fonskiyonuyla konfigürasyona girip ardından UseTpcMappingStrategy fonksiyonu eşliğinde davranışın TPC olacağını belirleyebiliriz.
#endregion
#region TPC'de Veri Ekleme
//await context.Technicians.AddAsync(new() { Name = "Gençay", Surname = "Yıldız", Branch = "Yazılım", Department = "Yazılım Departmanı" });
//await context.Technicians.AddAsync(new() { Name = "Mustafa", Surname = "Yıldız", Branch = "Yazılım", Department = "Yazılım Departmanı" });
//await context.Technicians.AddAsync(new() { Name = "Hilmi", Surname = "Yıldız", Branch = "Yazılım", Department = "Yazılım Departmanı" });
//await context.SaveChangesAsync();
#endregion
Technician technician = new Technician() { Name = "Test", Surname = "asa", Branch = "Awa", Department = "HR" };
await context.Technicians.AddAsync(new Technician() { Name = "Ali", Surname = "Duran", Branch = "Wow", Department = "None" });
await context.Technicians.AddAsync(new Technician() { Name = "Ensar", Surname = "Duran", Branch = "Wow", Department = "None" });
await context.SaveChangesAsync();
#region TPC'de Veri Silme
//Technician? silinecek = await context.Technicians.FindAsync(2);
//context.Technicians.Remove(silinecek);
//await context.SaveChangesAsync();
#endregion
#region TPC'de Veri Güncelleme
//Technician? guncellenecek = await context.Technicians.FindAsync(3);
//guncellenecek.Name = "Ahmet";
//await context.SaveChangesAsync();
#endregion
#region TPC'de Veri Sorgulama
//var datas = await context.Technicians.ToListAsync();
//Console.WriteLine();
#endregion
abstract class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}
class Employee : Person
{
    public string? Department { get; set; }
}
class Customer : Person
{
    public string? CompanyName { get; set; }
}
class Technician : Employee
{
    public string? Branch { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Technician> Technicians { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TPT
        //modelBuilder.Entity<Person>().ToTable("Persons");
        //modelBuilder.Entity<Employee>().ToTable("Employees");
        //modelBuilder.Entity<Customer>().ToTable("Customers");
        //modelBuilder.Entity<Technician>().ToTable("Technicians");
        modelBuilder.Entity<Person>().UseTpcMappingStrategy(); //tpc
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("?");
    }
}