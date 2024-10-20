using Library.Application.Authors.Commands.CreateAuthor;
using Library.Application.Authors.Queries.GetAuthor;
using Library.Application.Authors.Queries.GetAuthors;
using Library.Application.Common.Extensions;
using Library.Application.Common.Models;
using Library.Application.Contracts.Requests;
using Library.Domain.Entities.Authors;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

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

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<Author>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, int pageSize = 50)
        {
            var result = await _mediator.Send(new GetAuthorsQuery(pageNumber, pageSize));

            var paginatedAuthors = result.ToPaginatedAuthorReponse();

            return Ok(paginatedAuthors);  
        }

        [HttpGet("{authorId:int}")]
        [ProducesResponseType(typeof(Author), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int authorId)
        {
            var result = await _mediator.Send(new GetAuthorQuery(authorId));

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value());
        }
    }
}
