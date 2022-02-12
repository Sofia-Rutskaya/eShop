namespace Catalog.Host.Data
{
    public class SingleItem<TItem>
    {
        public IEnumerable<TItem> Data { get; init; } = null!;
    }
}
