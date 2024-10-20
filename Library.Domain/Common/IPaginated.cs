namespace Library.Domain.Common;

public interface IPaginated<T>
{
    IReadOnlyCollection<T> Items { get; }
    int TotalCount { get; }
    int PageNumber { get; }
    int PageSize { get; }
    int TotalPages { get; }

    bool HasPreviousPage { get; }
    bool HasNextPage { get; }
}
