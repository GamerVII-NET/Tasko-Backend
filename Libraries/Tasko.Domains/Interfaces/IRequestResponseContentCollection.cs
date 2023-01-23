namespace Tasko.Domains.Interfaces;

public interface IRequestResponseContentCollection<T>
{
    public int Count { get; set; }

    public List<T> Items { get; set; }
}
