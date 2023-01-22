namespace Tasko.Domains.Models.RequestResponses;

public class GetRequestResponseContent<T> : IGetRequestResponseContent<T>
{
    public int Count { get; set; }
    public List<T> Items { get; set; }

    public GetRequestResponseContent()
    {

    }

    public GetRequestResponseContent(IEnumerable<T> items)
    {
        Count = items.Count();
        Items = items.ToList();
    }
}
