namespace Tasko.Client.Helpers;


public class ValidateHeaderHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!request.Headers.Contains("Authorization") && 
            request.RequestUri.LocalPath != "/api/auth" &&
            (request.RequestUri.LocalPath != "/api/users" && 
            request.Method.Method == "POST")
            )
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}