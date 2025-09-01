namespace GameStats.Model;

public class PagedQuery<TFilter>(int? take, int? offset, TFilter filter)
{
    public TFilter Filter { get; set; } = filter;

    public int Take { get; set; } = take ?? 10;

    public int Offset { get; set; } = offset ?? 0;
}
