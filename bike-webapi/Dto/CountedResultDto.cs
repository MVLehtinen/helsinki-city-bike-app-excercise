namespace bike_webapi.Dto
{
    public class CountedResultDto<T>
    {
        public int Total { get; set; }
        public T Item { get; set; } = default!;
    }
}
