namespace Library.Api.Controllers;

/// <summary>
/// Handles requests related to books.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator for sending commands and queries.</param>
    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new book.
    /// </summary>
    /// <param name="request">The request containing book details.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
    {
        var result = await _mediator.Send(new CreateBookCommand(request.AuthorId, request.Title, request.PublishedDate, request.ISBN));

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return CreatedAtAction(nameof(GetById), new { bookId = result.Value()?.Id }, null);
    }

    /// <summary>
    /// Retrieves a paginated list of books.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve (default is 1).</param>
    /// <param name="pageSize">The number of books per page (default is 50).</param>
    /// <returns>An <see cref="IActionResult"/> containing the paginated list of books.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<GetBookResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, int pageSize = 50)
    {
        var result = await _mediator.Send(new GetBooksQuery(pageNumber, pageSize));

        var paginatedBooks = result.ToPaginatedBookResponse();

        return Ok(paginatedBooks);
    }

    /// <summary>
    /// Retrieves a book by its unique identifier.
    /// </summary>
    /// <param name="bookId">The unique identifier of the book.</param>
    /// <returns>An <see cref="IActionResult"/> containing the book details or a not found status.</returns>
    [HttpGet("{bookId:int}")]
    [ProducesResponseType(typeof(GetBookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int bookId)
    {
        var result = await _mediator.Send(new GetBookQuery(bookId));

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        var book = result.Value().ToBookResponse();

        return Ok(book);
    }

    /// <summary>
    /// Updates an existing book.
    /// </summary>
    /// <param name="bookId">The unique identifier of the book to update.</param>
    /// <param name="request">The request containing updated book details.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the update operation.</returns>
    [HttpPut("{bookId:int}")]
    [ProducesResponseType(typeof(GetBookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int bookId, [FromBody] UpdateBookRequest request)
    {
        var result = await _mediator.Send(new UpdateBookCommand(bookId, request.AuthorId, request.Title, request.ISBN, request.PublishedDate));

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok();
    }
}
