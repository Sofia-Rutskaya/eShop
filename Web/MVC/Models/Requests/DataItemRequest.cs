namespace MVC.Models.Requests
{
    public class DataItemRequest<TItem>
    {
        public string? Data { get; init; }
        public IEnumerable<TItem> Items { get; init; } = null!;
    }
}
