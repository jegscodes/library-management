namespace Library.Application.Contracts.Responses;

public class EntityCreatedResponse
{
    public int Id { get; }
    public EntityCreatedResponse(int id)
    {
        Id = id;
    }

    public static implicit operator EntityCreatedResponse(int id) => new EntityCreatedResponse(id);
}
