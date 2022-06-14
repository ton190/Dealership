using EntityLibrary;

namespace ModelLibrary.Orders;

public class OrderDto
    : BaseDto, IMap<Order, OrderDto>
{
    public string? SessionId { get; set; }
    public string? RecordSearch { get; set; }
    public string? SearchResult { get; set; }
    public string? Status { get; set; }
    public string? Email { get; set; }
}
