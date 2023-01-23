namespace Tasko.Domains.Models.RequestResponses;

public class RequestResponseContentCollection<T> : IRequestResponseContentCollection<T>
{
    public int Count { get; set; }
    public List<T> Items { get; set; }

    public RequestResponseContentCollection()
    {

    }

    public RequestResponseContentCollection(IEnumerable<T> items)
    {
        Count = items.Count();
        Items = items.ToList();
    }
}
