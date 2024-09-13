using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new ApplicationDbContext();

#region One to One İlişkisel Senaryolarda Veri Güncelleme
#region Saving
//Person person = new()
//{
//    Name = "Burak Can",
//    Address = new()
//    {
//        PersonAddress = "Yeşilyurt/MALATYA"
//    }
//};
//Person person1 = new()
//{
//    Name = "Ali Rıza"

//};

//await context.AddRangeAsync(person, person1);
//await context.SaveChangesAsync();
#endregion

#region 1. Durum Esas tablodaki veriye bağımlı veriyi değiştirme
//Person? person = await context.Persons
//    .Include(x=>x.Address)
//    .FirstOrDefaultAsync (p=>p.Id==1);

//context.Addresses.Remove(person.Address);

//person.Address = new()
//{
//    PersonAddress = "fjdfudh"
//};
//await context.SaveChangesAsync();   
#endregion

#region 2. Durum Bağımlı Verinin ilişkisel olduğu ana veriyi güncelleme
//Address? address = await context.Addresses.FindAsync(1); //bu sekilde olmaz 
//address.Id = 2;
//await context.SaveChangesAsync();
//Bu hata, Entity Framework Core'da bir entity'nin key (anahtar) özelliğini değiştirmeye çalıştığınızda ortaya çıkar. Entity Framework Core, bir entity'nin key özelliğinin değiştirilmesine izin vermez. Bunun yerine, eğer bir anahtar özelliği değiştirmeniz gerekiyorsa, önce mevcut entity'yi silip değişiklikleri kaydetmeniz ve ardından yeni key ile yeniden eklemeniz gerekir.
/*Address? address = await context.Addresses.FindAsync(1);*/ //bu sekilde olmaz 
//context.Addresses.Remove(address);
//await context.SaveChangesAsync();

//Person person = await context.Persons.FindAsync(2);
//address.Person = person;
//await context.Addresses.AddAsync(address);

//await context.SaveChangesAsync();



#endregion

#endregion

#region One to Many ilişkisel senaryolarda veri güncelleme

#region Saving
//Blog blog = new()
//{
//    Name = "BurakCanDuran.com",
//    Posts = new List<Post>()
//    {
//      new(){Title = "1. Post"},
//      new(){Title = "2. Post"},
//      new(){Title = "3. Post"}
//    }

//};


#endregion

#region 1. Durum Esas tablodaki veriye bağımlı veriyi değiştirme
//await context.Blogs.AddAsync(blog);
//await context.SaveChangesAsync();

//Blog? blog = await context.Blogs
//    .Include(b => b.Posts)
//    .FirstOrDefaultAsync(b => b.Id == 1);

//Post? silinecekpost = blog.Posts.FirstOrDefault(p => p.Id == 2);
//blog.Posts.Remove(silinecekpost);

//blog.Posts.Add(new() { Title = "4. Post" });
//blog.Posts.Add(new() { Title = "5. Post" });

//await context.SaveChangesAsync();
#endregion

#region 2. Durum Bağımlı Verinin ilişkisel olduğu ana veriyi güncelleme
//Blog? blog = new()
//{
//    Name = "Ensar.Com"
//};
//await context.Blogs.AddAsync(blog);

//Post? post = await context.Posts.FindAsync(4);
//post.Blog = new()
//{
//    Name = "3. Blog"
//};

//await context.SaveChangesAsync();



Post? post = await context.Posts.FindAsync(4); // Id degeri 4 olan postu Blog Id degeri 2 olana atadım.
Blog? blog = await context.Blogs.FindAsync(2);
post.Blog = blog;
await context.SaveChangesAsync();


#endregion
#endregion


#region Many to Many senaryolarda veri güncelleme

#region Saving
//Book book1 = new() { BookName = "1. Kitap" };
//Book book2 = new() { BookName = "2. Kitap" };
//Book book3 = new() { BookName = "3. Kitap" };

//Author author1 = new() { AuthorName = "1. Yazar" };
//Author author2 = new() { AuthorName = "2. Yazar" };
//Author author3 = new() { AuthorName = "3. Yazar" };

//book1.Authors.Add(author1);
//book1.Authors.Add(author2);

//book2.Authors.Add(author1);
//book2.Authors.Add(author2);
//book2.Authors.Add(author2);

//book3.Authors.Add(author3);

//await context.AddRangeAsync(book1, book2, book3);
//await context.SaveChangesAsync();


#endregion

#region 1. kitabı 3. yazar ile ilişkilendirme.  
//Book? book = await context.Books.FindAsync(1); //Burada ilişkileri güncelledik, ekleme yapmadık o yüzden aşşağı da context.Add kullanmadık
//Author? author = await context.Authors.FindAsync(3);
//book.Authors.Add(author);

//await context.SaveChangesAsync();
#endregion

#region 2. Örnek
//Author? author = await context.Authors  //Burada Author ve Book ikisi de Principle, AuthorBook Dependent
//    .Include(a => a.Books)
//    .FirstOrDefaultAsync(x => x.Id == 3);
//foreach (var book in author.Books)
//{
//    if (book.Id != 1)
//    {
//        author.Books.Remove(book);
//    }
//}
//await context.SaveChangesAsync();
#endregion

#region 3. Örnek
//Book? book = await context.Books
//    .Include(a => a.Authors)
//    .FirstOrDefaultAsync(x => x.Id == 2);

//Author? silinecekyazar = book.Authors.FirstOrDefault(x => x.Id == 1);
//book.Authors.Remove(silinecekyazar);

//Author author = await context.Authors.FindAsync(3);
//book.Authors.Add(author);
//book.Authors.Add(new() { AuthorName = "4. Yazar" });

//await context.SaveChangesAsync();   





#endregion

#endregion



class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Address Address { get; set; }
}
class Address
{
    public int Id { get; set; }
    public string PersonAddress { get; set; }

    public Person Person { get; set; }
}
class Blog
{
    public Blog()
    {
        Posts = new HashSet<Post>();
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}
class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Title { get; set; }

    public Blog Blog { get; set; }
}
class Book
{
    public Book()
    {
        Authors = new HashSet<Author>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }

    public ICollection<Author> Authors { get; set; }
}
class Author
{
    public Author()
    {
        Books = new HashSet<Book>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<Book> Books { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("   ");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .HasOne(a => a.Person)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(a => a.Id);
    }
}

