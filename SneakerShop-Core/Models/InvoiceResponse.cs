using System;
namespace SneakerShop_Core.Models
{
	public class InvoiceResponse
	{
		public long Id{ get; set; }
		public OrderService.OrderData? OrderData { get; set; }
		public string CreateAt { get; set; }
		public List<OrderService.OrderDetailData> OrderDetailData { get; set; }

        public InvoiceResponse()
		{

		}


	}
}

