using Tasko.BoardSevice.Infrasructure.Repositories;
using Tasko.Domains.Models.RequestResponses;
using Tasko.Domains.Models.Structural;
using Tasko.Jwt.Models;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace Tasko.BoardSevice.Infrasructure.Functions
{
    public class RequestHandler
    {
        internal static Func<IBoardRepository, IMapper, Guid, Task<IResult>> FindBoard()
        {
            return [Authorize] async (IBoardRepository boardRepository, IMapper mapper, Guid id) =>
            {
                var board = await boardRepository.FindBoardAsync(id);
                if (board == null)
                {
                    var errors = new List<ValidationFailure>
                    {
                        new ValidationFailure("Id", "The board with this guid was not found", id)
                    };

                    var response = new BadRequestResponse<List<ValidationFailure>>(errors, "The board with this guid was not found", StatusCodes.Status404NotFound);

                    return Results.NotFound(id);
                }
                var boardRead = mapper.Map<BoardRead>(board);
                return Results.Ok(new RequestResponse<IBoardRead>(boardRead, StatusCodes.Status200OK));
            };
        }

        internal static Func<IBoardRepository, IMapper, Task<IResult>> GetBoards()
        {
            return [Authorize] async (IBoardRepository repository, IMapper mapper) =>
            {
                var response = mapper.Map<IEnumerable<IBoard>, IEnumerable<BoardRead>>(await repository.GetBoardsAsync());

                return Results.Ok(new GetRequestResponse<IBoardRead>(response));
            };
        }

        internal static Func<IBoardRepository, IMapper, BoardCreate, IValidator<IBoardCreate>, Task<IResult>> CreateBoard(ValidationParameter validationParameter)
        {
            return [Authorize] async (IBoardRepository boardRepository, IMapper mapper, BoardCreate boardCreate, IValidator<IBoardCreate> validator) =>
            {
                var boardValidate = validator.Validate(boardCreate);

                if (!boardValidate.IsValid)
                {
                    return Results.BadRequest(new BadRequestResponse<List<ValidationFailure>>(boardValidate.Errors, "Board not valid"));
                }

                var board = mapper.Map<IBoardCreate, Board>(boardCreate);

                await boardRepository.CreateBoardAsync(board);

                var response = new RequestResponse<IBoard>(board, StatusCodes.Status201Created);

                return Results.Created($"/api/boards/{board.Id}", response);
            };
        }

        internal static Func<HttpContext, IBoardRepository, IMapper, BoardUpdate, IValidator<IBoardUpdate>, Task<IResult>> UpdateBoard(ValidationParameter validationParmeter)
        {
            return [Authorize] async (HttpContext context, IBoardRepository boardRepository, IMapper mapper, BoardUpdate BoardUpdate, IValidator<IBoardUpdate> validator) =>
            {
                return Results.Ok();
            };
        }

        internal static Func<HttpContext, IBoardRepository, IMapper, Guid, Task<IResult>> DeleteRole(ValidationParameter validationParmeter)
        {
            return [Authorize] async (HttpContext context, IBoardRepository boardRepository, IMapper mapper, Guid id) =>
            {
                return Results.Ok();
            };
        }
    }
}