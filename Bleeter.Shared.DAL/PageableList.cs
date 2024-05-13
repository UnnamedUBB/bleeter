namespace Bleeter.Shared.DAL;

public class PageableList<T>
{
    public int TotalCount { get; set; }
    public List<T> Data { get; set; }
}