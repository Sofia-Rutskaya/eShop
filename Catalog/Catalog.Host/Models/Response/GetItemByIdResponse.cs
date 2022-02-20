namespace Catalog.Host.Models.Response
{
    public class GetItemByIdResponse<TItem>
    {
        public int Id { get; init; }
        public TItem Data { get; init; } = default(TItem) !;
    }
}
