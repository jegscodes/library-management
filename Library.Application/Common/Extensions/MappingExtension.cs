namespace Library.Application.Common.Extensions;

/// <summary>
/// Provides extension methods for mapping entities to response models and for pagination.
/// </summary>
public static class MappingExtension
{
    /// <summary>
    /// Maps an <see cref="Author"/> entity to a <see cref="GetAuthorResponse"/> response model.
    /// </summary>
    /// <param name="author">The author entity to map.</param>
    /// <returns>A <see cref="GetAuthorResponse"/> representing the author.</returns>
    public static GetAuthorResponse ToAuthorResponse(this Author author)
    {
        return new GetAuthorResponse
        {
            Name = author.Name,
            Email = author.Email.Value,
            NoOfBooks = author.Books.Count,
        };
    }

    /// <summary>
    /// Maps a <see cref="Book"/> entity to a <see cref="GetBookResponse"/> response model.
    /// </summary>
    /// <param name="book">The book entity to map.</param>
    /// <returns>A <see cref="GetBookResponse"/> representing the book.</returns>
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

    /// <summary>
    /// Converts a paginated list of <see cref="Author"/> entities to a paginated list of <see cref="GetAuthorResponse"/> response models.
    /// </summary>
    /// <param name="paginatedAuthors">The paginated list of authors.</param>
    /// <returns>A <see cref="PaginatedList{GetAuthorResponse}"/> representing the paginated authors.</returns>
    public static PaginatedList<GetAuthorResponse> ToPaginatedAuthorReponse(this IPaginated<Author> paginatedAuthors)
    {
        var authorReponse = paginatedAuthors.Items.Select(author => ToAuthorResponse(author)).ToList();

        return new PaginatedList<GetAuthorResponse>(authorReponse,
                                                    paginatedAuthors.TotalCount,
                                                    paginatedAuthors.PageNumber,
                                                    paginatedAuthors.PageSize);
    }

    /// <summary>
    /// Converts a paginated list of <see cref="Book"/> entities to a paginated list of <see cref="GetBookResponse"/> response models.
    /// </summary>
    /// <param name="paginatedBooks">The paginated list of books.</param>
    /// <returns>A <see cref="PaginatedList{GetBookResponse}"/> representing the paginated books.</returns>
    public static PaginatedList<GetBookResponse> ToPaginatedBookResponse(this IPaginated<Book> paginatedBooks)
    {
        var bookResponse = paginatedBooks.Items
                                         .Select(book => ToBookResponse(book))
                                         .ToList();

        return new PaginatedList<GetBookResponse>(bookResponse,
                                                  paginatedBooks.TotalCount,
                                                  paginatedBooks.PageNumber,
                                                  paginatedBooks.PageSize);
    }

    /// <summary>
    /// Converts an <see cref="IQueryable{T}"/> to a paginated list asynchronously.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination objects.</typeparam>
    /// <param name="queryable">The queryable collection of source objects.</param>
    /// <param name="pageNumber">The number of the page to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated list of <typeparamref name="TDestination"/>.</returns>
    public static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);
}
