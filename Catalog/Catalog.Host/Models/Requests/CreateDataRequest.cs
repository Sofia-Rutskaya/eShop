namespace Catalog.Host.Models.Requests
{
    public class CreateDataRequest
    {
        public int Id { get; set; }
        public string Data { get; set; } = null!;
    }
}
