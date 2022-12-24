using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Models.RequestResponses;

namespace Tasko.Client.Services;

public interface IBoardsService
{
    HttpClient HttpClient { get; set; }
    Task<IEnumerable<BoardRead>> GetBoardsAsync(string token);
    Task<(IBaseRequestResponse, HttpStatusCode)> GetBoardInfoAsync(Guid boardGuid, string token);
}

public class BoardsService : IBoardsService
{
    public HttpClient HttpClient { get; set; }

    public BoardsService(IHttpClientFactory clientFactory)
    {
        HttpClient = clientFactory.CreateClient("base");        
    }

    public async Task<IEnumerable<BoardRead>> GetBoardsAsync(string token)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await HttpClient.GetAsync($"/api/boards");

        if (response.IsSuccessStatusCode)
        {
            var boardsContent = await response.Content.ReadAsStringAsync();

            var boards = JsonConvert.DeserializeObject<GetRequestResponse<BoardRead>>(boardsContent);

            return boards.Response.Items;
        }

        return Enumerable.Empty<BoardRead>();
    }


    public async Task<(IBaseRequestResponse, HttpStatusCode)> GetBoardInfoAsync(Guid boardGuid, string token)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await HttpClient.GetAsync($"/api/boards/{boardGuid}");
        var result = await response.Content.ReadAsStringAsync();

        RequestResponse<BoardRead> boardModel;

        if (response.IsSuccessStatusCode)
            boardModel = JsonConvert.DeserializeObject<RequestResponse<BoardRead>>(result);
        else
            return (JsonConvert.DeserializeObject<BadRequestResponse<IEnumerable<Models.ValidationFailure>>>(result), response.StatusCode);

        return (boardModel, response.StatusCode);
    }

}