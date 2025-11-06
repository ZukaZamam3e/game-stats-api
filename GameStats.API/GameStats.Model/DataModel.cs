namespace GameStats.Model;

public class DataModel<T>
{
    public IEnumerable<T> Data { get; set; }

    public int Count { get; set; }
}
