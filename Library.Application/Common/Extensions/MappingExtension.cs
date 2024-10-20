using Library.Application.Common.Models;
using Library.Application.Contracts.Responses;

namespace Library.Application.Common.Extensions;

public static class MappingExtension
{
    public static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

    public static GetBookResponse ToBookResponse(this Book book)
    {
        return new GetBookResponse
        {
            Id = book.Id,
            Author = book.Author.Name,
            Title = book.Title,
            PublishedDate = book.PublishedDate.ToString(),
            ISBN = book.ISBN.Value,
        };
    }

    public static PaginatedList<GetBookResponse> ToPaginatedBookResponse(this IPaginated<Book> paginatedBooks)
    {
        var bookResponse = paginatedBooks.Items
                                         .Select(book => ToBookResponse(book))
                                         .ToList();

        return new PaginatedList<GetBookResponse>(bookResponse,
                                                  paginatedBooks.TotalCount,
                                                  paginatedBooks.PageNumber,
                                                  paginatedBooks.PageSize
                                                  );
    }
}
