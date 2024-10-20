namespace Library.Application.Contracts.Responses;

public class GetAuthorResponse
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int NoOfBooks { get; set; }
}
