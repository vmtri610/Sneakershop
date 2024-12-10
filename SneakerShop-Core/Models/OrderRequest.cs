namespace SneakerShop_Core.Models
{
    public class OrderRequest
    {
        public OrderService.OrderData? OrderData { get; set; }
        public List<OrderService.OrderDetailData>? Details { get; set; }
    }
}
