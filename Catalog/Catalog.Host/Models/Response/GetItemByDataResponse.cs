namespace Catalog.Host.Models.Response
{
    public class GetItemByDataResponse<TItem>
    {
        public string Data { get; init; } = null!;
        public IEnumerable<TItem> Items { get; init; } = null!;
    }
}
