namespace Library.Api.Controllers;

/// <summary>
/// Handles requests related to authors.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator for sending commands and queries.</param>
    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new author.
    /// </summary>
    /// <param name="request">The request containing author details.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAuthorRequest request)
    {
        var result = await _mediator.Send(new CreateAuthorCommand(request.Name, request.Email));

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetById), new { authorId = result.Value()?.Id }, null);
    }

    /// <summary>
    /// Retrieves a paginated list of authors.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve (default is 1).</param>
    /// <param name="pageSize">The number of authors per page (default is 50).</param>
    /// <returns>An <see cref="IActionResult"/> containing the paginated list of authors.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<GetAuthorResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, int pageSize = 50)
    {
        var result = await _mediator.Send(new GetAuthorsQuery(pageNumber, pageSize));

        var paginatedAuthors = result.ToPaginatedAuthorReponse();

        return Ok(paginatedAuthors);
    }

    /// <summary>
    /// Retrieves an author by their unique identifier.
    /// </summary>
    /// <param name="authorId">The unique identifier of the author.</param>
    /// <returns>An <see cref="IActionResult"/> containing the author details or a not found status.</returns>
    [HttpGet("{authorId:int}")]
    [ProducesResponseType(typeof(GetAuthorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int authorId)
    {
        var result = await _mediator.Send(new GetAuthorQuery(authorId));

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        var authorResponse = result.Value()
                                   .ToAuthorResponse();

        return Ok(authorResponse);
    }
}
