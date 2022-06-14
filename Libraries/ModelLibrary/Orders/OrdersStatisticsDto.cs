namespace ModelLibrary.Orders;

public class OrdersStatisticsDto
{
    public int TodayOrdersPaid { get; set; }
    public int WeekOrdersPaid { get; set; }
    public int MonthOrdersPaid { get; set; }
    public int TotalOrdersPaid { get; set; }
}
