namespace Catalog.Host.Models.Requests
{
    public class CreateOrDeleteDataRequest
    {
        public int Id { get; set; }
        public string Data { get; set; } = null!;
    }
}
