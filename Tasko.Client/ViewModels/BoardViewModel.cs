using System.Net;
using Tasko.Client.Services;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Models.RequestResponses;

namespace Tasko.Client.ViewModels;


public interface IBoardViewModel
{

    HttpClient HttpClient { get; set; }
    BoardRead Board { get; set; }

    Task GetBoardAsync(Guid boardGuid);
}

public abstract class BoardViewModelBase : IBoardViewModel
{

    public BoardViewModelBase(IHttpClientFactory clientFactory) => HttpClient = clientFactory.CreateClient("base");

    public abstract BoardRead Board { get; set; }
    public abstract HttpClient HttpClient { get; set; }

    public abstract Task GetBoardAsync(Guid boardGuid);
}

public class BoardViewModel : BoardViewModelBase
{
    public BoardViewModel(IHttpClientFactory clientFactory) : base(clientFactory)
    {
    }

    public override BoardRead Board { get; set; }
    public override HttpClient HttpClient { get; set; }

    public override async Task GetBoardAsync(Guid boardGuid)
    {
        var boardInfo = await BoardsService.GetBoardInfoAsync(HttpClient, boardGuid);

        if (boardInfo.Item2== HttpStatusCode.OK)
        {
            Board = (boardInfo.Item1 as RequestResponse<BoardRead>).Response;
        }

    }
}