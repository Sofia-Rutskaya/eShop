namespace Catalog.Host.Models.Requests
{
    public class DeleteDataRequest
    {
        public int Id { get; set; }
        public string Data { get; set; } = null!;
    }
}
