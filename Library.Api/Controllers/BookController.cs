using Library.Application.Books.Commands.UpdateBook;
using Library.Application.Books.Queries.GetBooks;
using Library.Application.Common.Extensions;
using Library.Application.Common.Models;
using Library.Application.Contracts.Requests;
using Library.Application.Contracts.Responses;

namespace Library.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;
    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

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

    [HttpGet("{bookId:int}")]
    [ProducesResponseType(typeof(GetBookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int bookId)
    {
        var result = await _mediator.Send(new GetBookQuery(bookId));

        if(result.IsFailure)
        {
            return NotFound(result.Error);
        }

        var book = result.Value()
                         .ToBookResponse();

        return Ok(book);  
    }

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

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<GetBookResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, int pageSize = 50)
    {
        var result = await _mediator.Send(new GetBooksQuery(pageNumber, pageSize));

        var paginatedBooks = result.ToPaginatedBookResponse();

        return Ok(paginatedBooks);
    }
}
