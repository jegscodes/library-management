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
            Author = book.Author is null ? string.Empty : book.Author.Name,
            Title = book.Title,
            PublishedDate = book.PublishedDate.ToString(),
            ISBN = book.ISBN.Value,
        };
    }

    public static GetAuthorResponse ToAuthorResponse(this Author author)
    {
        return new GetAuthorResponse
        {
            Name = author.Name,
            Email = author.Email.Value,
            NoOfBooks = author.Books.Count,
        };
    }

    public static PaginatedList<GetAuthorResponse> ToPaginatedAuthorReponse(this IPaginated<Author> paginatedAuthors)
    {
        var authorReponse = paginatedAuthors.Items.Select(author => ToAuthorResponse(author)).ToList();

        return new PaginatedList<GetAuthorResponse>(authorReponse,
                                                    paginatedAuthors.TotalCount,
                                                    paginatedAuthors.PageNumber,
                                                    paginatedAuthors.PageSize
                                                    );
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
