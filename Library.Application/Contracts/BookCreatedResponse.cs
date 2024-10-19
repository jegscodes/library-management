namespace Library.Application.Contracts;

public class BookCreatedResponse
{
    public int Id { get; }
    public BookCreatedResponse(int id)
    {
        Id = id;
    }

    public static implicit operator BookCreatedResponse(int id) => new BookCreatedResponse(id);
}
