// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Reflection;
Console.WriteLine();

ApplicationDbContext context = new();

#region Complex Query Operators

#region Join

#region Query Syntax
//var query = from photo in context.Photos
//            join person in context.Persons
//                on photo.PersonId equals person.PersonId
//            select new
//            {
//                person.Name,
//                photo.Url
//            };
//var datas = await query.ToListAsync();

#endregion
#region Method Syntax

//var query = context.Photos
//    .Join(context.Persons,
//    photo => photo.PersonId,
//    person => person.PersonId,
//    (photo, person) => new
//    {
//        person.Name,
//        photo.Url
//    });
//var datas = await query.ToListAsync();
#endregion

#region Multiple Columns join
#region Method Syntax

//var query = context.Photos
//    .Join(context.Persons,
//        photo => new { photo.PersonId, photo.Url },
//        person => new { person.PersonId, Url = person.Name },
//        (photo, person) => new
//        {
//            person.Name,
//            photo.Url
//        }
//    );

//var datas = await query.ToListAsync();
#endregion
#region Query Syntax
//var query = from photo in context.Photos
//            join person in context.Persons
//            on new { photo.PersonId, photo.Url }
//            equals new { person.PersonId, Url = person.Name }
//            select new // tüm kolonları seçmek içn
//            {
//                person,
//                photo
//            };
//var datas = await query.ToListAsync();
#region Method Syntax

//var query = context.Photos
//    .Join(context.Persons,
//        photo => new { photo.PersonId, photo.Url },
//        person => new { person.PersonId, Url = person.Name },
//        (photo, person) => new
//        {
//            person.Name,
//            photo.Url
//        }
//    );

//var datas = await query.ToListAsync();
#endregion

#endregion
#endregion

#region 2'den fazla tabloyla join
#region Query Syntax
//var query = from photo in context.Photos
//            join person in context.Persons
//             on photo.PersonId equals person.PersonId
//            join order in context.Orders
//            on person.PersonId equals order.PersonId
//            select  new
//            {
//                person.Name,
//                photo.Url,
//                order.Description
//            };
//var datas = await query.ToListAsync();

#endregion
#region Method Syntax
//var query = context.Photos
//            .Join(context.Persons,
//            photo => photo.PersonId,
//            person => person.PersonId,
//            (photo, person) => new
//            {
//                person.Name,
//                photo.Url,
//                person.PersonId
//            })
//            .Join(context.Orders,
//            personPhotos => personPhotos.PersonId,
//            order => order.PersonId,
//            (personPhotos, order) => new
//            {
//                personPhotos.Name,
//                personPhotos.Url,
//                order.Description
//            });
//var datas = await query.ToListAsync();
#endregion

#endregion
#region Groupjoin 
//var query = from person in context.Persons
//            join order in context.Orders
//               on person.PersonId equals order.PersonId into personOrders
//            select new
//            {
//                person.Name, //order'a erişmezsin çünki grupladın, artık personOrders'a erişebilirsin!,
//                Count = personOrders.Count(),
//                personOrders
//            };
//var datas = await query.ToListAsync();
#endregion
#endregion

#region Left join
//var query = from person in context.Persons
//            join order in context.Orders
//            on person.PersonId equals order.PersonId into personOrders
//            from order in personOrders.DefaultIfEmpty()
//            select new
//            {
//                person.Name,
//                order.Description
//            };
//var datas = await query.ToListAsync();
#endregion

#region Full Join
//var leftjoin = from person in context.Persons
//               join order in context.Orders
//               on person.PersonId equals order.PersonId into personOrder
//               from order in personOrder.DefaultIfEmpty()
//               select new
//               {
//                   person.Name,
//                   order.Description
//               };

//var rightjoin = from order in context.Orders
//                join person in context.Persons
//                on order.PersonId equals person.PersonId into orderPerson
//                from person in orderPerson.DefaultIfEmpty()
//                select new
//                {
//                    person.Name,
//                    order.Description
//                };

//var fullJoin = leftjoin.Union(rightjoin);

//var datas = await fullJoin.ToListAsync();

#endregion

#region Cross Join
//var query = from order in context.Orders
//            from person in context.Persons
//            select new
//            {
//                order,
//                person
//            };

//var datas = await query.ToListAsync();
#endregion

var query = from order in context.Orders
            from person in context.Persons.Where(p=>p.PersonId == order.PersonId)
            select new 
            {
                order,
                person
            };

var datas = await query.ToListAsync();

#endregion

Console.WriteLine();


public class Photo
{
    public int PersonId { get; set; }
    public string Url { get; set; }
    public Person Person { get; set; }

}
public enum Gender { Man, Woman }

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public Photo Photo { get; set; }
    public ICollection<Order> Orders { get; set; }


}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }
    public Person Person { get; set; }
}


public class ApplicationDbContext : DbContext
{
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("?");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Photo>()
            .HasKey(p => p.PersonId);

        modelBuilder.Entity<Person>()
            .HasOne(p => p.Photo)
            .WithOne(p => p.Person)
            .HasForeignKey<Photo>(p => p.PersonId);

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }

}
