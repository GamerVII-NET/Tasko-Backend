using Newtonsoft.Json;
using System.Net;
using Tasko.Client.Services;
using Tasko.Domains.Models.DTO.User;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Models.RequestResponses;

namespace Tasko.Client.ViewModels;


public interface IBoardViewModel
{

    HttpClient HttpClient { get; set; }
    BoardRead Board { get; set; }
    UserAuthRead UserInfo { get; set; }
    IBoardsService BoardsService { get; set; }

    Task GetBoardAsync(Guid boardGuid);

    Task GetProfileAsync();
}

public abstract class BoardViewModelBase : IBoardViewModel
{

    public BoardViewModelBase(IHttpClientFactory clientFactory, IBoardsService boardsService)
    {
        HttpClient = clientFactory.CreateClient("base");
        BoardsService = boardsService;
    }

    public abstract BoardRead Board { get; set; }
    public abstract HttpClient HttpClient { get; set; }
    public abstract UserAuthRead UserInfo { get; set; }
    public abstract IBoardsService BoardsService { get; set; }

    public abstract Task GetBoardAsync(Guid boardGuid);

    public abstract Task GetProfileAsync();
}

public class BoardViewModel : BoardViewModelBase
{
    public BoardViewModel(IHttpClientFactory clientFactory, IBoardsService boardsService) : base(clientFactory, boardsService)
    {
    }

    public override BoardRead Board { get; set; }
    public override HttpClient HttpClient { get; set; }
    public override UserAuthRead UserInfo { get; set; }
    public override IBoardsService BoardsService { get; set; }

    public override async Task GetBoardAsync(Guid boardGuid)
    {

        var boardInfo = await BoardsService.GetBoardInfoAsync(boardGuid, UserInfo.Token);

        if (boardInfo.Item2== HttpStatusCode.OK)
        {
            Board = (boardInfo.Item1 as RequestResponse<BoardRead>).Response;
        }

    }

    public override async Task GetProfileAsync()
    {
        var user = await SecureStorage.GetAsync("UserInfo");

        if (user != null)
            UserInfo = JsonConvert.DeserializeObject<UserAuthRead>(user);

        

    }
}