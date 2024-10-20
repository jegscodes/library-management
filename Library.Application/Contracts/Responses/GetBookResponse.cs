namespace Library.Application.Contracts.Responses;

public class GetBookResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string PublishedDate { get; set; } = string.Empty;
}
