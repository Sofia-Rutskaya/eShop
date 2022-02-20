namespace Catalog.Host.Data
{
    public class ListOfItems<TItem>
    {
        public IEnumerable<TItem> Data { get; init; } = null!;
    }
}
