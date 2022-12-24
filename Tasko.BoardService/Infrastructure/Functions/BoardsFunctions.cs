using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Tasko.BoardSevice.Infrasructure.Repositories;
using Tasko.Domains.Models.DTO.Board;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Models.RequestResponses;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace Tasko.BoardSevice.Infrasructure.Functions
{
    public class BoardsFunctions
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
                        new ValidationFailure("id", "Доска с указанным Guid не найдена", id)
                    };

                    var response = new BadRequestResponse<List<ValidationFailure>>(errors, "Доска с указанным Guid не найдена", StatusCodes.Status404NotFound);

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

        internal static Func<IBoardRepository, IMapper, BoardCreate, IValidator<IBoardCreate>, Task<IResult>> CreateBoard(JwtValidationParameter jwtValidationParameter)
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

        internal static Func<HttpContext, IBoardRepository, IMapper, BoardUpdate, IValidator<IBoardUpdate>, Task<IResult>> UpdateBoard(JwtValidationParameter jwtValidationParmeter)
        {
            return [Authorize] async (HttpContext context, IBoardRepository boardRepository, IMapper mapper, BoardUpdate BoardUpdate, IValidator<IBoardUpdate> validator) =>
            {

                return Results.Ok();
            };
        }

        internal static Func<HttpContext, IBoardRepository, IMapper, Guid, Task<IResult>> DeleteRole(JwtValidationParameter jwtValidationParmeter)
        {
            return [Authorize] async (HttpContext context, IBoardRepository boardRepository, IMapper mapper, Guid id) =>
            {
                return Results.Ok();
            };
        }
    }
}