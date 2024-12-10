using System;
namespace SneakerShop_Core.Models
{
	public class ProductResponse
	{
        public List<ProductService.ProductData> products { get; set; }
        public List<StockService.StockData> stocks { get; set; }
        public ProductResponse()
		{
		}
	}
}

