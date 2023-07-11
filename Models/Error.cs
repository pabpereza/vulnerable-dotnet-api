namespace WebApi.Models
{
    public class Error
    {
        public string Message { get; set; }
        public string Route { get; set; }
        public string Method { get; set; }
        public DateTime date { get; set; }
    }
}