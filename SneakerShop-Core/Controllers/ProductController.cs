using Microsoft.AspNetCore.Mvc;

namespace SneakerShop_Core.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private ProductService.Product.ProductClient _productClient;
        private StockService.Stock.StockClient _stockClient;

        public ProductController(ProductService.Product.ProductClient productClient, StockService.Stock.StockClient stockClient)
        {
            _productClient = productClient;
            _stockClient = stockClient;
        }

        [HttpPost]
        public async Task<ProductService.CreateProductReply> CreateProduct(ProductService.CreateProductRequest createProductRequest)
        {
            var result = await _productClient.AddProductAsync(createProductRequest);
            return result;
        }

        [HttpGet("paginate")]
        public async Task<ProductService.GetProductPaginateReply> GetProductPaginate([FromQuery]long afterID, [FromQuery]int limit)
        {
            return await _productClient.GetProductPaginateAsync(new ProductService.GetProductPaginateRequest { AfterID = afterID, Limit = limit });
        }

        [HttpGet("total")]
        public async Task<ProductService.GetNumOfProductReply> GetNumOfProduct()
        {
            return await _productClient.GetNumOfProductAsync(new ProductService.GetNumOfProductRequest { Message = "" });
        }

        [HttpGet("search")]
        public async Task<ProductService.GetProductByIdReply> GetProductByIdReply([FromQuery]long id)
        {
            return await _productClient.GetProductByIdAsync(new ProductService.GetProductByIdRequest { Id = id });
        }

        [HttpPut("update")]
        public async Task<ProductService.UpdateProductReply> UpdateProduct(ProductService.UpdateProductRequest updateProductRequest)
        {
            return await _productClient.UpdateProductAsync(updateProductRequest);
        }

        [HttpDelete("delete")]
        public async Task<ProductService.DeleteProductReply> DeleteProduct([FromQuery]long id)
        {
            return await _productClient.DeleteProductAsync(new ProductService.DeleteProductRequest { Id = id });
        }

        [HttpGet("all")]
        public async Task<Models.ProductResponse> GetALLProductDetail()
        {
            var stocks = new List<StockService.StockData>();
            var total = await _productClient.GetNumOfProductAsync(new ProductService.GetNumOfProductRequest { Message = "" });
            var products = await _productClient.GetProductPaginateAsync(new ProductService.GetProductPaginateRequest { AfterID = 0, Limit = (int)total.Total }); 
            foreach(var item in products.ProductList)
            {
                var stock = await _stockClient.GetStockByIdAsync(new StockService.GetStockByProdIdRequest { ProdID = item.Id });
                stocks.Add(stock.Data);
            }
            return new Models.ProductResponse { stocks = stocks, products = products.ProductList.ToList() };
        }
    }
}
