namespace Tasko.Domains.Interfaces;

public interface IGetRequestResponseContent<T>
{
    public int Count { get; set; }

    public List<T> Items { get; set; }
}
