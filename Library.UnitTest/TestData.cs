using Library.Domain.Entities.Authors;
using System.Security.Cryptography.X509Certificates;

namespace Library.UnitTest;

static class TestData
{
    public static int AuthorId = 1;
    public static string DefaultIsbn = "978-0-306-40615-7";
    public static string DefaultTitle = "Default Title";
    public static DateTime DefaultPublishedDate = DateTime.Now.Date;

    public static string AuthorName = "John Doe";
    public static string AuthorEmail = "jdoe@mailinator.com";

    public static Book CreateDefaultBook()
        => CreateBook(AuthorId, DefaultTitle, DefaultIsbn, DefaultPublishedDate);

    public static Book CreateBook(int authorId, string title, string isbn, DateTime publishedDate)
        => new Book(authorId, title, CreatePublishDate(publishedDate), CreateISBN(isbn));
    
    public static BookIdentifier CreateISBN(string isbn)
        => BookIdentifier.Create(isbn);

    public static PublishedDate CreatePublishDate(DateTime publishedDate)
        => PublishedDate.Create(publishedDate);


    public static Author CreateDefaultAuthor() => CreateAuthor(AuthorId, AuthorName, AuthorEmail);

    public static Author CreateAuthor(int id, string name, string email)
    {
        var author = new Author(name, email);

        return author;
    }
       
}
