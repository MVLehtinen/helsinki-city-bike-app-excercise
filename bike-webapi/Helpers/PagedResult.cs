
namespace bike_webapi.Helpers
{
    public class PagedResult<T>
    {
        public int Total { get; set; }
        public List<T> Result { get; set; } = null!;
    }
}