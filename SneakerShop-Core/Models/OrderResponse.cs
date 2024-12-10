using System;
namespace SneakerShop_Core.Models
{
	public class OrderResponse
	{
		public long Id { get; set; }
		public double Total { get; set; }
        public string CreatedAt { get; set; } = null!;
        public UserService.UserData User{ get; set; }
		public List<ProductService.ProductData> Products { get; set; }
		public List<OrderService.OrderDetailData> OrderDetails { get; set; }
        public OrderResponse()
		{
		}
	}
}

