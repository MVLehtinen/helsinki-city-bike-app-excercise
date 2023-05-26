
namespace bike_webapi.Dto
{
    public class PagedResultDto<T>
    {
        public int Total { get; set; }
        public List<T> Result { get; set; } = null!;
    }
}
